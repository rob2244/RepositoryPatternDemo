using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DataLayer.DataContexts
{
    //This is our custom Db Context that inherits from
    //Entity Frameworks Db COntext
    public class MyDbContext : DbContext
    {
        // This is the Db Context Constructor where we pass in
        // our connection string so that entity framework knows
        // which Db to use
        public MyDbContext(string connectionString)
            : base(connectionString)
        {
        }


        // Don't be scared! This method tells Entity Framework how to build our
        // models. Actually in this case we dont even need this method! 
        // Our models are simple enough that we could omit this method and 
        //Entity Framework would build the models correctly by itself.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RepositoryTest");
            base.OnModelCreating(modelBuilder);
        }

        //Here are our Dbsets that tell Entity Framework what to map
        //our database tables to.
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Address> Address { get; set; }

    }
}
