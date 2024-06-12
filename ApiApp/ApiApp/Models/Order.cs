using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Createdate { get; set; }
  
        public virtual ICollection<OrderItem> orderItems { get; set; }
    }
}
