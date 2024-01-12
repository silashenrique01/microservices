using Application.Guest;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Exceptions;
using Domain.Ports;
using System.Text.RegularExpressions;

namespace Application
{
    public class GuestManager : IGuestManager
    {

        private readonly IGuestRepository _guestRepository;

        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDto.MapToEntity(request);

                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                //guestDto.Id = await _guestRepository.Create(guest);

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            catch(InvalidPersonDocumentIdException error) {

                return new GuestResponse
                {
                    Data = null,
                    Success = false,
                    Message = "The ID passed is not valid",
                    ErrorCode = ErrorCodes.INVALID_PERSON_ID
                };
            }

            catch(MissingRequiredInformation error)
            {
                return new GuestResponse
                {
                    Data = null,
                    Success = false,
                    Message = "Missing required information passed",
                    ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION
                };
            }

            catch (InvalidEmailException error)
            {
                return new GuestResponse
                {
                    Data = null,
                    Success = false,
                    Message = "The given email is not valid",
                    ErrorCode = ErrorCodes.INVALID_EMAIL
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
