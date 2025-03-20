using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Data.Concrete.Repositories;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.DeliveredItemDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using StockTracker.Shared.DTOs.ReturnedProductDTOs;

public class ReturnedProductService : IReturnedProductService
{
    private readonly IGenericRepository<ReturnedProduct> _returnedProductRepository;
    private readonly IGenericRepository<RemainingProduct> _remainingProductRepository;
    private readonly IGenericRepository<RentalItem> _rentalItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReturnedProductService(
        IGenericRepository<ReturnedProduct> returnedProductRepository,
        IGenericRepository<RemainingProduct> remainingProductRepository,
        IGenericRepository<RentalItem> rentalItemRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _returnedProductRepository = returnedProductRepository;
        _remainingProductRepository = remainingProductRepository;
        _rentalItemRepository = rentalItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<CreateReturnedProductDTO>> CreateReturnedProductAsync(CreateReturnedProductDTO createReturnedProductDTO)
    {
  
        var rentalItem = await _rentalItemRepository.GetAsync(
            ri => ri.Id == createReturnedProductDTO.RentalItemId,
            q => q.Include(ri => ri.Rental)); 

        if (rentalItem == null || rentalItem.Rental == null)
        {
            return ResponseDTO<CreateReturnedProductDTO>.Fail("Kiralama bulunamadı.", StatusCodes.Status404NotFound);
        }

        var returnedProduct = _mapper.Map<ReturnedProduct>(createReturnedProductDTO);
        await _returnedProductRepository.AddAsync(returnedProduct);


        var remainingProduct = await _remainingProductRepository.GetAsync(rp => rp.RentalItemId == createReturnedProductDTO.RentalItemId);
        if (remainingProduct != null)
        {

            var daysUsed = (DateTime.Today - rentalItem.Rental.StartDate).Days;
            int totalReturnedQuantity = createReturnedProductDTO.QuantityReturned;
            int totalRentalQuantity = remainingProduct.RentalItem.Quantity;

        
            if (totalReturnedQuantity >= totalRentalQuantity)
            {
       
                remainingProduct.RentalItem.Quantity = 0;
                remainingProduct.DaysRemaining = 0;


                _remainingProductRepository.Delete(remainingProduct);
            }
            else
            {
              
                remainingProduct.RentalItem.Quantity -= totalReturnedQuantity;

        
                var productDays = (totalReturnedQuantity * 30) / totalRentalQuantity;

              


                remainingProduct.DaysRemaining = Math.Max(daysUsed, 0);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        return ResponseDTO<CreateReturnedProductDTO>.Success(createReturnedProductDTO, StatusCodes.Status200OK);
    }



    public async Task<ResponseDTO<IEnumerable<ReturnedProductDTO>>> GetAllReturnedProductsAsync()
    {
        var returnedProducts = await _returnedProductRepository.GetAllAsync();
        var returnedProductsDTO = _mapper.Map<IEnumerable<ReturnedProductDTO>>(returnedProducts);
        return ResponseDTO<IEnumerable<ReturnedProductDTO>>.Success(returnedProductsDTO, StatusCodes.Status200OK);
    }

    public async Task<ResponseDTO<ReturnedProductDTO>> GetReturnedProductByIdAsync(int id)
    {
        var returnedProduct = await _returnedProductRepository.GetByIdAsync(id);
        if (returnedProduct == null)
        {
            return ResponseDTO<ReturnedProductDTO>.Fail("Teslim edilen ürün bulunamadı.", StatusCodes.Status404NotFound);
        }

        var returnedProductDTO = _mapper.Map<ReturnedProductDTO>(returnedProduct);
        return ResponseDTO<ReturnedProductDTO>.Success(returnedProductDTO, StatusCodes.Status200OK);
    }

    public async Task<ResponseDTO<string>> UpdateReturnedProductAsync(UpdateReturnedProductDTO updateReturnedProduct)
    {
        var returnedProduct = await _returnedProductRepository.GetByIdAsync(updateReturnedProduct.Id);
        if (returnedProduct == null)
        {
            return ResponseDTO<string>.Fail("Teslim edilen ürün bulunamadı.", StatusCodes.Status404NotFound);
        }

        _mapper.Map(updateReturnedProduct, returnedProduct);
        _returnedProductRepository.Update(returnedProduct);
        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Teslim edilen ürün güncellendi.", StatusCodes.Status200OK);
    }

    public async Task<ResponseDTO<string>> DeleteReturnedProductAsync(int id)
    {
        var returnedProduct = await _returnedProductRepository.GetByIdAsync(id);
        if (returnedProduct == null)
        {
            return ResponseDTO<string>.Fail("Teslim edilen ürün bulunamadı.", StatusCodes.Status404NotFound);
        }

        _returnedProductRepository.Delete(returnedProduct);
        await _unitOfWork.SaveChangesAsync();

        return ResponseDTO<string>.Success("Teslim edilen ürün silindi.", StatusCodes.Status200OK);
    }
}
