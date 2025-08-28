using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(24)]
        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
