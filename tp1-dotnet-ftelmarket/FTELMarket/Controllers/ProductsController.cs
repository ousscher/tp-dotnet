using FTELMarket.Infrastructure;
using FTELMarket.Infrastructure.Configurations;
using FTELMarket.Infrastructure.Entities;
using FTELMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FTELMarket.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly FtelMarketDbContext _dbContext;

    public ProductsController(FtelMarketDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // page et pageSize sont envoyés par le front mais volontairement ignorés dans ce TP.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        var products = await _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .OrderByDescending(product => product.CreatedAt)
            .ThenByDescending(product => product.Id)
            .ToListAsync();

        return Ok(products.Select(ToResponse));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetById(int id)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Category)
            .FirstOrDefaultAsync(product => product.Id == id);

        if (product is null)
        {
            return ProductNotFound(id);
        }

        return Ok(ToResponse(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(ProductPayload payload)
    {
        var category = await _dbContext.Categories.FindAsync(payload.CategoryId!.Value);
        if (category is null)
        {
            return CategoryNotFound(payload.CategoryId.Value);
        }

        var product = new Product
        {
            CreatedAt = DateTimeOffset.UtcNow,
            UserId = UserConfiguration.DefaultUserId,
        };
        Apply(payload, product, category);

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ToResponse(product));
    }

    // PUT est un remplacement complet : tous les champs sont exigés.
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductResponse>> Update(int id, ProductPayload payload)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product is null)
        {
            return ProductNotFound(id);
        }

        var category = await _dbContext.Categories.FindAsync(payload.CategoryId!.Value);
        if (category is null)
        {
            return CategoryNotFound(payload.CategoryId.Value);
        }

        Apply(payload, product, category);
        await _dbContext.SaveChangesAsync();

        return Ok(ToResponse(product));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product is null)
        {
            return ProductNotFound(id);
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private static void Apply(ProductPayload payload, Product product, Category category)
    {
        product.Name = payload.Name!.Trim();
        product.Description = string.IsNullOrWhiteSpace(payload.Description) ? null : payload.Description.Trim();
        product.Price = payload.Price!.Value;
        product.Stock = payload.Stock!.Value;
        product.Category = category;
        product.CategoryId = category.Id;
    }

    private static ProductResponse ToResponse(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Stock = product.Stock,
        CategoryId = product.CategoryId,
        CategoryName = product.Category.Name,
        CreatedAt = product.CreatedAt.UtcDateTime,
    };

    // Les erreurs métier sont renvoyées sous la forme { "message": "..." }.
    private NotFoundObjectResult ProductNotFound(int id) =>
        NotFound(new { message = $"Le produit {id} est introuvable." });

    private BadRequestObjectResult CategoryNotFound(int categoryId) =>
        BadRequest(new { message = $"La catégorie {categoryId} n'existe pas." });
}
