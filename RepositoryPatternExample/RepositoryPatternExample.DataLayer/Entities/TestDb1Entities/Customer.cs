using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
