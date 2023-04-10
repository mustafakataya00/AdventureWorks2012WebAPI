using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventureWorkAPI.Data;
using AdventureWorkAPI.Models;
using AdventureWorkAPI.Interfaces;
using AutoMapper;
using AdventureWorkAPI.Dto;
using AdventureWorkAPI.Repository;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;

namespace AdventureWorkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : Controller
    {
        
        private ICategoryRepository _categoryRepository;
        private ISubCategoryRepository _subcategoryRepository;
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductCategoriesController(ICategoryRepository categoryRepository,ISubCategoryRepository subCategoryRepository , IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;   
            _subcategoryRepository = subCategoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("products/{categoryid}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryid)
        {
           if(!await _categoryRepository.CategoryExists(categoryid))
            {
                return NotFound();
            }
            var products = await _categoryRepository.GetProductByCategoryID(categoryid);
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error while DontKnow");
                return StatusCode(500, ModelState);
            }
            return Ok(products);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody]CategoryDto productCategoryUpdated)
        {
            if (productCategoryUpdated == null)
            {
                return BadRequest(ModelState);
            }
            if (id != productCategoryUpdated.ProductCategoryId)
            {
                return BadRequest(ModelState);
            }

            if (!await _categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }
            var ProductCatMap = _mapper.Map<ProductCategory>(productCategoryUpdated);

            if (!await _categoryRepository.UpdateCategory(ProductCatMap))
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

        
        [HttpPost("/SubCategory")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateSubCategory([FromBody]SubCategoryDto productsubCategory)
        {
          if (productsubCategory == null)
          {
                return BadRequest(ModelState);
          }

          if (!ModelState.IsValid)
          {
               return BadRequest(ModelState);
          }
            var subCategoryMap= _mapper.Map<ProductSubcategory>(productsubCategory);

            if (!await _subcategoryRepository.CreateSubCat(subCategoryMap))
            {
                ModelState.AddModelError("", "Error while Saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto productCategory)
        {
            if (productCategory == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CategoryMap = _mapper.Map<ProductCategory>(productCategory);

            if (!await _categoryRepository.CreateCategory(CategoryMap))
            {
                ModelState.AddModelError("", "Error while Saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{categoryid}")]
        public async Task<IActionResult> DeleteProductCategory(int categoryid)
        {
            //if (!await _categoryRepository.CategoryExists(categoryid))
            //{
            //    return NotFound();
            //}

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var categoryToDelete = await _categoryRepository.GetCategory(categoryid);
            //var subcatToDelete = await _subcategoryRepository.GetSubCatByCatID(categoryid);
            //var productsToDelete = new List<Product>();

            //foreach (var subcat in subcatToDelete)
            //{
            //    var products = await _productRepository.GetProductsBySubCatId(subcat.ProductSubcategoryId);
            //    if (products != null)
            //    {
            //        productsToDelete.AddRange(products);
            //    }
            //}
            //foreach(var product in productsToDelete)
            //{
            //    var billMaterialsToDelete = await _productRepository.GetBillOfMaterial(product.ProductId);
            //    var productCostHistoriesToDelete = await _productRepository.GetProductCostHistories(product.ProductId);
            //    var productInventoriesToDelete = await _productRepository.GetProductInventories(product.ProductId);
            //    var productListPriceHistoriesToDelete = await _productRepository.GetProductListPriceHistories(product.ProductId);
            //    var productProductPhotoesToDelete = await _productRepository.GetProductProductPhotos(product.ProductId);
            //    var productReviewsToDelete = await _productRepository.GetProductReviews(product.ProductId);
            //    var transactionHistoriesToDelete = await _productRepository.GetTransactionHistories(product.ProductId);
            //    var workOrdersToDelete = await _productRepository.GetWorkOrders(product.ProductId);

            //    if (!await _productRepository.DeleteBill(billMaterialsToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting bills ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteCostHist(productCostHistoriesToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting costHist ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteProductInv(productInventoriesToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting inventories ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteProductListHist(productListPriceHistoriesToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting ListPriceHist ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteProductPhoto(productProductPhotoesToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting productphoto ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteProductReview(productReviewsToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting reviews ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteTransactionHist(transactionHistoriesToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting transactions ");
            //        return BadRequest(ModelState);
            //    }
            //    if (!await _productRepository.DeleteWorkOrder(workOrdersToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting workorders ");
            //        return BadRequest(ModelState);
            //    }
            //}

            //if (productsToDelete.Any())
            //{
            //    if (!await _productRepository.DeleteProducts(productsToDelete))
            //    {
            //        ModelState.AddModelError("", "Something went wrong deleting products.");
            //    }
            //}
            //if (!await _categoryRepository.DeleteSubCategories(subcatToDelete))
            //{
            //    ModelState.AddModelError("", "Something went wrong deleting subcategories");
            //    return BadRequest(ModelState);
            //}

            //if (!await _categoryRepository.DeleteCategory(categoryToDelete))
            //{
            //    ModelState.AddModelError("", "Something went wrong deleting category");
            //    return BadRequest(ModelState);
            //}

            return Content("Delete Success");
        }


    }
}
