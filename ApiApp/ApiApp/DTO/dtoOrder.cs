namespace ApiApp.DTO
{
    public class dtoOrder
    {
        public int orderId { get; set; }
        public DateTime Orderdate { get; set; }
        public virtual ICollection<dtoOrderItem> orderItems { get; set; }=new List<dtoOrderItem>();
    }

    public class dtoOrderItem
    {
        public int itemId { get; set;}
        public string ItemName { get; set; }
        public decimal price { get; set; }
        public double quantity { get; set; }
    }
}
