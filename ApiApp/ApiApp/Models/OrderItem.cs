using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ApiApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        [ForeignKey(nameof(orders))]
        public int OrderId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Order? orders { get; set; }

        [ForeignKey(nameof(Items))]
        public int ItemId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Item? Items { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
