using DataLayer.DataContexts;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;
using System.Linq;

namespace DataLayer.Repositories
{
    //This is one of our concrete(actual class) repositories
    //implementing our repository interface.
    //Remember that in order to implement an interface a class 
    //must have AT LEAST all the same things the interface has.
    public class TestDb1Repository : IRepository
    {
        //This is the field were we store the MyDbContextObject
        readonly MyDbContext _context;

        //Here we are passing in MyDbContext into the constructor of our 
        //Repository and storing it in a field. Whenever we pass in an object
        // to a constructor and store it this is called DEPENDENCY INJECTION.
        public TestDb1Repository(MyDbContext context)
        {
            _context = context;
        }

        public void AddAddress(Address address)
        {
            _context.Address.Add(address);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteAddress(int addressId)
        {
            Address addr = _context
                .Address
                .FirstOrDefault(a => a.AddressId == addressId);

            if (addr == null) throw new ArgumentException($"Could not find the address with Id {addressId}");

            _context.Address.Remove(addr);
        }

        public void DeleteCustomer(int customerId)
        {
            Customer cust = _context.Customer
                .FirstOrDefault(c => c.CustomerId == customerId);

            if(cust == null) throw new ArgumentException($"Could not find the address with Id {customerId}");

            _context.Address.RemoveRange(cust.Address);
            _context.Order.RemoveRange(cust.Order);
            _context.Customer.Remove(cust);
        }

        public void DeleteOrder(int orderId)
        {
            Order order = _context.Order
                .FirstOrDefault(c => c.OrderId == orderId);

            if (order == null) throw new ArgumentException($"Could not find the address with Id {orderId}");

            _context.Order.Remove(order);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Address> GetAddresses()
        {
            return _context.Address.ToList();
        }

        public List<Address> GetAddresses(int customerId)
        {
            List<Address> addresses = _context
                .Address
                .Where(a => a.Customer.CustomerId == customerId)
                .ToList();

            return addresses;
        }

        public Customer GetCustomer(int customerId)
        {
            return _context
                .Customer
                .FirstOrDefault(c => c.CustomerId == customerId);
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customer.ToList();
        }

        public List<Order> GetOrders()
        {
            return _context.Order.ToList();
        }

        public List<Order> GetOrders(int customerId)
        {
            List<Order> orders = _context
                .Order
                .Where(o => o.Customer.CustomerId == customerId)
                .ToList();

            return orders;
        }

        public bool SaveChanges()
        {
            int result = _context.SaveChanges();

            if (result != 0) return true;
            else return false;
        }
    }
}
