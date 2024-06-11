using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneShopApi.Ordering.Data;
using PhoneShopApi.Ordering.Dto.Brand;
using PhoneShopApi.Ordering.Interfaces.IRepository;
using PhoneShopApi.Ordering.Mappers;

namespace PhoneShopApi.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController(
        PhoneShopDbContext context,
        IBrandRepository brandRepo) : Controller
    {
        private readonly PhoneShopDbContext _context = context;
        private readonly IBrandRepository _brandRepo = brandRepo;


        [HttpGet("AllBrand")]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandRepo.GetAllAsync();
            var brandDTOs = brands.Select(b => b.ToBrandDto());

            return Ok(brandDTOs);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandRepo.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand.ToBrandDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBrandRequestDto createBrandRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBrand = createBrandRequestDto.ToBrandFromCreateBrandRequestDto();
            newBrand = await _brandRepo.Create(newBrand);

            return CreatedAtAction(
                nameof(Create),
                new { brandId = newBrand.Id },
                newBrand.ToBrandDto());
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateBrandRequestDto updateBrandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandUpdated = await _brandRepo.Update(Id, updateBrandDto);
            if (brandUpdated == null)
            {
                return NotFound("Brand not found.");
            }

            return Ok(brandUpdated.ToBrandDto());
        }


        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteById([FromRoute] int Id)
        {
            var brandToDelete = await _brandRepo.DeleteById(Id);
            if (brandToDelete == null)
            {
                return NotFound("Brand not found.");
            }

            return Ok("Brand deleted.");
        }
    }
}
