using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace api
{
  public class ProductsGet
  {
    private readonly IProductData productData;

    public ProductsGet(IProductData productData)
    {
      this.productData = productData;
    }

    [Function("ProductsGet")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req)
    {
      var products = await productData.GetProducts();
      return new OkObjectResult(products);
    }
  }

}

