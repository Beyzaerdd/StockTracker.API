using StockTracker.MVC.Areas.Admin.Models.ProductModels;
using StockTracker.MVC.Areas.Admin.Models.RemainingProductModels;
using StockTracker.MVC.Areas.Admin.Models.ReturnedProductModels;



    namespace StockTracker.MVC.Areas.Admin.Models.RentalModels
    {
        public class RentalDetailsViewModel
        {
            public RentalModel Rental { get; set; }
            public IEnumerable<RemainingProductModel> RemainingProducts { get; set; }
            public IEnumerable<ReturnedProductModel> ReturnedProducts { get; set; }
            public IEnumerable<ProductModel> Products { get; set; }
        }
    }

