using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using StockTracker.MVC.Areas.Admin.Models.CustomerPaymentModels;
using StockTracker.MVC.Areas.Admin.Services.Abstract;
using StockTracker.MVC.Services.CustomerPayments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTracker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerPaymentController : Controller
    {
        private readonly ICustomerPaymentService _customerPaymentService;
        private readonly ICustomerAccountService customerAccountService;
        private readonly IToastNotification _toaster;

        public CustomerPaymentController(ICustomerPaymentService customerPaymentService, IToastNotification toaster, ICustomerAccountService customerAccountService)
        {
            _customerPaymentService = customerPaymentService;
            _toaster = toaster;
            this.customerAccountService = customerAccountService;
        }


        public async Task<IActionResult> Index(int? id)
        {
            if (!id.HasValue)
            {
                _toaster.AddErrorToastMessage("Müşteri hesabı ID'si girilmelidir.");
                return View(new List<CustomerPaymentModel>());
            }

 
            var customerAccountResponse = await customerAccountService.GetCustomerAccountByIdAsync(id.Value);
            if (customerAccountResponse == null || !customerAccountResponse.success)
            {
                _toaster.AddErrorToastMessage("Müşteri hesabı bulunamadı.");
                return View(new List<CustomerPaymentModel>());
            }

            var customerAccount = customerAccountResponse.Data;

 
            decimal remainingAmount = customerAccount.TotalAmount - customerAccount.PaidAmount;
            decimal totalAmount = customerAccount.TotalAmount;
            decimal paidAmount = customerAccount.PaidAmount;

           
            DateTime startDate = customerAccount.StartDate;
            DateTime endDate = customerAccount.EndDate;

  
            ViewBag.TotalAmount = totalAmount;   
            ViewBag.PaidAmount = paidAmount;      
            ViewBag.RemainingAmount = remainingAmount;  
            ViewBag.StartDate = startDate.ToString("dd/MM/yyyy");  
            ViewBag.EndDate = endDate.ToString("dd/MM/yyyy");    

      
            var paymentResponse = await _customerPaymentService.GetCustomerPaymentsAsync(id.Value);

            if (paymentResponse.success)
            {
                ViewBag.CustomerAccountId = id;
                return View(paymentResponse.Data);
            }

            _toaster.AddErrorToastMessage("Ödeme geçmişi alınırken bir hata oluştu.");
            return View(new List<CustomerPaymentModel>());
        }






        [HttpGet]
        public IActionResult ReceivePayment(int id)
        {
            var model = new CustomerPaymentCreateModel
            {
                CustomerAccountId = id 
            };
            return View(model);
        }

      
       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ReceivePayment(int id, CustomerPaymentCreateModel model)
{
    
    model.CustomerAccountId = id; 

    if (ModelState.IsValid)
    {
        var response = await _customerPaymentService.ReceivePaymentAsync(model);

        if (response.success)
        {
            _toaster.AddSuccessToastMessage("Ödeme başarıyla alındı.");
            return RedirectToAction(nameof(Index), new { id = model.CustomerAccountId }); 
        }
        else
        {
            _toaster.AddErrorToastMessage("Ödeme alınırken bir hata oluştu.");
        }
    }

    return View(model);
}

    }
}
