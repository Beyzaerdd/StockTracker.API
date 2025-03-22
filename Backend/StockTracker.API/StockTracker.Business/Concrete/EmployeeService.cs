using AutoMapper;
using Microsoft.AspNetCore.Http;
using StockTracker.Business.Abstract;
using StockTracker.Data.Abstract;
using StockTracker.Entity.Concrete;
using StockTracker.Shared.DTOs.EmployeeDTOs;
using StockTracker.Shared.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker.Business.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Employee> _employee;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IGenericRepository<Employee> employee, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _employee = employee;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<EmployeeDTO>> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO)
        {
           var employee= _mapper.Map<Employee>(createEmployeeDTO);

            if (employee != null) {
                await _employee.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                return ResponseDTO<EmployeeDTO>.Success(_mapper.Map<EmployeeDTO>(employee), StatusCodes.Status201Created);
            }

            else
            {

                return ResponseDTO<EmployeeDTO>.Fail("Çalışan oluşturulamadı", StatusCodes.Status400BadRequest);
            }

        

        }

        public async Task<ResponseDTO<string>> DeleteEmployeeAsync(int id)
        {
       
            var employee =await  _employee.GetByIdAsync(id);
            if (employee != null) {
                _employee.Delete(employee);
                _unitOfWork.SaveChangesAsync();
                return ResponseDTO<string>.Success("Çalışan başarıyla silindi", StatusCodes.Status200OK);
            }
            return ResponseDTO<string>.Fail("Çalışan bulunamadı", StatusCodes.Status404NotFound);
        }

        public async Task<ResponseDTO<IEnumerable<EmployeeDTO>>> GetAllEmployeesAsync(int? take=null)
        {
           var employee= await _employee.GetAllAsync(null, orderBy: query => query.OrderByDescending(x => x.CreatedAt),
                take: take);
            if (employee != null) {
                var employeeDTO = _mapper.Map<IEnumerable<EmployeeDTO>>(employee);
                return ResponseDTO<IEnumerable<EmployeeDTO>>.Success(employeeDTO, StatusCodes.Status200OK);
            }
            return ResponseDTO<IEnumerable<EmployeeDTO>>.Fail("Çalışan bulunamadı", StatusCodes.Status404NotFound);
        }

        public async Task<ResponseDTO<EmployeeDTO>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employee.GetByIdAsync(id);
            if (employee != null)
            {
                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                return ResponseDTO<EmployeeDTO>.Success(employeeDTO, StatusCodes.Status200OK);
            }
            return ResponseDTO<EmployeeDTO>.Fail("Çalışan bulunamadı", StatusCodes.Status404NotFound);
        }

        public async Task<ResponseDTO<UpdateEmployeeDTO>> UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO)
        {
           var employee = await _employee.GetByIdAsync(updateEmployeeDTO.Id);
            if (employee != null) {
                _mapper.Map(updateEmployeeDTO, employee);
                 _employee.Update(employee);
                employee.UpdatedAt = DateTime.Now;
                await _unitOfWork.SaveChangesAsync();
                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                return ResponseDTO<UpdateEmployeeDTO>.Success(_mapper.Map<UpdateEmployeeDTO>(employee), StatusCodes.Status200OK);
            }
            return ResponseDTO<UpdateEmployeeDTO>.Fail("Çalışan bulunamadı", StatusCodes.Status404NotFound);

        }
    }
}
