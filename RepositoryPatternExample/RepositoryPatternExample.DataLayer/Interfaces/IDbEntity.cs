using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternExample.DataLayer.Interfaces
{
    public interface IDbEntity
    {
        void Load(SqlDataReader reader);
    }
}
