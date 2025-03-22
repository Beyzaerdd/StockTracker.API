using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.RemainingProductDTO;
using StockTracker.Shared.DTOs.RemainingProductDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;

public class RemainingProductService : IRemainingProductService
{
    private readonly IGenericRepository<RemainingProduct> _remainingProductRepository;
    private readonly IGenericRepository<ReturnedProduct> _returnedProduct;
    private readonly IGenericRepository<Rental> _rentalRepository;
    private readonly IGenericRepository<CustomerAccount> _customerAccountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RemainingProductService(
        IGenericRepository<RemainingProduct> remainingProductRepository,
        IGenericRepository<Rental> rentalRepository,
        IGenericRepository<CustomerAccount> customerAccountRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IGenericRepository<ReturnedProduct> returnedProduct)
    {
        _remainingProductRepository = remainingProductRepository;
        _rentalRepository = rentalRepository;
        _customerAccountRepository = customerAccountRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _returnedProduct = returnedProduct;
    }

    public async Task<ResponseDTO<IEnumerable<RemainingProductDTO>>> GetAllRemainingProductsAsync()
    {
        var remainingProducts = await _remainingProductRepository.GetAllAsync();
        var remainingProductDTOs = _mapper.Map<IEnumerable<RemainingProductDTO>>(remainingProducts);
        return ResponseDTO<IEnumerable<RemainingProductDTO>>.Success(remainingProductDTOs, StatusCodes.Status200OK);
    }

    public async Task<ResponseDTO<string>> UpdateRemainingProductAsync(UpdateRemainingProductDTO updateRemainingProductDTO)
    {
        var remainingProduct = await _remainingProductRepository.GetByIdAsync(updateRemainingProductDTO.Id);
        if (remainingProduct == null)
            return ResponseDTO<string>.Fail("Kalan ürün bulunamadı", StatusCodes.Status404NotFound);

        remainingProduct.DaysRemaining = updateRemainingProductDTO.DaysRemaining;

        _remainingProductRepository.Update(remainingProduct);
                remainingProduct.UpdatedAt = DateTime.Now;
        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Kalan ürün güncellendi", StatusCodes.Status200OK);
    }

