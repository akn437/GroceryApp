using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace GroceryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly GroceryAppContext _context;

        public ProductsController(GroceryAppContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            return Grocery.GetAllProducts(_context);

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = Grocery.GetProductById(_context, id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                Grocery.EditProductById(_context, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'GroceryAppContext.Products'  is null.");
            }
            Grocery.AddProducts(_context, product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpGet("Search/{category_id}")]
        public IActionResult SearchProduct(int category_id)
        {
            if (category_id == 0)
            {
                return BadRequest("Bad Request.");
            }

            List<Product> p = Grocery.SearchProduct(_context, category_id);
            if (p.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(p);
        }

        [HttpGet("Search/{product_name}/{price}")]
        public IActionResult SearchProductByNameAndPrice(string product_name, decimal price)
        {

            List<Product> p = Grocery.SearchProductByNameAndPrice(_context, product_name, price);
            if (p.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(p);
        }
    }
}

