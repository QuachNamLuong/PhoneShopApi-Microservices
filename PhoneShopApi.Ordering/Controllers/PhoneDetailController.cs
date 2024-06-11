using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Ordering.Mappers;
using PhoneShopApi.Ordering.Data;
using PhoneShopApi.Ordering.Dto.Phone.Detail;

namespace PhoneShopApi.Ordering.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneDetailController(PhoneShopDbContext context) : Controller
    {
        private readonly PhoneShopDbContext _context = context;

        [HttpGet("{phoneId:int}")]
        public async Task<IActionResult> GetPhoneDetail(int phoneId)
        {
            var phoneDetail = await _context.PhoneDetails
                .Where(pd => pd.PhoneId == phoneId)
                .FirstOrDefaultAsync();
            if (phoneDetail == null) return NotFound();

            return Ok(phoneDetail.ToPhoneDetailDto());
        }

        [HttpPut("{phoneId:int}")]
        public async Task<IActionResult> UpdatePhoneDetail(
            int phoneId,
            [FromBody] UpdatePhoneDetailRequestDto updatePhoneDetailRequestDto)
        {
            var phoneDetail = await _context.PhoneDetails
                .Where(pd => pd.PhoneId == phoneId)
                .FirstOrDefaultAsync();
            if (phoneDetail == null) return NotFound();

            phoneDetail.Description = updatePhoneDetailRequestDto.Description;
            phoneDetail.Device = updatePhoneDetailRequestDto.Device;
            phoneDetail.Memory = updatePhoneDetailRequestDto.Memory;
            phoneDetail.FronCamera = updatePhoneDetailRequestDto.FronCamera;
            phoneDetail.BackCamera = updatePhoneDetailRequestDto.BackCamera;
            phoneDetail.PinAndCharger = updatePhoneDetailRequestDto.PinAndCharger;
            phoneDetail.Connection = updatePhoneDetailRequestDto.Connection;
            phoneDetail.OsAndCpu = updatePhoneDetailRequestDto.OsAndCpu;
            phoneDetail.Screen = updatePhoneDetailRequestDto.Screen;
            phoneDetail.Sound = updatePhoneDetailRequestDto.Sound;

            await _context.SaveChangesAsync();
            return Ok(phoneDetail.ToPhoneDetailDto());
        }
    }
}
