using SuperShop96.Data.Entities;
using SuperShop96.Models;

namespace SuperShop96.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, string path, bool isNew);

        ProductViewModel ToProductViewModel(Product model);
    }
}
