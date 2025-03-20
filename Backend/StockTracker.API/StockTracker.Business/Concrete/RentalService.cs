using AutoMapper;
using Microsoft.AspNetCore.Http;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.RentalDTOs;
using StockTracker.Shared.DTOs.RentalItemDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;

public class RentalService : IRentalService
{
    private readonly IGenericRepository<RemainingProduct> _remainingProductRepository;
    private readonly IGenericRepository<Rental> _rentalRepository;
    private readonly IGenericRepository<CustomerAccount> _customerAccountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RentalService(
        IGenericRepository<RemainingProduct> remainingProductRepository,
        IGenericRepository<Rental> rentalRepository,
        IGenericRepository<CustomerAccount> customerAccountRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _remainingProductRepository = remainingProductRepository;
        _rentalRepository = rentalRepository;
        _customerAccountRepository = customerAccountRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<string>> DeleteRentalAsync(int rentalId)
    {
        var rental = await _unitOfWork.GetRepository<Rental>().GetByIdAsync(rentalId);
        if (rental == null)
        {
            return ResponseDTO<string>.Fail("Kiralama bulunamadı.", StatusCodes.Status404NotFound);
        }


        foreach (var rentalItem in rental.RentalItems)
        {
            _unitOfWork.GetRepository<RentalItem>().Delete(rentalItem);
        }

        _unitOfWork.GetRepository<Rental>().Delete(rental);
        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Kiralama başarıyla silindi.", StatusCodes.Status200OK);
    }
    public async Task<ResponseDTO<string>> UpdateRentalItemAsync(int rentalId, int rentalItemId, UpdateRentalItemDTO updateRentalItemDTO)
    {
        var rentalItem = await _unitOfWork.GetRepository<RentalItem>()
                                           .FindAsync(item => item.RentalId == rentalId && item.Id == rentalItemId);

        if (rentalItem == null)
        {
            return ResponseDTO<string>.Fail("Rental item bulunamadı.", StatusCodes.Status404NotFound);
        }

        rentalItem.Quantity = updateRentalItemDTO.Quantity;
        rentalItem.MonthlyPrice = updateRentalItemDTO.MonthlyPrice;


        var rental = await _unitOfWork.GetRepository<Rental>().GetByIdAsync(rentalId);
        rental.TotalPrice = rental.RentalItems.Sum(item => item.Quantity * item.MonthlyPrice);

        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Rental item başarıyla güncellendi.", StatusCodes.Status200OK);
    }


    public async Task<ResponseDTO<string>> DeleteRentalItemAsync(int rentalId, int rentalItemId)
    {
        var rentalItem = await _unitOfWork.GetRepository<RentalItem>()
                                           .FindAsync(item => item.RentalId == rentalId && item.Id == rentalItemId);

        if (rentalItem == null)
        {
            return ResponseDTO<string>.Fail("Rental item bulunamadı.", StatusCodes.Status404NotFound);
        }

        _unitOfWork.GetRepository<RentalItem>().Delete(rentalItem);


        var rental = await _unitOfWork.GetRepository<Rental>().GetByIdAsync(rentalId);
        rental.TotalPrice = rental.RentalItems.Sum(item => item.Quantity * item.MonthlyPrice);

        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Rental item başarıyla silindi.", StatusCodes.Status200OK);
    }
    public async Task<ResponseDTO<string>> CreateRentalAsync(CreateRentalDTO createRentalDTO)
    {
        var rental = new Rental
        {
            CustomerId = createRentalDTO.CustomerId,
            StartDate = createRentalDTO.StartDate,
            EndDate = createRentalDTO.EndDate,
            RentalItems = new List<RentalItem>()
        };

        var customer = await _unitOfWork.GetRepository<Customer>().GetByIdAsync(createRentalDTO.CustomerId);

        if (customer == null)
        {
            return ResponseDTO<string>.Fail("Müşteri bulunamadı.", StatusCodes.Status404NotFound);
        }

        decimal totalPrice = 0;

        foreach (var item in createRentalDTO.RentalItems)
        {
            var rentalItem = new RentalItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                MonthlyPrice = item.MonthlyPrice
            };

            rental.RentalItems.Add(rentalItem);

            decimal itemTotalPrice = rentalItem.MonthlyPrice * rentalItem.Quantity;

            if (createRentalDTO.VATRate.HasValue)
            {
                decimal vatAmount = itemTotalPrice * createRentalDTO.VATRate.Value / 100;
                itemTotalPrice += vatAmount;
            }

            totalPrice += itemTotalPrice;

            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(item.ProductId);

            if (product == null)
            {
                return ResponseDTO<string>.Fail("Ürün bulunamadı.", StatusCodes.Status404NotFound);
            }

            if (product.StockQuantity < item.Quantity)
            {
                return ResponseDTO<string>.Fail("Yeterli stok bulunmamaktadır.", StatusCodes.Status400BadRequest);
            }

            product.StockQuantity -= item.Quantity;
            _unitOfWork.GetRepository<Product>().Update(product);
        }

        rental.TotalPrice = totalPrice;


        await _unitOfWork.GetRepository<Rental>().AddAsync(rental);
        await _unitOfWork.SaveChangesAsync();

        foreach (var rentalItem in rental.RentalItems)
        {
            var startDate = rental.StartDate;  
            var today = DateTime.Now.Date;  

            var daysUsed = (today - startDate).Days + 1; 



            daysUsed = daysUsed < 0 ? 0 : daysUsed; 

      
            var remainingProduct = new RemainingProduct
            {
                RentalItemId = rentalItem.Id,
                DaysRemaining = daysUsed  
            };

            await _unitOfWork.GetRepository<RemainingProduct>().AddAsync(remainingProduct);
        }


        var customerAccount = new CustomerAccount
        {
            CustomerId = createRentalDTO.CustomerId,
            StartDate = createRentalDTO.StartDate,
            EndDate = createRentalDTO.EndDate,
            TotalAmount = totalPrice,
            PaidAmount = 0,
            Description = "Yeni Kiralama"
        };

        await _unitOfWork.GetRepository<CustomerAccount>().AddAsync(customerAccount);
        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Kiralama başarıyla oluşturuldu.", StatusCodes.Status201Created);
    }


