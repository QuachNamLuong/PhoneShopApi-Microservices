using Microsoft.AspNetCore.Mvc;
using PhoneShopApi.Data;
using PhoneShopApi.Interfaces.IRepository;
using PhoneShopApi.Dto.BuiltInStorage;
using PhoneShopApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace PhoneShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuiltInStorageController(PhoneShopDbContext context, IBuiltInStorageRepository ramRepo) : Controller
    {
        private readonly PhoneShopDbContext _context = context;
        private readonly IBuiltInStorageRepository _ramRepo = ramRepo;

        [HttpGet]
        [Route("GetAllbuiltInStorage")]
        public async Task<IActionResult> GetAllRams()
        {
            var builtInStorages = await _ramRepo.GetAllRamsAsync();
            var builtInStoragesDto = builtInStorages.Select(b => b.ToRamDto());
            return Ok(builtInStorages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById( int id)
        {
            var ram = await _ramRepo.GetByIdAsync(id);

            if (ram is null) return NotFound();

            return Ok(ram.ToRamDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBuiltInStorageRequestDto createRamRequestDto)
        {
            var storage = await _context.BuiltInStorages
                .Where(s => s.Capacity == createRamRequestDto.Capacity && s.Unit == createRamRequestDto.Unit)
                .FirstOrDefaultAsync();
            if (storage != null) return Ok(storage.ToRamDto());

            var newRam = createRamRequestDto.ToRamFromCreateRamRequestDto();
            await _ramRepo.CreateAsync(newRam);

            return CreatedAtAction(nameof(Post), new { id = newRam.Id }, newRam.ToRamDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(
            [FromRoute] int id,
            [FromBody] UpdateBuiltInStorageRequestDto updateRamRequestDto)
        {
            var ramUpdated = await _ramRepo.UpdateAsync(id, updateRamRequestDto);

            if (ramUpdated is null) return NotFound();

            return Ok(ramUpdated.ToRamDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRamById([FromRoute] int id)
        {
            var deletedRam = await _ramRepo.DeleteAsync(id);
            if (deletedRam is null)
            {
                return NotFound();
            }

            return Ok(deletedRam.ToRamDto());
        }

    }
}
