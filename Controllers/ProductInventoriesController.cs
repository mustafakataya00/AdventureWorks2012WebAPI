using Microsoft.AspNetCore.Mvc;
using AdventureWorkAPI.Data;
using AdventureWorkAPI.Models;
using AdventureWorkAPI.Interfaces;
using AutoMapper;
using AdventureWorkAPI.Dto;


namespace AdventureWorkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInventoriesController : Controller
    {
        private readonly AdventureWorks2012Context _context;
        private IInventoryRepository _inventoryRepository;
        private IMapper _mapper;

        public ProductInventoriesController(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }


        
        [HttpGet("{shelf}")]
        public async Task<IActionResult> GetAllProductsByShelf(String shelf)
        {
            var productInventory = await _inventoryRepository.GetAllProductsByShelf(shelf);

            if (productInventory == null)
            {
                return NotFound();
            }

            return Ok(productInventory);
        }

        
        [HttpPut("updateinventory")]
        public async Task<IActionResult> UpdateProductInventory([FromQuery] int productId, [FromQuery] int locationId, [FromBody] short quantity)
        {
            if (await _inventoryRepository.UpdateProductInventory(productId, locationId, quantity))
            {
                return Ok($"Product inventory for productId {productId} at locationId {locationId} has been updated.");
            }

            return NotFound($"Product inventory for productId {productId} at locationId {locationId} was not found.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody]InventoryDto productInventorycreate)
        {
            if (productInventorycreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var InventoryMap = _mapper.Map<ProductInventory>(productInventorycreate);

            if (!await _inventoryRepository.CreateInventory(InventoryMap))
            {
                ModelState.AddModelError("", "Error while Saving");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpGet("quantities")]
        public async Task<IActionResult> GetAllProductQuantities()
        {
            var quantities = await _inventoryRepository.GetAllProductQuantities();

            return Ok(quantities);
        }

    }
}