    public async Task<ResponseDTO<string>> UpdateRentalAsync(int rentalId, UpdateRentalDTO updateRentalDTO)
    {
        var rental = await _unitOfWork.GetRepository<Rental>().GetByIdAsync(rentalId);
        if (rental == null)
        {
            return ResponseDTO<string>.Fail("Kiralama bulunamadı.", StatusCodes.Status404NotFound);
        }

     
        rental.StartDate = updateRentalDTO.StartDate;
        rental.EndDate = updateRentalDTO.EndDate ?? rental.EndDate; 

        decimal totalPrice = 0;
        foreach (var item in updateRentalDTO.RentalItems)
        {
            var rentalItem = rental.RentalItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (rentalItem != null)
            {
                rentalItem.Quantity = item.Quantity;
                rentalItem.MonthlyPrice = item.MonthlyPrice;

              
                decimal itemTotalPrice = rentalItem.MonthlyPrice * rentalItem.Quantity;


                if (updateRentalDTO.VATRate.HasValue)
                {
                    decimal vatAmount = itemTotalPrice * updateRentalDTO.VATRate.Value / 100;
                    itemTotalPrice += vatAmount;
                }

                totalPrice += itemTotalPrice;  
            }
            else
            {

                var newRentalItem = new RentalItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    MonthlyPrice = item.MonthlyPrice,
                    RentalId = rentalId
                };
                rental.RentalItems.Add(newRentalItem);

                decimal itemTotalPrice = newRentalItem.MonthlyPrice * newRentalItem.Quantity;

       
                if (updateRentalDTO.VATRate.HasValue)
                {
                    decimal vatAmount = itemTotalPrice * updateRentalDTO.VATRate.Value / 100;
                    itemTotalPrice += vatAmount;
                }

                totalPrice += itemTotalPrice;
            }
        }


        rental.TotalPrice = totalPrice;

        await _unitOfWork.SaveChangesAsync();

        var customerAccount = await _unitOfWork.GetRepository<CustomerAccount>()
                                          .FindAsync(c => c.CustomerId == rental.CustomerId
                                                             && c.StartDate <= rental.EndDate
                                                             && c.EndDate >= rental.StartDate);

        if (customerAccount != null)
        {
            customerAccount.TotalAmount = rental.TotalPrice;
            await _unitOfWork.SaveChangesAsync();
        }

        return ResponseDTO<string>.Success("Kiralama başarıyla güncellendi.", StatusCodes.Status200OK);
    }
    public async Task<ResponseDTO<List<RentalDTO>>> GetAllRentalsAsync()
    {
        var rentals = await _unitOfWork.GetRepository<Rental>().GetAllAsync();

        if (rentals == null || !rentals.Any())
        {
            return ResponseDTO<List<RentalDTO>>.Fail("Kiralama bulunamadı.", StatusCodes.Status404NotFound);
        }

        var rentalDTOs = _mapper.Map<List<RentalDTO>>(rentals);

        return ResponseDTO<List<RentalDTO>>.Success(rentalDTOs, StatusCodes.Status200OK);
    }

    public async Task<ResponseDTO<RentalDTO>> GetRentalByIdAsync(int rentalId)
    {
        var rental = await _unitOfWork.GetRepository<Rental>().GetByIdAsync(rentalId);

        if (rental == null)
        {
            return ResponseDTO<RentalDTO>.Fail("Kiralama bulunamadı.", StatusCodes.Status404NotFound);
        }

        var rentalDTO = _mapper.Map<RentalDTO>(rental);

        return ResponseDTO<RentalDTO>.Success(rentalDTO, StatusCodes.Status200OK);
    }
}






