using ProductCatalogService.Models;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogService.Services;

namespace ProductCatalogService.Abellera.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCatalogController : Controller
{
    private readonly ProductService _productService;

    public ProductCatalogController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<List<Product>> Get() =>
        await _productService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _productService.GetAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product newProduct)
    {
        await _productService.CreateAsync(newProduct);
        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updatedProduct)
    {
        var product = await _productService.GetAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        updatedProduct.Id = product.Id;
        await _productService.UpdateAsync(id, updatedProduct);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _productService.GetAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        await _productService.RemoveAsync(id);
        return NoContent();
    }
}

