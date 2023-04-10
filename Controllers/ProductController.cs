using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventureWorkAPI.Data;
using AdventureWorkAPI.Models;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Dto;
using Microsoft.CodeAnalysis;
using AdventureWorkAPI.Repository;

namespace AdventureWorkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository _productrepository;
        private IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productrepository = productRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]

        public async Task<IActionResult> GetProducts()
        {

            var products = _mapper.Map<List<ProductDto>>(await _productrepository.GetProducts());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (! await _productrepository.ProductExists(id))
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductDto>(await _productrepository.GetProduct(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto Updatedproduct)
        {
            if (Updatedproduct == null || id != Updatedproduct.ProductId)
            {
                return BadRequest(ModelState);
            }

            if (!await _productrepository.ProductExists(id))
            {
                return NotFound();
            }

            var ProductMap = _mapper.Map<Product>(Updatedproduct);

            if (!await _productrepository.UpdateProduct(ProductMap))
            {

                ModelState.AddModelError("", "Error Occured While Updating");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            return NoContent();
        }


        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ProductMap = _mapper.Map<Product>(productCreate);

            if (!await _productrepository.CreateProduct(ProductMap))
            {

                ModelState.AddModelError("", "Error while Saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {

            if (!await _productrepository.ProductExists(productId))
            {
                return NotFound();
            }

            var productToDelete = await _productrepository.GetProduct(productId);
           
            var billMaterialsToDelete = await _productrepository.GetBillOfMaterial(productId);
            var productCostHistoriesToDelete = await _productrepository.GetProductCostHistories(productId);
            var productInventoriesToDelete = await _productrepository.GetProductInventories(productId);
            var productListPriceHistoriesToDelete = await _productrepository.GetProductListPriceHistories(productId);
            var productProductPhotoesToDelete = await _productrepository.GetProductProductPhotos(productId);
            var productReviewsToDelete = await _productrepository.GetProductReviews(productId);
            var transactionHistoriesToDelete = await _productrepository.GetTransactionHistories(productId);
            var workOrdersToDelete = await _productrepository.GetWorkOrders(productId);

            if (!await _productrepository.DeleteBill(billMaterialsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bills ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteCostHist(productCostHistoriesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting costHist ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteProductInv(productInventoriesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting inventories ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteProductListHist(productListPriceHistoriesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ListPriceHist ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteProductPhoto(productProductPhotoesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting productphoto ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteProductReview(productReviewsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviews ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteTransactionHist(transactionHistoriesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting transactions ");
                return BadRequest(ModelState);
            }
            if (!await _productrepository.DeleteWorkOrder(workOrdersToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting workorders ");
                return BadRequest(ModelState);
            }

            if (!await _productrepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product ");
                return BadRequest(ModelState);
            }

            return NoContent();


        }



    }
}
