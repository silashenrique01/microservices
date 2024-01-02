using Domain.Enums;

namespace Domain.Entities
{
    public class Price
    {
        public decimal Value {  get; set; }    
        public AcceptedCurrencies Currency { get; set; }


    }
}
