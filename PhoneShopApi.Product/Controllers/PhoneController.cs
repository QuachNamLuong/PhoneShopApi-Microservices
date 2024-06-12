using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Product.Models;
using PhoneShopApi.Product.Data;
using PhoneShopApi.Product.Dto.Phone;
using PhoneShopApi.Product.Helper;
using PhoneShopApi.Product.Interfaces.IRepository;
using PhoneShopApi.Product.Mappers;

namespace PhoneShopApi.Product.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController(PhoneShopDbContext context, IPhoneRepository phoneRepo) : Controller
    {
        private readonly PhoneShopDbContext _context = context;
        private readonly IPhoneRepository _phoneRepo = phoneRepo;

        [HttpGet]
        [Route("AllPhonesActive")]
        public async Task<IActionResult> GetAllPhoneSelling()
        {
            var phones = await _phoneRepo.GetAllPhonesSellingAsync();
            var phoneDTOs = phones.Select(p => p.ToPhoneDto());

            return Ok(phoneDTOs);
        }

        [HttpGet]
        [Route("AllPhonesSellingFollowBrand")]
        public async Task<IActionResult> GetAllPhoneSellingFollowBrand([FromQuery] QueryPhone query)
        {
            var phones = await _phoneRepo.GetAllPhonesSellingFollowBrandAsync(query);
            return Ok(phones);
        }

        [HttpGet]
        [Route("AllPhones")]
        public async Task<IActionResult> GetAll()
        {
            var phones = await _phoneRepo.GetAllAsync();
            var phoneDTOs = phones.Select(p => p.ToPhoneDto());

            return Ok(phoneDTOs);
        }

        [HttpGet("GetPhoneBuiltInStorages/{phoneId:int}")]
        public async Task<IActionResult> GetPhoneBuiltInStorages(int phoneId)
        {
            var p = await _context.Phones
                .Select(p => p)
                .FirstOrDefaultAsync(p => p.Id == phoneId);
            if (p == null) return NotFound("Phone not found.");

            var builtInStorageIds = await _context.PhoneOptions
                .Where(po => po.PhoneId == phoneId)
                .Select(po => po.BuiltInStorageId)
                .ToListAsync();

            var storages = await _context.BuiltInStorages
                .Where(b => builtInStorageIds.Contains(b.Id))
                .ToListAsync();
            var phone = p.ToPhoneDto();
            return Ok(new { phone, storages });
        }

        [HttpGet("{phoneId:int}/{builtInStorageId:int}")]
        public async Task<IActionResult> GetPhoneColors(int phoneId, int builtInStorageId)
        {
            var phoneColors = await _context.PhoneOptions
                .Where(po => po.PhoneId == phoneId && po.BuiltInStorageId == builtInStorageId)
                .Select(po => new
                {
                    po.PhoneColor.Id,
                    po.PhoneColor.ImageUrl,
                    po.PhoneColor.Name,
                    po.Price,
                    po.Quantity
                })
                .ToListAsync();

            return Ok(phoneColors);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePhoneRequestDto createPhoneRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newPhone = createPhoneRequestDto.ToPhoneFromCreatePhoneRequestDto();

            await _phoneRepo.CreateAsync(newPhone);

            return CreatedAtAction(
                nameof(Post),
                new { id = newPhone.Id },
                newPhone.ToPhoneDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePhoneRequestDto updatePhoneRequestDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var phoneModel = await _phoneRepo.UpdateAsync(id, updatePhoneRequestDTO);
            if (phoneModel == null) return NotFound("Phone not found.");

            return Ok(phoneModel.ToPhoneDto());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var deletePhone = await _phoneRepo.DeleteByIdAsync(id);
            if (deletePhone == null) return NotFound();

            return Ok(deletePhone.ToPhoneDto());
        }
    }
}
