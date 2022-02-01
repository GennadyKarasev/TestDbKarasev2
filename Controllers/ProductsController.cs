using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestDbKarasev2.Model;
using TestDbKarasev2.Repository;

namespace TestDbKarasev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;
        //private readonly IProductRepository _productRepository;

        public ProductsController(ProductsContext context) // ,IProductRepository productRepository)
        {
            _context = context;
            //_productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            List<Product> product = _context.Products.ToList();  // не работает, если Description не заполнено. Почему? Там же String.Empty;

            if (product == null)
                return NotFound();

            return new ObjectResult(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(h => h.Id == id);  // что за предупреждение?

            if (product == null)
                return NotFound();

            return new ObjectResult(product);
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> Get(string name)
        {
            IEnumerable<Product> products = await _context.Products.ToListAsync();
            products = products.Where(e => e.Name.Contains(name));

            if (products == null)
                return NotFound();

            return new ObjectResult(products);
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> Post(Product product)
        {
            if (product == null)
                return BadRequest();

            _context.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        

        [HttpPut]
        public async Task<ActionResult<List<Product>>> Put(Product product)
        {
            if (product == null)
                return BadRequest();
            if (!_context.Products.Any(h => h.Id == product.Id))
            {
                return NotFound();
            }
            /*
             * var result = _context.Products.Find(request.Id);
            if (!_context.Products.Any(h => h.Id == product.Id)
                return NotFound();
            result.Name = request.Name;
            result.Description = request.Description;
            */
            _context.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> Remove(Guid id)
        {
            Product product =_context.Products.FirstOrDefault(h => h.Id == id); // async? error

            if (product == null)
                return BadRequest();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(); 
            return Ok(product);
        }
    }
}
