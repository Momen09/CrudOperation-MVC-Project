using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SimpleProject.ViewModels.Products
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "NameEnIsRequired")]
        [Remote("IsProductNameArExistExcludeItself", "Product", HttpMethod = "POST",AdditionalFields ="Id", ErrorMessage = "Name Ar Is Already Exist")]

        public string NameAr { get; set; }
        [Required(ErrorMessage = "NameEnIsRequired")]
        [Remote("IsProductNameEnExist", "Product", HttpMethod = "POST",AdditionalFields ="Id", ErrorMessage = "Name En Is Already Exist")]

        public string NameEn { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "min value 1 max value 5000")]
        public decimal Price { get; set; }
        public List<IFormFile>? Files { get; set; }
        public List<string>? CurrentPaths { get; set; }
        public int CategoryId { get; set; }
    }
}
