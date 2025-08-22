using Microsoft.AspNetCore.Mvc;
using SimpleProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProject.ViewModels.Products
{
    public class AddProductViewModel
    {
        [Required (ErrorMessage ="NameIsRequired")]
        [Remote("IsProductNameArExist", "Product", HttpMethod = "POST", ErrorMessage = "Name Ar Is Already Exist")]
        public string NameAr { get; set; }
        [Required(ErrorMessage = "NameIsRequired")]
        [Remote("IsProductNameEnExist", "Product", HttpMethod = "POST", ErrorMessage = "Name En Is Already Exist")]
        public string NameEn { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "min value 1 max value 5000")]
        public decimal Price { get; set; }
        
        public List<IFormFile>? Files { get; set; }
        public int CategoryId { get; set; }

    }
}
