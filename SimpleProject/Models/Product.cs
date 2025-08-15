using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Remote("IsProductNameExist", "Product",HttpMethod="POST", ErrorMessage ="Name Is Already Exist")]
        public string Name { get; set; }
        [Range(1,double.MaxValue,ErrorMessage ="min value 1 max value 5000")]
        public decimal Price { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; } 

        public string? Path { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

    }
}
