using Application.Guest.Responses;

namespace Application.Guest.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(GuestDto guestDto);
    }
}