    public async Task<ResponseDTO<string>> ProcessRemainingProductsAsync(int rentalId, bool createNewRental)
    {
        var rental = await _rentalRepository.GetAsync(r => r.Id == rentalId, query =>
            query.Include(r => r.RentalItems).ThenInclude(ri => ri.Product));
        if (rental == null)
            return ResponseDTO<string>.Fail("Kiralama bulunamadı", StatusCodes.Status404NotFound);

       
        var remainingProducts = await _remainingProductRepository.GetAllAsync(
     rp => rental.RentalItems.Select(ri => ri.Id).Contains(rp.RentalItemId),
     includes: query => query.Include(rp => rp.RentalItem)
                             .ThenInclude(ri => ri.Product) 
 );




        if (createNewRental)
        {
      
            var newRental = new Rental
            {
                CustomerId = rental.CustomerId,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                TotalPrice = 0,
                RentalItems = new List<RentalItem>()
            };


            foreach (var remainingProduct in remainingProducts)
            {
                var rentalItem = new RentalItem
                {
                    ProductId = remainingProduct.RentalItem.ProductId,
                    Quantity = remainingProduct.RentalItem.Quantity,
                    MonthlyPrice = remainingProduct.RentalItem.MonthlyPrice,
                };

                newRental.RentalItems.Add(rentalItem);
                newRental.TotalPrice += rentalItem.Quantity * rentalItem.MonthlyPrice;
            }

 
            await _rentalRepository.AddAsync(newRental);
            await _unitOfWork.SaveChangesAsync();

    
            var customerAccount = new CustomerAccount
            {
                CustomerId = rental.CustomerId,
                StartDate = DateTime.Now, 
                EndDate = DateTime.Now.AddMonths(1),
                TotalAmount = newRental.TotalPrice,
                PaidAmount = 0,
                Description = "Yeni kiralama ücreti"
            };

            await _customerAccountRepository.AddAsync(customerAccount);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDTO<string>.Success("Yeni kiralama oluşturuldu ve müşteri hesabına işlendi", StatusCodes.Status201Created);
        }
        else
        {

            var extraDays = (DateTime.Today - rental.EndDate).Days;
            if (extraDays > 0)
            {
                decimal extraCharge = 0;

                foreach (var remainingProduct in remainingProducts)
                {
                    var additionalPrice = (remainingProduct.RentalItem.MonthlyPrice/30) * extraDays * remainingProduct.RentalItem.Quantity;
                    extraCharge += additionalPrice;

                    var returnedProduct = new ReturnedProduct
                    {
                        RentalItem=remainingProduct.RentalItem,
                        RentalItemId = remainingProduct.RentalItem.Id, 
                        QuantityReturned = remainingProduct.RentalItem.Quantity, 
                        ReturnDate = DateTime.UtcNow 
                    };
                
                    await _returnedProduct.AddAsync(returnedProduct);
                    await _unitOfWork.SaveChangesAsync();
                }

               
                var existingCustomerAccount = await _customerAccountRepository.GetAsync(ca => ca.CustomerId == rental.CustomerId);

               
                    var customerAccount = new CustomerAccount
                    {
                        CustomerId = rental.CustomerId,
                        StartDate = rental.EndDate,
                        EndDate = DateTime.UtcNow,
                        TotalAmount = extraCharge,
                        PaidAmount = 0,
                        Description = $"Gecikme ücreti - İlk kiralama: {rental.StartDate:dd.MM.yyyy} - {rental.EndDate:dd.MM.yyyy}, Gecikme: {extraDays} gün"
                    };

                    await _customerAccountRepository.AddAsync(customerAccount);
                    await _unitOfWork.SaveChangesAsync();
                

                foreach (var remainingProduct in remainingProducts)
                {
                    _remainingProductRepository.Delete(remainingProduct);
                }

                await _unitOfWork.SaveChangesAsync();

                return ResponseDTO<string>.Success("Gecikme ücreti hesaplandı ve müşteri hesabına işlendi, teslim alındı", StatusCodes.Status200OK);
            }
            else
            {
                return ResponseDTO<string>.Success("Kiralama süresi henüz dolmadı, ek ücret uygulanmadı.", StatusCodes.Status200OK);
            }
        }
    }


    public async Task<ResponseDTO<IEnumerable<RemainingProductDTO>>> GetRemainingProductsByRentalIdAsync(int rentalId)
    {
        // Kiralamaya ait kalan ürünleri çekiyoruz
        var remainingProducts = await _unitOfWork.GetRepository<RemainingProduct>()
            .GetAllAsync(
                predicate: rp => rp.RentalItem.RentalId == rentalId,
                includes: query => query
                    .Include(rp => rp.RentalItem)
                    .ThenInclude(ri => ri.Product)
            );

        if (remainingProducts == null || !remainingProducts.Any())
        {
            return ResponseDTO<IEnumerable<RemainingProductDTO>>.Fail("Kalan ürünler bulunamadı", StatusCodes.Status404NotFound);
        }

        // Veritabanında tutulan kalan ürünler bilgisiyle DTO'yu oluşturuyoruz
        var remainingProductDTOs = remainingProducts.Select(rp => new RemainingProductDTO
        {
            ProductId = rp.RentalItem.ProductId,
            ProductName = rp.RentalItem.Product.Name,
            Quantity = rp.RentalItem.Quantity,
            DailyPrice = Math.Round(rp.RentalItem.MonthlyPrice / 30.0m, 2), // 2 ondalıklı yuvarlama
            TotalPrice = (int)(Math.Round((rp.RentalItem.MonthlyPrice / 30.0m) * rp.RentalItem.Quantity * rp.DaysRemaining, 2)), // Toplam fiyatı tam sayıya dönüştür
            DaysRemaining = rp.DaysRemaining
        }).ToList();

        return ResponseDTO<IEnumerable<RemainingProductDTO>>.Success(remainingProductDTOs, StatusCodes.Status200OK);
    }












}

