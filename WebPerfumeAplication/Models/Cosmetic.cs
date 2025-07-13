using System.ComponentModel.DataAnnotations;

namespace WebCosmeticApp.Models
{
    public class Cosmetic
    {
    
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }

        [Display(Name = "Picture URL")]
        public string Picture { get; set; }

        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }
        
    }
}
