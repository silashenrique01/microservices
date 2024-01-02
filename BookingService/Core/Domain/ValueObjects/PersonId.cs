using Domain.Enums;
using System.Reflection.Metadata;

namespace Domain.ValueObjects
{
    public class PersonId
    {
        public string IdNumber { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
