namespace DataLayer.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public virtual Customer Customer { get; set; }
        public string AddressType { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{AddressType}: {Street} {ZipCode} {City}, {State}";
        }
    }
}