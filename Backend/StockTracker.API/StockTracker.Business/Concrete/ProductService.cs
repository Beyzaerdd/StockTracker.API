using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.ProductDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<RentalItem> _rentalRepository;
        private readonly IGenericRepository<RemainingProduct> _remainingProductRepository;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> productRepository, IMapper mapper, IGenericRepository<RentalItem> rentalRepository, IGenericRepository<RemainingProduct> remainingProductRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
            _rentalRepository = rentalRepository;
            _remainingProductRepository = remainingProductRepository;
        }

        public async Task<ResponseDTO<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var product = _mapper.Map<Product>(createProductDTO);
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            if (product == null)
            {
                return ResponseDTO<ProductDTO>.Fail("Ürün oluşturulamadı", StatusCodes.Status400BadRequest);
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return ResponseDTO<ProductDTO>.Success(productDTO, StatusCodes.Status201Created);
        }

        public async Task<ResponseDTO<string>> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ResponseDTO<string>.Fail("Ürün bulunamadı", StatusCodes.Status404NotFound);
            }

             _productRepository.Delete(product);
            return ResponseDTO<string>.Success("Ürün başarıyla silindi", StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<IEnumerable<ProductDTO>>> GetAllProductsAsync(int? take=null)
        {
            var products = await _productRepository.GetAllAsync(null, orderBy: query => query.OrderByDescending(x => x.CreatedAt),
                take: take);

            if (products == null || !products.Any())
            {
                return ResponseDTO<IEnumerable<ProductDTO>>.Fail("Hiç ürün bulunamadı", StatusCodes.Status404NotFound);
            }

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return ResponseDTO<IEnumerable<ProductDTO>>.Success(productDTOs, StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<ProductDTO>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ResponseDTO<ProductDTO>.Fail("Ürün bulunamadı", StatusCodes.Status404NotFound);
            }

            var productDTO = _mapper.Map<ProductDTO>(product);
            return ResponseDTO<ProductDTO>.Success(productDTO, StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<ProductDTO>> UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            var product = await _productRepository.GetByIdAsync(updateProductDTO.Id);

            if (product == null)
            {
                return ResponseDTO<ProductDTO>.Fail("Ürün bulunamadı", StatusCodes.Status404NotFound);
            }

            _mapper.Map(updateProductDTO, product);
             _productRepository.Update(product);
            product.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            var updatedProductDTO = _mapper.Map<ProductDTO>(product);
            return ResponseDTO<ProductDTO>.Success(updatedProductDTO, StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<List<ProductStockInfoDTO>>> GetProductStockInfoAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productStockInfoList = new List<ProductStockInfoDTO>();

            foreach (var product in products)
            {
                // 1. Mevcut stok miktarı ve quantity
                int stockQuantity = product.StockQuantity;
                int productQuantity = product.Quantity;

                // 2. Kiralanmış ürünlerin miktarını hesaplayalım
                var rentedItems = await _rentalRepository
                    .GetAllAsync(ri => ri.ProductId == product.Id && ri.Rental.EndDate >= DateTime.Now);

                // Debugging: rentedItems kontrolü
                Console.WriteLine($"Product: {product.Name}, Rented Items Count: {rentedItems.Count()}");

                int rentedQuantity = rentedItems.Sum(ri => ri?.Quantity ?? 0); // Kiralanan toplam miktar
                Console.WriteLine($"Product: {product.Name}, Rented Quantity: {rentedQuantity}");

                // 3. Kalan ürünlerin miktarını hesaplayalım
                var remainingProducts = await _remainingProductRepository
                    .GetAllAsync(rp => rp.RentalItem.ProductId == product.Id && rp.DaysRemaining > 0);

                // Debugging: remainingProducts kontrolü
                Console.WriteLine($"Product: {product.Name}, Remaining Products Count: {remainingProducts.Count()}");

                int remainingQuantity = remainingProducts.Sum(rp => rp?.RentalItem?.Quantity ?? 0); // Geri alınmamış ürünler
                Console.WriteLine($"Product: {product.Name}, Remaining Quantity: {remainingQuantity}");

                // 4. Bilgileri DTO'ya ekleyelim
                productStockInfoList.Add(new ProductStockInfoDTO
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    StockQuantity = stockQuantity,  // Mevcut stok
                    RentedQuantity = rentedQuantity,  // Kiralanan miktar
                    RemainingQuantity = remainingQuantity,  // Geri alınmayan ürün miktarı
                    Quantity = productQuantity  // Diğer quantity bilgisi
                });
            }

            // 5. Sonuçları döndürelim
            return ResponseDTO<List<ProductStockInfoDTO>>.Success(productStockInfoList, StatusCodes.Status200OK);
        }






    }
}
