using RepositoryPatternExample.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternExample.DataLayer.Entities
{
    public class Student: IDbEntity
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public void Load(SqlDataReader reader)
        {
            StudentId = Int32.Parse(reader["StudentId"].ToString());
            FirstName = reader["FirstName"].ToString();
            LastName = reader["LastName"].ToString();
            Email = reader["Email"].ToString();
            PhoneNumber = reader["PhoneNumber"].ToString();
        }
    }
}
