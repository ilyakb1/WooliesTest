namespace WooliesTest.Models
{
    public class Special
    {
        public ProductQuantity[] Quantities { get; set; }

        public decimal Total { get; set; }

        public override string ToString()
        {
            return $"Special amount: {Total}";
        }
    }
}
