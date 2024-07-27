using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace api
{
  public class ProductsPut
  {
    private readonly IProductData productData;

    public ProductsPut(IProductData productData)
    {
      this.productData = productData;
    }

    [Function("ProductsPut")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products")] HttpRequest req,
        ILogger log)
    {
      var body = await new StreamReader(req.Body).ReadToEndAsync();
      var product = JsonSerializer.Deserialize<Product>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      var updatedProduct = await productData.UpdateProduct(product);
      return new OkObjectResult(updatedProduct);
    }
  }
}
