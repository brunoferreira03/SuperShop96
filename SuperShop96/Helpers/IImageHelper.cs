using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SuperShop96.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
