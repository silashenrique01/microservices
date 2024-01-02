using Application.Guest;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Ports;

namespace Application
{
    public class GuestManager : IGuestManager
    {

        private readonly IGuestRepository _guestRepository;

        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> CreateGuest(GuestDto guestDto)
        {
            try
            {
                var guest = GuestDto.MapToEntity(guestDto);
                guestDto.Id = await _guestRepository.Create(guest);

                return new GuestResponse
                {
                    Data = guestDto,
                    Success = true
                };
            }
            catch (Exception ex) {
                return new GuestResponse
                {
                    Data = null,
                    Success = false,
                    Message = "There was an error when saving to DB",
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA

                };
            }
            
        }
    }
}
