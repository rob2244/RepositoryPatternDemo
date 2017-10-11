using RepositoryPatternExample.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternExample.DataLayer.Entities
{
    public class StudentAddress : IDbEntity
    {
        public int AddressId { get; set; }
        public int StudentId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public void Load(SqlDataReader reader)
        {
            AddressId = reader.GetInt32(0);
            StudentId = reader.GetInt32(1);
            Street = reader.GetString(2);
            City = reader.GetString(3);
            ZipCode = reader.GetString(4);
            State = reader.GetString(5);
            Country = reader.GetString(6);
        }
    }


}
