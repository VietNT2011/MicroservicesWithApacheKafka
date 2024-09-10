using Shared;

namespace ProductAPI.ProductServices
{
    //interface
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task DeleteProduct(int id);
    }
}
