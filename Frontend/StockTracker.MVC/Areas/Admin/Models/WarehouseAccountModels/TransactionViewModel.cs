namespace StockTracker.MVC.Areas.Admin.Models.WarehouseAccountModels
{
    public class TransactionViewModel
    {
        public IEnumerable<IncomingTransactionModel> IncomingTransactions { get; set; }
        public IEnumerable<OutgoingTransactionModel> OutgoingTransactions { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
