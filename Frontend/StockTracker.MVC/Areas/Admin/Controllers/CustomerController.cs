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

        // GET: Admin/Customer
        // GET: Admin/Customer
        public async Task<IActionResult> Index()
        {
            var response = await _customerService.GetAllCustomerAsync();

            if (response.success)
            {
                return View(response.Data); // Müşteri listesine ulaşınca view'a gönderiyoruz
            }

            _toaster.AddErrorToastMessage("Müşteri listesi alınırken bir hata oluştu.");
            return View(new List<CustomerModel>()); // Hata durumunda boş bir liste döndürüyoruz
        }


        // GET: Admin/Customer/Details/5
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

        // GET: Admin/Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customer/Create
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

        // GET: Admin/Customer/Edit/5
        // GET: Admin/Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Müşteri bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            // Müşteri verilerini doldurarak UpdateCustomerModel'e atıyoruz
            var updateCustomerModel = new UpdateCustomerModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                LastName = response.Data.LastName,
                Email = response.Data.Email,
                Phone = response.Data.Phone,
                Address = response.Data.Address
            };

            return View(updateCustomerModel); // Formu bu veri ile döndürüyoruz
        }

        // POST: Admin/Customer/Edit/5
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

        // GET: Admin/Customer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _customerService.GetCustomerByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Müşteri bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data); // View'da müşteri bilgilerini göstermek için
        }

        // POST: Admin/Customer/Delete/5
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

            return RedirectToAction(nameof(Index)); // Başarıyla silindiğinde listeye geri yönlendir
        }

    }
}

