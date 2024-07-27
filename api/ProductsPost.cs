using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace api
{
  public class ProductsPost
  {
    private readonly IProductData productData;

    public ProductsPost(IProductData productData)
    {
      this.productData = productData;
    }

    [Function("ProductsPost")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req,
        ILogger log)
    {
      var body = await new StreamReader(req.Body).ReadToEndAsync();
      var product = JsonSerializer.Deserialize<Product>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      var newProduct = await productData.AddProduct(product);
      return new OkObjectResult(newProduct);
    }
  }
}
