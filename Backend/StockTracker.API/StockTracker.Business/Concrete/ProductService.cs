using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<ProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var product = _mapper.Map<Product>(createProductDTO);
            await _productRepository.AddAsync(product);

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

        public async Task<ResponseDTO<IEnumerable<ProductDTO>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();

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

            var updatedProductDTO = _mapper.Map<ProductDTO>(product);
            return ResponseDTO<ProductDTO>.Success(updatedProductDTO, StatusCodes.Status200OK);
        }

    }
}
