using Application;
using Application.Guest;
using Application.Guest.Ports;
using Domain.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController( ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }


        [HttpPost]
        public async Task<ActionResult<GuestDto>> Post(GuestDto guestDto)
        {
            var res = await _guestManager.CreateGuest(guestDto);

            if (res.Success)
            {
                return Created("", res.Data);
            }
            if(res.ErrorCode == ErrorCodes.NOT_FOUND)
            {

                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
