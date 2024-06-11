using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Auth.Data;
using PhoneShopApi.Auth.Dto.Phone.Option;
using PhoneShopApi.Auth.Mappers;
using PhoneShopApi.Auth.Models;

namespace PhoneShopApi.Auth.Controllers
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

        private async Task<string> WriteFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file), "The uploaded file cannot be null or empty.");
            }

            string filename = "";
            string exactpath = "";

            try
            {
                var extension = Path.GetExtension(file.FileName); // Use Path.GetExtension for cleaner extraction
                filename = file.FileName; // Generate unique filename with GUID
                var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "Uploads", "PhoneImages"); // Clearer path construction

                // Create uploads folder if it doesn't exist
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                exactpath = Path.Combine(uploadsFolderPath, filename);

                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
            }

            return Path.Combine("Uploads", "PhoneImages", filename); // Return relative path
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
                /*var brand = await _context.Brands
                .Where(b => b.Name.ToLower().Equals(createNewPhoneOptionRequest.BrandName.ToLower()))
                .FirstOrDefaultAsync();
                if (brand == null)
                {
                    brand = new Brand
                    {
                        Name = createNewPhoneOptionRequest.BrandName
                    };

                    await _context.Brands.AddAsync(brand);
                    await _context.SaveChangesAsync();
                }*/

                var phone = await _context.Phones
                    .Where(p => p.Id == phoneId)
                    .FirstOrDefaultAsync();
                if (phone == null) return NotFound("Phone not found.");

                phone.IsSelling = true;
                await _context.SaveChangesAsync();
                var builtInStorage = await _context.BuiltInStorages
                    .Where(b => b.Capacity == createNewPhoneOptionRequest.BuiltInStorageCapacity
                    && b.Unit.ToLower().Equals(createNewPhoneOptionRequest.BuiltInStorageUnit))
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

                var phoneColor = new PhoneColor
                {
                    Name = createNewPhoneOptionRequest.PhoneColorName,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Uploads/PhoneImages/NotFound.jpg"
                };

                await _context.PhoneColors.AddAsync(phoneColor);
                await _context.SaveChangesAsync();

                var phoneOption = await _context.PhoneOptions
                    .Where(po => po.PhoneId == phone.Id)
                    .Include(po => po.PhoneColor)
                    .Include(po => po.BuiltInStorage)
                    .Where(po => po.PhoneColor.Name.ToLower().Equals(createNewPhoneOptionRequest.PhoneColorName)
                    && po.BuiltInStorage.Capacity == createNewPhoneOptionRequest.BuiltInStorageCapacity
                    && po.BuiltInStorage.Unit.ToLower().Equals(createNewPhoneOptionRequest.BuiltInStorageUnit))
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
