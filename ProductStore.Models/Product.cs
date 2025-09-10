using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        [DisplayName("Price for 1-49")]
        public double Price { get; set; }
        [Required]
        [DisplayName("Price for 50-99")]
        public double Price50 { get; set; }
        [Required]
        [DisplayName("Price for 100+")]
        public double Price100 { get; set; }
    }
}
