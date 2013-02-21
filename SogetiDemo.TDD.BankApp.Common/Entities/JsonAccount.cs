namespace SogetiDemo.TDD.BankApp.Common.Entities
{
    public class JsonAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(ErrorMessage); }
        }
    }
}
