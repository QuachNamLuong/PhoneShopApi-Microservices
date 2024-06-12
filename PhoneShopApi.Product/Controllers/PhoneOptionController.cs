using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Product.Data;
using PhoneShopApi.Product.Dto.Phone.Option;
using PhoneShopApi.Product.Mappers;
using PhoneShopApi.Product.Models;

namespace PhoneShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneOptionController(
        PhoneShopDbContext context,
        IWebHostEnvironment environment) : Controller
    {
        private readonly PhoneShopDbContext _context = context;
        private readonly IWebHostEnvironment _environment = environment;



        [HttpGet]
        [Route("phone/{phoneId:int}/phoneColor/{phoneColorId:int}/builtInStorageId/{builtInStorageId:int}")]
        public async Task<IActionResult> GetPhoneOption(int phoneId, int phoneColorId, int builtInStorageId)
        {
            var phoneOption = await _context.PhoneOptions
                .Where(po => po.PhoneId == phoneId && po.BuiltInStorageId == builtInStorageId && po.PhoneColorId == phoneColorId)
                .FirstOrDefaultAsync();

            if (phoneOption == null) return NotFound("Phone option not found.");

            return Ok(phoneOption.ToPhoneOptionDto());
        }

        [HttpDelete]
        [Route("ResetPhoneOption/{phoneId:int}")]
        public async Task<IActionResult> ResetPhoneOption(int phoneId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == phoneId);
                if (phone == null) return NotFound("phone not found.");

                var phoneOptions = await _context.PhoneOptions
                .Where(po => po.PhoneId == phoneId)
                .ToListAsync();
                if (phoneOptions.Count <= 0) return NotFound("phoneOptions not found.");

                var phoneColors = await _context.PhoneOptions
                    .Where(po => po.PhoneId == phoneId)
                    .Include(po => po.PhoneColor)
                    .Select(po => po.PhoneColor)
                    .ToListAsync();
                if (phoneColors.Count <= 0) return NotFound("phoneColors not found.");

                _context.PhoneOptions.RemoveRange(phoneOptions);
                _context.PhoneColors.RemoveRange(phoneColors);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok("Success.");
            }
        }

        [HttpPost]
        [Route("CreateNewPhoneOption/{phoneId:int}")]
        public async Task<IActionResult> CreateNewPhoneOption(
            [FromRoute] int phoneId,
            [FromBody] CreateNewPhoneOptionRequest createNewPhoneOptionRequest)
        {
            if (!ModelState.IsValid) return BadRequest("Cannot create Phone Option now.");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {

                var phone = await _context.Phones
                    .Where(p => p.Id == phoneId)
                    .FirstOrDefaultAsync();
                if (phone == null) return NotFound("Phone not found.");

                phone.IsSelling = true;
                await _context.SaveChangesAsync();
                var builtInStorage = await _context.BuiltInStorages
                    .Where(b => b.Capacity == createNewPhoneOptionRequest.BuiltInStorageCapacity
                    && b.Unit.ToLower().Equals(createNewPhoneOptionRequest.BuiltInStorageUnit.ToLower()))
                    .FirstOrDefaultAsync();
                if (builtInStorage == null)
                {
                    builtInStorage = new BuiltInStorage
                    {
                        Capacity = createNewPhoneOptionRequest.BuiltInStorageCapacity,
                        Unit = createNewPhoneOptionRequest.BuiltInStorageUnit
                    };

                    await _context.BuiltInStorages.AddAsync(builtInStorage);
                    await _context.SaveChangesAsync();
                }

                var phoneColorExist = await _context.PhoneOptions
                    .Where(p => p.PhoneId == phoneId)
                    .Include(p => p.PhoneColor)
                    .Include(p => p.BuiltInStorage)
                    .Where(p => p.BuiltInStorage.Unit.ToLower().Equals(createNewPhoneOptionRequest.BuiltInStorageUnit.ToLower())
                    && p.BuiltInStorage.Capacity == createNewPhoneOptionRequest.BuiltInStorageCapacity)
                    .ToListAsync();

                var phoncolor = phoneColorExist
                    .Any(p => p.PhoneColor.Name.ToLower().Equals(createNewPhoneOptionRequest.PhoneColorName.ToLower()));

                if (phoncolor) return Ok();
                 var phoneColor = new PhoneColor
                {
                    Name = createNewPhoneOptionRequest.PhoneColorName,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Uploads/NotFound.jpg"
                };

                await _context.PhoneColors.AddAsync(phoneColor);
                await _context.SaveChangesAsync();

                var phoneOption = await _context.PhoneOptions
                    .Where(po => po.PhoneId == phone.Id)
                    .Include(po => po.PhoneColor)
                    .Include(po => po.BuiltInStorage)
                    .Where(po => po.PhoneColor.Name.ToLower().Equals(createNewPhoneOptionRequest.PhoneColorName.ToLower())
                    && po.BuiltInStorage.Capacity == createNewPhoneOptionRequest.BuiltInStorageCapacity
                    && po.BuiltInStorage.Unit.ToLower().Equals(createNewPhoneOptionRequest.BuiltInStorageUnit.ToLower()))
                    .FirstOrDefaultAsync();
                if (phoneOption != null) return Ok(phoneOption.ToPhoneOptionDto());

                phoneOption = new PhoneOption
                {
                    PhoneId = phone.Id,
                    BuiltInStorageId = builtInStorage.Id,
                    PhoneColorId = phoneColor.Id,
                    Price = createNewPhoneOptionRequest.Price,
                    Quantity = createNewPhoneOptionRequest.Quantity
                };

                await _context.PhoneOptions.AddAsync(phoneOption);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    await transaction.CommitAsync();
                    return Ok(phoneOption.ToPhoneOptionDto());
                }
                else
                {
                    await transaction.RollbackAsync();
                    return BadRequest("Failed to create phone option.");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhoneOption(
            [FromBody] CreatePhoneOptionRequestDto createPhoneOptionRequestDto)
        {
            var phone = await _context.Phones.FindAsync(createPhoneOptionRequestDto.PhoneId);
            if (phone == null) return NotFound("Phone not found.");

            var builtInStorage = await _context.BuiltInStorages.FindAsync(createPhoneOptionRequestDto.BuiltInStorageId);
            if (builtInStorage == null) return NotFound("Built In Storage not found.");

            var phoneColor = await _context.PhoneColors.FindAsync(createPhoneOptionRequestDto.PhoneColorId);
            if (phoneColor == null) return NotFound("Phone color not found.");

            var phoneOption = await _context.PhoneOptions
                .Where(po => po.PhoneId == createPhoneOptionRequestDto.PhoneId
                            && po.BuiltInStorageId == createPhoneOptionRequestDto.BuiltInStorageId
                            && po.PhoneColorId == createPhoneOptionRequestDto.PhoneColorId)
                .FirstOrDefaultAsync();
            if (phoneOption != null) return BadRequest("Option existed.");


            var newPhoneOption = createPhoneOptionRequestDto.ToPhoneOptionFromCreatePhoneOptionRequestDto();
            await _context.PhoneOptions.AddAsync(newPhoneOption);
            await _context.SaveChangesAsync();

            return Ok(newPhoneOption.ToPhoneOptionDto());
        }

        [HttpPut]
        [Route("UpdatePhoneOption/Phone/{phoneId:int}/PhoneColor/{phoneColorId:int}/BuiltInStorage/{builtInStorageId:int}")]
        public async Task<IActionResult> UpdatePhoneOption(
            int phoneId,
            int phoneColorId,
            int builtInStorageId,
            [FromBody] UpdatePhoneOptionRequest updatePhoneOptionRequest)
        {
            var phoneOption = await _context.PhoneOptions
                .Where(po => po.PhoneId == phoneId
                        && po.BuiltInStorageId == builtInStorageId
                        && po.PhoneColorId == phoneColorId)
                .Include(po => po.PhoneColor)
                .FirstOrDefaultAsync();

            if (phoneOption == null) return NotFound("Phone option not found.");

            var builtInStorage = await _context.BuiltInStorages
                .Where(b => b.Capacity == updatePhoneOptionRequest.BuiltInStorageCapacity
                && b.Unit.ToLower().Equals(updatePhoneOptionRequest.BuiltInStorageUnit.ToLower()))
                .FirstOrDefaultAsync();
            if (builtInStorage == null)
            {
                builtInStorage = new BuiltInStorage
                {
                    Capacity = updatePhoneOptionRequest.BuiltInStorageCapacity,
                    Unit = updatePhoneOptionRequest.BuiltInStorageUnit
                };

                await _context.BuiltInStorages.AddAsync(builtInStorage);
                await _context.SaveChangesAsync();
            }

            phoneOption.BuiltInStorageId = builtInStorage.Id;
            phoneOption.PhoneColor.Name = updatePhoneOptionRequest.PhoneColorName;
            phoneOption.Price = updatePhoneOptionRequest.Price;
            phoneOption.Quantity = updatePhoneOptionRequest.Quantity;
            await _context.SaveChangesAsync();

            return Ok(phoneOption.ToPhoneOptionDto());
        }

        [HttpDelete]
        [Route("phone/{phoneId:int}/phoneColor/{phoneColorId:int}/ram/{builtInStorageId:int}")]
        public async Task<IActionResult> DeletePhoneOption(
            int phoneId,
            int phoneColorId,
            int builtInStorageId)
        {
            var phoneOption = await _context.PhoneOptions
                .Where(po => po.PhoneId == phoneId && po.BuiltInStorageId == builtInStorageId && po.PhoneColorId == phoneColorId)
                .FirstOrDefaultAsync();

            if (phoneOption == null) return NotFound();

            _context.PhoneOptions.Remove(phoneOption);

            await _context.SaveChangesAsync();

            return Ok(phoneOption.ToPhoneOptionDto());
        }
    }
}