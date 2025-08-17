using Microsoft.AspNetCore.Mvc;
using SimpleProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProject.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        [Remote("IsProductNameExist", "Product", HttpMethod = "POST", ErrorMessage = "Name Is Already Exist")]
        public string Name { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "min value 1 max value 5000")]
        public decimal Price { get; set; }
        [NotMapped]
        public List<IFormFile>? Files { get; set; }
        public int CategoryId { get; set; }

    }
}
