using AutoMapper;
using Microsoft.AspNetCore.Http;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.DTOs.CustomerDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class CustomerService : ICustomerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper, IGenericRepository<Customer> customerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO<CreateCustomerDTO>> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            var customer = _mapper.Map<Customer>(createCustomerDTO);

            if (customer == null) {
                return ResponseDTO<CreateCustomerDTO>.Fail("Müşteri oluşturulamadı", StatusCodes.Status400BadRequest);
            }

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDTO<CreateCustomerDTO>.Success(_mapper.Map<CreateCustomerDTO>(customer), StatusCodes.Status201Created);


        }

        public async Task<ResponseDTO<CustomerDTO>> DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer != null) {
                _customerRepository.Delete(customer);
               await _unitOfWork.SaveChangesAsync();
                return ResponseDTO<CustomerDTO>.Success(_mapper.Map<CustomerDTO>(customer), StatusCodes.Status200OK);
            }
            return ResponseDTO<CustomerDTO>.Fail("Müşteri bulunamadı", StatusCodes.Status404NotFound);
        }

        public async Task<ResponseDTO<IEnumerable<CustomerDTO>>> GetAllCustomerAsync(int? take=null)
        {

            var customers=await _customerRepository.GetAllAsync(null, orderBy: query => query.OrderByDescending(x => x.CreatedAt),
                take: take);
            if (customers == null)
            {
                return ResponseDTO<IEnumerable<CustomerDTO>>.Fail("Müşteriler bulunamadı", StatusCodes.Status404NotFound);
            }
            return ResponseDTO<IEnumerable<CustomerDTO>>.Success(_mapper.Map<IEnumerable<CustomerDTO>>(customers), StatusCodes.Status200OK);



        }

        public async Task<ResponseDTO<CustomerDTO>> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return ResponseDTO<CustomerDTO>.Fail("Müşteri bulunamadı", StatusCodes.Status404NotFound);
            }
            return ResponseDTO<CustomerDTO>.Success(_mapper.Map<CustomerDTO>(customer), StatusCodes.Status200OK);

        }

        public async Task<ResponseDTO<UpdateCustomerDTO>> UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO)
        {
            
            var customer = _mapper.Map<Customer>(updateCustomerDTO);
            if (customer == null)
            {
                return ResponseDTO<UpdateCustomerDTO>.Fail("Müşteri bulunamadı", StatusCodes.Status404NotFound);
            }
            _customerRepository.Update(customer);
            customer.UpdatedAt = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
            return ResponseDTO<UpdateCustomerDTO>.Success(_mapper.Map<UpdateCustomerDTO>(customer), StatusCodes.Status200OK);

        }

       
    }
}
