using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AShop_Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BrandName { get; set; }
        
    }
}
