using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using StockTracker.MVC.Areas.Admin.Services.Abstract;
using StockTracker.MVC.Areas.Admin.Models.ProductModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StockTracker.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IToastNotification _toaster;

        public ProductController(IProductService productService, IToastNotification toaster)
        {
            _productService = productService;
            _toaster = toaster;
        }

        public async Task<IActionResult> Index(int? take)
        {
            // Varsayılan olarak 11 ürün gösterilsin.
            int productCount = take ?? 11;

            var response = await _productService.GetAllProductsAsync(productCount);

            if (response.success)
            {
                return View(response.Data);
            }

            _toaster.AddErrorToastMessage("Ürün listesi alınırken bir hata oluştu.");
            return View(new List<ProductModel>());
        }


        public async Task<IActionResult> Details(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Ürün bilgileri alınırken bir hata oluştu.");
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
        public async Task<IActionResult> Create(CreateProductModel createProductModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(createProductModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Ürün başarıyla oluşturuldu.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Ürün oluşturulurken bir hata oluştu.");
                }
            }

            return View(createProductModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Ürün bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            var updateProductModel = new UpdateProductModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                Description = response.Data.Description,
                MonthlyPrice = response.Data.MonthlyPrice,
                StockQuantity = response.Data.StockQuantity,
                Quantity = response.Data.Quantity

            };

            return View(updateProductModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductModel updateProductModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync(updateProductModel);

                if (response.success)
                {
                    _toaster.AddSuccessToastMessage("Ürün başarıyla güncellendi.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toaster.AddErrorToastMessage("Ürün güncellenirken bir hata oluştu.");
                }
            }

            return View(updateProductModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);

            if (response == null || !response.success)
            {
                _toaster.AddErrorToastMessage("Ürün bilgileri alınırken bir hata oluştu.");
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _productService.DeleteProductAsync(id);

            if (response.success)
            {
                _toaster.AddSuccessToastMessage("Ürün başarıyla silindi.");
                return Json(new { success = true });
            }
            else
            {
                _toaster.AddErrorToastMessage("Ürün silinirken bir hata oluştu.");
                return Json(new { success = false });
            }
        }
        public async Task<IActionResult> ProductStockInfo()
        {
          
            var response = await _productService.GetProductStockInfoAsync();

            if (response.success)
            {
               
                return View(response.Data);
            }

            _toaster.AddErrorToastMessage("Stok bilgileri alınırken bir hata oluştu.");
            return View(new List<ProductStockInfoModel>());
        }
    }
}
