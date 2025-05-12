using Microsoft.AspNetCore.Http;
using SuperShop96.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperShop96.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
