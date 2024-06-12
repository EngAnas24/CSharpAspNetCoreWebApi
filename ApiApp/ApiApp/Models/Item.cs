using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiApp.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        public string? Notes { get; set; }

        public byte[]? Image { get; set; }

        [ForeignKey(nameof(category))]
        public int CategoryId { get; set; }
      
        public virtual Category category { get; set; }
     
        public virtual ICollection<OrderItem> orderItems { get; set; }

    }
}
