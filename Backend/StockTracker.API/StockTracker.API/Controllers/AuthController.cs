using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Abstract;
using StockTracker.Shared.DTOs.EmployeeDTOs;
using StockTracker.Shared.Helpers;


namespace StockTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginEmployeeDTO loginEmployeeDTO)
        {
            var response = await _authService.LoginAsync(loginEmployeeDTO);
            return CreateResponse(response);
        }
    }
}
