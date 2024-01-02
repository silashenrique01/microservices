using Domain.Enums;
using Entities = Domain.Entities;

namespace Application.Guest
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Emain { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Entities.Guest MapToEntity(GuestDto guestDto)
        {
            return new Entities.Guest
            {
                Id = guestDto.Id,
                Name = guestDto.Name,
                Surname = guestDto.Surname,
                Emain = guestDto.Emain,
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    IdNumber = guestDto.IdNumber,
                    DocumentType = (DocumentType)guestDto.IdTypeCode
                }

            };
        }
    }
}
