using DataLayer.DataContexts;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using System;

namespace DataPopulater
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationManager
            MyDbContext context = new MyDbContext();
            IRepository repo = new TestDb1Repository(context);
        }
    }
}
