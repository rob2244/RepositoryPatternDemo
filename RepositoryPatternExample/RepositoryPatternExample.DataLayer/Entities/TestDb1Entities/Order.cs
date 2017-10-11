namespace DataLayer.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual Customer Customer { get; set; }
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public double Price { get; set; }
    }
}