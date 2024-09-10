using Confluent.Kafka;
using Shared;
using System.Text.Json;

namespace ProductAPI.ProductServices
{
    //implement
    public class ProductService(IProducer<Null, string> producer) : IProductService
    {
        public List<Product> Products = [];
        public async Task AddProduct(Product product)
        {
            Products.Add(product);
            var result = await producer.ProduceAsync("add-product-topic",
                new Message<Null, string> { Value = JsonSerializer.Serialize(product)});
            if(result.Status != PersistenceStatus.Persisted)
            {
                //Get last product
                var lastProduct = Products.Last();
                //Remove last product
                Products.Remove(lastProduct);
            }
        }

        public async Task DeleteProduct(int id)
        {
            Products.Remove(Products.FirstOrDefault(p => p.Id == id)!);
            await producer.ProduceAsync("delete-product-topic", new Message<Null, string>
            {
                Value = id.ToString()
            });
        }
    }
}
