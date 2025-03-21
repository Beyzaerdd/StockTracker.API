using AutoMapper;
using StockTracker.Entity.Concrete;

using StockTracker.Shared.DTOs.AccountTransactionDTOs;
using StockTracker.Shared.DTOs.CustomerDTOs;
using StockTracker.Shared.DTOs.CustomerPaymentDTOs;
using StockTracker.Shared.DTOs.DeliveredItemDTOs;
using StockTracker.Shared.DTOs.EmployeeDTOs;

using StockTracker.Shared.DTOs.ProductDTOs;
using StockTracker.Shared.DTOs.RemainingItemDTOs;
using StockTracker.Shared.DTOs.RemainingProductDTOs;
using StockTracker.Shared.DTOs.RentalDTOs;
using StockTracker.Shared.DTOs.RentalItemDTOs;
using StockTracker.Shared.DTOs.ReturnedProductDTOs;

using StockTracker.Shared.DTOs.WarehouseAccountDTOs;

namespace StockTracker.Business.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {

            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();

            CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();


            CreateMap<CreateCustomerAccountDTO, Customer>().ReverseMap();
            CreateMap<UpdateCustomerAccountDTO, Customer>().ReverseMap();

         

            // Product Mapping
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();

            // Rental Mapping
            CreateMap<Rental, RentalDTO>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ReverseMap();
            CreateMap<CreateRentalDTO, Rental>();
            CreateMap<UpdateRentalDTO, Rental>();

            // RentalItem Mapping
            CreateMap<RentalItem, RentalItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();
            CreateMap<CreateRentalItemDTO, RentalItem>();
            CreateMap<UpdateRentalItemDTO, RentalItem>();

         
                CreateMap<RemainingProduct, RemainingProductDTO>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.RentalItem.ProductId))
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.RentalItem.Product.Name))
                    .ForMember(dest => dest.DaysRemaining, opt => opt.MapFrom(src => src.DaysRemaining))
                    .ForMember(dest => dest.DailyPrice, opt => opt.MapFrom(src => src.DailyPrice))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.RentalItem.Quantity));
            CreateMap<CustomerPayment, CustomerPaymentCreateDTO>().ReverseMap();
            CreateMap<CustomerPayment, CustomerPaymentUpdateDTO>().ReverseMap();
           
            CreateMap<CustomerPayment, CustomerPaymentDTO>().ReverseMap();


            // ReturnedProduct Mapping
            CreateMap<ReturnedProduct, ReturnedProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.RentalItem.Product.Name))
                .ReverseMap();
            CreateMap<CreateReturnedProductDTO, ReturnedProduct>();

            // CustomerAccount Mapping
            CreateMap<CustomerAccount, CustomerAccountDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : "Bilinmeyen Müşteri"));
            CreateMap<CreateCustomerAccountDTO, CustomerAccount>();
            CreateMap<UpdateCustomerAccountDTO, CustomerAccount>();

          

            // IncomingTransactionDTO -> IncomingTransaction
            CreateMap<CreateIncomingTransactionDTO, IncomingTransaction>().ReverseMap();
            CreateMap<UpdateIncomingTransactionDTO, IncomingTransaction>().ReverseMap();
            CreateMap<IncomingTransactionDTO, IncomingTransaction>().ReverseMap();


            // OutgoingTransactionDTO -> OutgoingTransaction
            CreateMap<CreateOutgoingTransactionDTO, OutgoingTransaction>().ReverseMap();
            CreateMap<UpdateOutgoingTransactionDTO, OutgoingTransaction>().ReverseMap();
            CreateMap<OutgoingTransactionDTO, OutgoingTransaction>().ReverseMap();

            // Employee Mapping
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<CreateEmployeeDTO, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDTO, Employee>().ReverseMap();
















        }


    }
}
