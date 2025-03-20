using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<CustomerAccount> _account;
        private readonly IMapper _mapper;

        public CustomerAccountService(IUnitOfWork unitOfWork, IGenericRepository<CustomerAccount> account, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _account = account;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<string>> CreateCustomerAccountAsync(CreateCustomerAccountDTO createCustomerAccountDTO)
        {
            var customerAccount = new CustomerAccount
            {
                CustomerId = createCustomerAccountDTO.CustomerId,
                PaidAmount = createCustomerAccountDTO.PaidAmount,
                Description = createCustomerAccountDTO.Description,
                StartDate = DateTime.Now,  
                EndDate = DateTime.Now.AddMonths(1) 
            };

            await _unitOfWork.GetRepository<CustomerAccount>().AddAsync(customerAccount);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDTO<string>.Success("Müşteri hesabı başarıyla oluşturuldu.", StatusCodes.Status201Created);
        }

        public async Task<ResponseDTO<UpdateCustomerAccountDTO>> UpdateCustomerAccountAsync(int id, UpdateCustomerAccountDTO updateCustomerAccountDTO)
        {
            var customerAccount = await _unitOfWork.GetRepository<CustomerAccount>().GetByIdAsync(id);

            if (customerAccount == null)
            {
                return ResponseDTO<UpdateCustomerAccountDTO>.Fail("Müşteri hesabı bulunamadı.", StatusCodes.Status404NotFound);
            }

       
            if (updateCustomerAccountDTO.PaidAmount > 0)
            {
                customerAccount.PaidAmount += updateCustomerAccountDTO.PaidAmount;

            
                if (customerAccount.PaidAmount > customerAccount.TotalAmount)
                {
                    return ResponseDTO<UpdateCustomerAccountDTO>.Fail("Ödenen tutar toplam borcu aşamaz.", StatusCodes.Status400BadRequest);
                }
            }

     
            if (updateCustomerAccountDTO.TotalAmount > 0)
            {
                customerAccount.TotalAmount = updateCustomerAccountDTO.TotalAmount;
            }

            await _unitOfWork.SaveChangesAsync();

    
            var updatedDTO = new UpdateCustomerAccountDTO
            {
                Id = customerAccount.Id,
                CustomerId = customerAccount.CustomerId,
                StartDate = customerAccount.StartDate,
                EndDate = customerAccount.EndDate,
                TotalAmount = customerAccount.TotalAmount,
                PaidAmount = customerAccount.PaidAmount,
       
                Description = customerAccount.Description
            };

            return ResponseDTO<UpdateCustomerAccountDTO>.Success(updatedDTO, StatusCodes.Status200OK);
        }




 
 
        public async Task<ResponseDTO<string>> DeleteCustomerAccountAsync(int id)
        {
            var customerAccount = await _unitOfWork.GetRepository<CustomerAccount>().GetByIdAsync(id);
            if (customerAccount == null)
            {
                return ResponseDTO<string>.Fail("Müşteri hesabı bulunamadı.", StatusCodes.Status404NotFound);
            }

            _unitOfWork.GetRepository<CustomerAccount>().Delete(customerAccount);
            await _unitOfWork.SaveChangesAsync();

            return ResponseDTO<string>.Success("Müşteri hesabı başarıyla silindi.", StatusCodes.Status200OK);
        }

        public async Task<ResponseDTO<CustomerAccountDTO>> GetCustomerAccountByIdAsync(int id)
        {
            var customerAccount = await _unitOfWork.GetRepository<CustomerAccount>()
                                                    .GetByIdAsync(id);

            if (customerAccount == null)
            {
                return ResponseDTO<CustomerAccountDTO>.Fail("Müşteri hesabı bulunamadı.", StatusCodes.Status404NotFound);
            }

            var customerAccountDTO = new CustomerAccountDTO
            {
                Id = customerAccount.Id,
                CustomerId = customerAccount.CustomerId,
                CustomerName = customerAccount.Customer?.Name,
                StartDate = customerAccount.StartDate,
                EndDate = customerAccount.EndDate,
                TotalAmount = customerAccount.TotalAmount,
                PaidAmount = customerAccount.PaidAmount,
                Description = customerAccount.Description
            };

            return ResponseDTO<CustomerAccountDTO>.Success(customerAccountDTO, StatusCodes.Status200OK);
        }


        public async Task<ResponseDTO<List<CustomerAccountDTO>>> GetAllCustomerAccountsAsync()
        {
            var customerAccounts = await _unitOfWork.GetRepository<CustomerAccount>().GetAllAsync();

            if (customerAccounts == null)
            {
                return ResponseDTO<List<CustomerAccountDTO>>.Fail("Müşteri hesapları bulunamadı.", StatusCodes.Status404NotFound);
            }

            var customerAccountDTOs = customerAccounts.Select(account => new CustomerAccountDTO
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                CustomerName = account.Customer?.Name, 
                StartDate = account.StartDate,
                EndDate = account.EndDate,
                TotalAmount = account.TotalAmount,
                PaidAmount = account.PaidAmount,
                Description = account.Description
            }).ToList();

            return ResponseDTO<List<CustomerAccountDTO>>.Success(customerAccountDTOs, StatusCodes.Status200OK);
        }


        public async Task<ResponseDTO<IEnumerable<CustomerAccountDTO>>> GetCustomerAccountsAsync(int customerId)
        {
            var customerAccounts = await _unitOfWork.GetRepository<CustomerAccount>()
        .GetAllAsync(
            r => r.CustomerId == customerId,
            includes: q => q.Include(ca => ca.Customer)
        );




            if (customerAccounts == null || !customerAccounts.Any())
            {
                return ResponseDTO<IEnumerable<CustomerAccountDTO>>.Fail("Bu müşteriye ait cari işlemler bulunamadı", StatusCodes.Status404NotFound);
            }
            var customerAccountDTOs = _mapper.Map<IEnumerable<CustomerAccountDTO>>(customerAccounts);

            return ResponseDTO<IEnumerable<CustomerAccountDTO>>.Success(customerAccountDTOs, StatusCodes.Status200OK

                  );
        }

    }
}
