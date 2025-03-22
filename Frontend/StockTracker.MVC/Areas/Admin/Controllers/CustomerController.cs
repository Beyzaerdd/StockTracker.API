using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using StockTracker.MVC.Areas.Admin.Services.Abstract;
using StockTracker.MVC.Areas.Admin.Models.CustomerModels;


namespace StockTracker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IToastNotification _toaster;

        public CustomerController(ICustomerService customerService, IToastNotification toaster)
        {
            _customerService = customerService;
            _toaster = toaster;
        }

    
        public async Task<IActionResult> Index(int? take)
        {
            int customerCount = take ?? 11;
            var response = await _customerService.GetAllCustomerAsync(customerCount);

            if (response.success)
            {
                return View(response.Data);
            }

            _toaster.AddErrorToastMessage("Müşteri listesi alınırken bir hata oluştu.");
            return View(new List<CustomerModel>()); 
        }


  
        public async Task<IActionResult> Details(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Müşteri bilgileri alınırken bir hata oluştu.");
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
        public async Task<IActionResult> Create(CreateCustomerModel createCustomerModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.CreateCustomerAsync(createCustomerModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Müşteri başarıyla oluşturuldu.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Müşteri oluşturulurken bir hata oluştu.");
                }
            }

            return View(createCustomerModel);
        }

    
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Müşteri bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

        
            var updateCustomerModel = new UpdateCustomerModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                LastName = response.Data.LastName,
                Email = response.Data.Email,
                Phone = response.Data.Phone,
                Address = response.Data.Address
            };

            return View(updateCustomerModel); 
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCustomerModel updateCustomerModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.UpdateCustomerAsync(updateCustomerModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Müşteri başarıyla güncellendi.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Müşteri güncellenirken bir hata oluştu.");
                }
            }

            return View(updateCustomerModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Müşteri bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data); 
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _customerService.DeleteCustomerAsync(id);

            if (response.success)
            {
                _toaster.AddSuccessToastMessage("Müşteri başarıyla silindi.");
            }
            else
            {
                _toaster.AddErrorToastMessage("Müşteri silinirken bir hata oluştu.");
            }

            return RedirectToAction(nameof(Index)); 
        }

    }
}

