using System.ComponentModel.DataAnnotations;

namespace ConsumingWebAPIs.Models
{
    public class Product
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

    }
}
