using System.ComponentModel.DataAnnotations;

namespace ApiApp.Models
{
    public class mlItem
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        public string? Notes { get; set; }

        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }

    }
}
