using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interfaces
{
    //This is our repository interface
    public interface IRepository: IDisposable
    {
        // Our CRUD(Create, Read, Update, Delete) operations
        // that anyone implementing our interface must implement
        List<Customer> GetCustomers();
        Customer GetCustomer(int customerId);
        List<Address> GetAddresses();
        List<Address> GetAddresses(int customerId);
        List<Order> GetOrders();
        List<Order> GetOrders(int customerId);
        void AddCustomer(Customer customer);
        void AddOrder(Order order);
        void AddAddress(Address address);
        void DeleteCustomer(int customerId);
        void DeleteAddress(int addressId);
        void DeleteOrder(int orderId);
        bool SaveChanges();
    }
}
