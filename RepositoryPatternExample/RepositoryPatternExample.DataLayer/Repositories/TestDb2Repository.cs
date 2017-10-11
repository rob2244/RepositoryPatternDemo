using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using RepositoryPatternExample.DataLayer.DataContexts;
using RepositoryPatternExample.DataLayer.Entities;

namespace RepositoryPatternExample.DataLayer.Repositories
{
    //Generally this isn't good practice; using
    //one repository interface across multiple databases.
    //DON'T DO THIS IN ACTUAL CODE IT GETS MESSY AND UGLY!
    //If you take a look at the code you'll see pretty quickly that this
    // doesn't make much sense. What we should do instead 
    //is create a different repository interface for TestDb2. 
    //I'm just doing this here to display the flexibility of the repository pattern
    public class TestDb2Repository : IRepository
    {
        AdoNetContext _ctx;
        public TestDb2Repository(AdoNetContext ctx)
        {
            _ctx = ctx;
        }

        public void AddAddress(Address address)
        {
            StudentAddress addr = new StudentAddress
            {
                AddressId = address.AddressId,
                City = address.City,
                Country = address.Country,
                State = address.State,
                Street = address.Street,
                StudentId = address.Customer.CustomerId,
                ZipCode = address.ZipCode
            };

            _ctx.AddAddress(addr);
        }

        public void AddCustomer(Customer customer)
        {
            Student stud = new Student
            {
                StudentId = customer.CustomerId,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber
            };

            _ctx.AddStudent(stud);
        }

        public void AddOrder(Order order)
        {
            StudentClass cls = new StudentClass
            {
                ClassId = order.OrderId,
                ClassName = order.ItemName,
                StudentId = order.Customer.CustomerId,
                Time = DateTime.Now
            };

            _ctx.AddClass(cls);
        }

        public void DeleteAddress(int addressId)
        {
            _ctx.DeleteAddress(addressId);
        }

        public void DeleteCustomer(int customerId)
        {
            _ctx.DeleteStudent(customerId);
        }

        public void DeleteOrder(int orderId)
        {
            _ctx.DeleteClass(orderId);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public List<Address> GetAddresses()
        {
            var addresses = _ctx.GetAddresses();

            return addresses.Select(a => new Address
            {
                AddressId = a.AddressId,
                AddressType = "Home",
                City = a.City,
                Country = a.Country,
                Customer = null,
                State = a.State,
                Street = a.Street,
                ZipCode = a.ZipCode
            }).ToList();
        }

        public List<Address> GetAddresses(int customerId)
        {
            var addresses = _ctx.GetAddresses(customerId);

            return addresses.Select(a => new Address
            {
                AddressId = a.AddressId,
                AddressType = "Home",
                City = a.City,
                Country = a.Country,
                Customer = null,
                State = a.State,
                Street = a.Street,
                ZipCode = a.ZipCode
            }).ToList();
        }

        public Customer GetCustomer(int customerId)
        {
            Student stud = _ctx.GetStudent(customerId);

            return new Customer
            {
                Address = null,
                CustomerId = stud.StudentId,
                Email = stud.Email,
                FirstName = stud.FirstName,
                LastName = stud.LastName,
                Order = null,
               PhoneNumber = stud.PhoneNumber 
            };
        }

        public List<Customer> GetCustomers()
        {
            var studs = _ctx.GetStudents();

            return studs.Select(stud =>
                new Customer
                {
                    Address = null,
                    CustomerId = stud.StudentId,
                    Email = stud.Email,
                    FirstName = stud.FirstName,
                    LastName = stud.LastName,
                    Order = null,
                    PhoneNumber = stud.PhoneNumber
                }).ToList();
        }

        public List<Order> GetOrders()
        {
            var classes = _ctx.GetClasses();

            return classes.Select(cls =>
                new Order
                {
                    Customer = null,
                    ItemName = cls.ClassName,
                    ItemQuantity = 5,
                    OrderId = cls.StudentId,
                    Price = cls.ClassId
                }).ToList();
        }

        public List<Order> GetOrders(int customerId)
        {
            var classes = _ctx.GetClasses(customerId);

            return classes.Select(cls =>
                new Order
                {
                    Customer = null,
                    ItemName = cls.ClassName,
                    ItemQuantity = 5,
                    OrderId = cls.StudentId,
                    Price = cls.ClassId
                }).ToList();
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
