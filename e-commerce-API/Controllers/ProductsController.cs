using System;
using e_commerce_API.Data;
using e_commerce_API.Model.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly APIContext dbContext;

        public ProductsController(APIContext context)
        {
            dbContext = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var samp = dbContext.Products;
            var products = dbContext.Products.ToList();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("filter")]
        public IActionResult GetProductsByCategory([FromQuery] string? category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category cannot be empty.");
            }

            var categories = category.Trim('"').ToLower().Split(",").ToList();
            var products = dbContext.Products
                .Where(p => categories.Contains(p.Category.ToLower())) // Case insensitive
                .ToList();

            if (products.Count == 0)
            {
                return NotFound($"No products found in category: {category}");
            }

            return Ok(products);
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProduct(int id)
        //{
        //    var product = await dbContext.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateProduct(Products product)
        //{
        //    dbContext.Products.Add(product);
        //    await dbContext.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, Products product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    dbContext.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await dbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await dbContext.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    dbContext.Products.Remove(product);
        //    await dbContext.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProductExists(int id)
        //{
        //    return dbContext.Products.Any(e => e.Id == id);
        //}
    }
}
