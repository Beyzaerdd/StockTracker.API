using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using StockTracker.MVC.Areas.Admin.Models.AuthModel;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StockTracker.MVC.Areas.Admin.Services.Abstract;

namespace StockTracker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IToastNotification _toaster;
        public AuthController(IAuthService authService, IToastNotification toaster)
        {
            _authService = authService;
            _toaster = toaster;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LoginUser()
        {
            return View("~/Areas/Admin/Views/Auth/LoginUser.cshtml");

        }


        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserModel loginUserModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Lütfen tüm alanları doğru şekilde doldurduğunuzdan emin olun.");
                return View(loginUserModel);
            }

            try
            {
                var response = await _authService.LoginUserAsync(loginUserModel);

                if (response.IsSucceeded && response.Data?.AccessToken != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(response.Data.AccessToken);

                    var userName = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                    var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userId))
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userName),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, role ?? string.Empty),
                    new Claim("AccessToken", response.Data.AccessToken)
                };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                        {
                            ExpiresUtc = response.Data.ExpirationDate,
                            IsPersistent = true
                        });

                        _toaster.AddSuccessToastMessage("Hoşgeldiniz! Giriş işlemi başarıyla tamamlandı.");

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }

                var errorMessage = returnUrl != null ? "Giriş işlemi başarısız. Lütfen tekrar deneyin." : "Giriş işlemi başarısız. Lütfen tekrar deneyin.";

                _toaster.AddErrorToastMessage(errorMessage);
                ModelState.AddModelError(string.Empty, errorMessage);

                return View(loginUserModel);
            }
            catch (Exception ex)
            {
                _toaster.AddErrorToastMessage($"Bir hata oluştu: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Bir hata oluştu, lütfen daha sonra tekrar deneyin.");
                return View(loginUserModel);
            }
        }

    }
}
