using AdventureWorkAPI.Dto;
using AdventureWorkAPI.Interfaces;
using AdventureWorkAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoController : Controller
    {
        private IProductPhotoRepository _productphotorepository;
        private IMapper _mapper;

        public ProductPhotoController(IProductPhotoRepository productphotoRepository, IMapper mapper)
        {
            _productphotorepository = productphotoRepository; 
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductPhotos()
        {
            var productPhotos = _mapper.Map<List<ProductPhotoDto>>(await _productphotorepository.GetAllProductPhotosAsync());
            return Ok(productPhotos);
        }


        [HttpGet("{productId}/thumbnail")]
        public async Task<IActionResult> GetProductThumbnail(int productId)
        {
            var thumbnail = await _productphotorepository.GetProductThumbnail(productId);

            if (thumbnail == null)
            {
                return NotFound();
            }

            return File(thumbnail, "image/jpeg");
        }


        [HttpGet("{productId}/LargePhoto")]
        public async Task<IActionResult> GetProductLargePhoto(int productId)
        {
            var largephoto = await _productphotorepository.GetProductLargePhoto(productId);

            if (largephoto == null)
            {
                return NotFound();
            }

            return File(largephoto, "image/jpeg");
        }

    }
}
