using DataLayer.DataContexts;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using RepositoryPatternExample.DataLayer.DataContexts;
using RepositoryPatternExample.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DataPopulator
{
    class Program
    {
        //Here we get our connection string from the configuration section
        //don't worry too much about this code
        static string connectionString = ConfigurationManager.ConnectionStrings["TestDb1"].ToString();
        static void Main(string[] args)
        {
            //Here we are creating the actual object MyDbContext
            MyDbContext ctx1 = new MyDbContext(connectionString);

            //Here is where we pass it to our first repository
            using (IRepository repo = new TestDb1Repository(ctx1))
            {

                //Here we add 1000 customers to TestDb1
                //bool IsSuccess = Add1000Customers(repo);

                //We have to save our changes to our database
                //repo.SaveChanges();

                //Deleting customers with Id's 4, 7, 9 and 11 from database
                // IsSuccess = DeleteCustomersFromDatabase(repo, new List<int> { 4, 7, 8, 9, 11 });
                //repo.SaveChanges();

                //Get a list of addresses for the customer
                //List<Address> addresses = GetAddresses(repo, 20);


                //Console.WriteLine(IsSuccess);
                //addresses.ForEach(a => Console.WriteLine(a));
                //Console.ReadKey();
            }

            //Here we make our second repository connected to a different db
            //with a different context.
            connectionString = ConfigurationManager.ConnectionStrings["TestDb2"].ToString();
            AdoNetContext ctx2 = new AdoNetContext(connectionString);

            using (IRepository repo = new TestDb2Repository(ctx2))
            {

                //Here we add 1000 students to TestDb1
                bool IsSuccess = Add1000Customers(repo);

                //Deleting Students with Id's 4, 7, 9 and 11 from database
                 IsSuccess = DeleteCustomersFromDatabase(repo, new List<int> { 4, 7, 8, 9, 11 });

                Console.ReadKey();
            }

        }

        //As you can see we can pass in a different repository!
        //Our business logic(method) doesn't care what repository we use.
        public static bool Add1000Customers(IRepository repo)
        {
            //This is just a couter variable for our loop
            int i = 0;

            //Adding 1000 customers to the database using our Repository
            foreach (var item in Enumerable.Range(0, 1000))
            {
                Customer customer = new Customer
                {
                    Email = $"Customer{i}@hotmail.com",
                    FirstName = $"CustomerFirst{i}",
                    LastName = $"CustomerLast{i}",
                    PhoneNumber = string.Format("{0}{0}{0}-{0}{0}{0}-{0}{0}{0}{0}", i),
                    Address = new List<Address>
                    {
                        new Address
                        {
                            AddressType = "Home",
                            City = "New York",
                            Country = "USA",
                            State = "NY",
                            Street = "12345 Sinatra St.",
                            ZipCode = "97806"
                        },
                        new Address
                        {
                            AddressType = "Work",
                            City = "New York",
                            Country = "USA",
                            State = "NY",
                            Street = "12345 Sinatra St.",
                            ZipCode = "97806"
                        }
                    },
                    Order = new List<Order>
                    {
                        new Order
                        {
                            ItemName = "Sandals",
                            ItemQuantity = 5,
                            Price = 250
                        }
                    }
                };

                repo.AddCustomer(customer);
                i++;
            };

            
            return true;
        }

        public static bool DeleteCustomersFromDatabase(IRepository repo, List<int> customerIds)
        {
            foreach (int Id in customerIds)
            {
                repo.DeleteCustomer(Id);
                
            }
            return true;
        }

        public static List<Address>  GetAddresses(IRepository repo, int customerId)
        {
            return repo.GetAddresses(customerId);
        }
    }
}
