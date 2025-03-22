using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using StockTracker.MVC.Areas.Admin.Services.Abstract;
using StockTracker.MVC.Areas.Admin.Models.EmployeeModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using StockTracker.MVC.Areas.Admin.Models.ProductModels;

namespace StockTracker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IToastNotification _toaster;

        public EmployeeController(IEmployeeService employeeService, IToastNotification toaster)
        {
            _employeeService = employeeService;
            _toaster = toaster;
        }

        public async Task<IActionResult> Index(int? take)
        {
            int employeeCount = take ?? 11;
            var response = await _employeeService.GetAllEmployeesAsync(employeeCount);

            if (response.success)
            {
                return View(response.Data);
            }

            _toaster.AddErrorToastMessage("Çalışan listesi alınırken bir hata oluştu.");
            return View(new List<EmployeeModel>());
        }
     

        public async Task<IActionResult> Details(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Çalışan bilgileri alınırken bir hata oluştu.");
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
        public async Task<IActionResult> Create(CreateEmployeeModel createEmployeeModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.CreateEmployeeAsync(createEmployeeModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Çalışan başarıyla oluşturuldu.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Çalışan oluşturulurken bir hata oluştu.");
                }
            }

            return View(createEmployeeModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Çalışan bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            var updateEmployeeModel = new UpdateEmployeeModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                LastName = response.Data.LastName,
                StartDate = response.Data.StartDate,

                EndDate = response.Data.EndDate,
                Salary = response.Data.Salary,
                Phone = response.Data.Phone,
                Position = response.Data.Position
            };

            return View(updateEmployeeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateEmployeeModel updateEmployeeModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeService.UpdateEmployeeAsync(updateEmployeeModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Çalışan başarıyla güncellendi.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Çalışan güncellenirken bir hata oluştu.");
                }
            }

            return View(updateEmployeeModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Çalışan bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }


        [HttpPost, ActionName("Delete")]
 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);

            if (response.success)
            {
                _toaster.AddSuccessToastMessage("Çalışan başarıyla silindi.");
                return Json(new { success = true });  
            }
            else
            {
                _toaster.AddErrorToastMessage("Çalışan silinirken bir hata oluştu.");
                return Json(new { success = false });  
            }
        }
    }
}