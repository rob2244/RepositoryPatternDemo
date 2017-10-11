using RepositoryPatternExample.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternExample.DataLayer.Entities
{
    public class StudentClass: IDbEntity
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime Time { get; set; }
        public int StudentId { get; set; }

        public void Load(SqlDataReader reader)
        {
            ClassId = reader.GetInt32(0);
            ClassName = reader.GetString(1);
            Time = reader.GetDateTime(2);
            StudentId = reader.GetInt32(3);
        }
    }
}
