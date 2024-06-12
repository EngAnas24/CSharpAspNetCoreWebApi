namespace MyFirstApiProject.Core
{
    public class Order
    {
        int Id { get; set; }
        public string Name { get; set; }
        public DateTime date { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }

    }
}
