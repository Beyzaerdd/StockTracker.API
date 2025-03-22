using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.EmployeeDTOs;
using StockTracker.Shared.Helpers;

namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : CustomControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("allEmployees")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int? take = null)
        {
            var response = await _employeeService.GetAllEmployeesAsync(take);
            return CreateResponse(response);
        }

        [HttpGet("getEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            return CreateResponse(response);
        }

        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            var response = await _employeeService.CreateEmployeeAsync(createEmployeeDTO);
            return CreateResponse(response);
        }

        [HttpPut("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO)
        {
            var response = await _employeeService.UpdateEmployeeAsync(updateEmployeeDTO);
            return CreateResponse(response);
        }

        [HttpDelete("deleteEmployee/{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);
            return CreateResponse(response);
        }

    }
}
