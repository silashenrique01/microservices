using Application.Guest.Requests;
using Domain.Enums;
using Entities = Domain.Entities;

namespace Application.Guest
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Entities.Guest MapToEntity(CreateGuestRequest request)
        {
            return new Entities.Guest
            {
                Id = request.Data.Id,
                Name = request.Data.Name,
                Surname = request.Data.Surname,
                Email = request.Data.Email,
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    IdNumber = request.Data.IdNumber,
                    DocumentType = (DocumentType)request.Data.IdTypeCode
                }

            };
        }
    }
}
