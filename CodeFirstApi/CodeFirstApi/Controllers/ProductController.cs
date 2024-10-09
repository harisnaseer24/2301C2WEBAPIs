using CodeFirstApi.Data;
using CodeFirstApi.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.Include(o => o.Brand).ToList();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult AddProduct(ProductDTO prd)
        {
            Product product = new Product()
            {
                Name=prd.Name,
                Description=prd.Description,
                Price=prd.Price,
                BrandId=prd.BrandId,
            };
          var addedProduct= _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(addedProduct.Entity);
        }


        [HttpGet("Filter/{bid}")]
        //[Route("{filter}/{bid}")]
        public IActionResult GetProductsByBrand(int bid)
        {
            var products = _context.Products.Include(o => o.Brand).Where(o => o.BrandId == bid).ToList();
            return Ok(products);
        }
        [HttpGet("Search/{query}")]
        public IActionResult SearchProduct(string query)
        {
            // For partial match search
            var products = _context.Products
                                   .Include(o => o.Brand)
                                   .Where(o => o.Name.Contains(query)
                                           || o.Description.Contains(query)
                                           || o.Brand.Name.Contains(query))
                                   .ToList(); // Partial Match

            return Ok(products);
        }

        //[HttpGet("Search}/{query}")]
        ////[Route("{Search}/{query}")]
        //public IActionResult SearchProduct(string query)
        //{
        //   // var products = _context.Products.Include(o => o.Brand).Where(o => o.Name==query || o.Description==query || o.Brand.Name ==query).ToList();//Exact match

        // var products = _context.Products.Include(o => o.Brand).Where(o => o.Name.Contains(query) || o.Description.Contains(query) || o.Brand.Name.Contains(query)).ToList();//Partial Match

        //    return Ok(products);
        //}

      
        //Edit
        [HttpGet("{id}")]
        public IActionResult GetProductDetails(int id)
        {
            if(id!= null)
            {
                var pDetails = _context.Products.Include(o => o.Brand).FirstOrDefault(o=> o.Id == id);

                if(pDetails != null)
                {
                    return Ok(pDetails);
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            else
            {
                return NotFound("Please provide product id to get details.");
            }           
        }
        [HttpPut]
        public IActionResult EditProduct(ProductDTO product)
        {
            var productToEdit = _context.Products.FirstOrDefault(o => o.Id ==product.Id);

            if(productToEdit != null)
            {
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.Description = product.Description;
                productToEdit.BrandId = product.BrandId;

                var editQuery=  _context.Products.Update(productToEdit);
                _context.SaveChanges();
                return Ok(editQuery.Entity);

            }
            else
            {
                return BadRequest("Invalid Id");
            }
          
        }
    }
}
