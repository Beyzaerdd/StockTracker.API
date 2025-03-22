using Microsoft.AspNetCore.Mvc;
using NToastNotify;

using StockTracker.MVC.Areas.Admin.Models.CustomerAccountModels;
using StockTracker.MVC.Areas.Admin.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTracker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerAccountController : Controller
    {
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IToastNotification _toaster;

        public CustomerAccountController(ICustomerAccountService customerAccountService, IToastNotification toaster)
        {
            _customerAccountService = customerAccountService;
            _toaster = toaster;
        }


        public async Task<IActionResult> Index(int? take)
        {
      
            int accountCount = take ?? 11;

            var response = await _customerAccountService.GetAllCustomerAccountsAsync(accountCount);

            if (response.success)
            {
                return View(response.Data);
            }

            _toaster.AddErrorToastMessage("Müşteri hesapları alınırken bir hata oluştu.");
            return View(new List<CustomerAccountModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _customerAccountService.GetCustomerAccountByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Hesap bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerAccountModel createCustomerAccountModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerAccountService.CreateCustomerAccountAsync(createCustomerAccountModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Hesap başarıyla oluşturuldu.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Hesap oluşturulurken bir hata oluştu.");
                }
            }

            return View(createCustomerAccountModel);
        }

   
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customerAccountService.GetCustomerAccountByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Hesap bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            var updateCustomerAccountModel = new UpdateCustomerAccountModel
            {
                Id = response.Data.Id,
                CustomerId = response.Data.CustomerId,
                StartDate = response.Data.StartDate,
                EndDate = response.Data.EndDate,
                TotalAmount = response.Data.TotalAmount,
                PaidAmount = response.Data.PaidAmount,
                Description = response.Data.Description
            };

            return View(updateCustomerAccountModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCustomerAccountModel updateCustomerAccountModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerAccountService.UpdateCustomerAccountAsync(updateCustomerAccountModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Hesap başarıyla güncellendi.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Hesap güncellenirken bir hata oluştu.");
                }
            }

            return View(updateCustomerAccountModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var response = await _customerAccountService.GetCustomerAccountByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Hesap bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _customerAccountService.DeleteCustomerAccountAsync(id);

            if (response.success)
            {
                _toaster.AddSuccessToastMessage("Hesap başarıyla silindi.");
                return Json(new { success = true });
            }
            else
            {
                _toaster.AddErrorToastMessage("Hesap silinirken bir hata oluştu.");
                return Json(new { success = false });
            }
        }
    }
}
