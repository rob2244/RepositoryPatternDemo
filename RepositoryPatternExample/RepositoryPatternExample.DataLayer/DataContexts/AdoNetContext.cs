using RepositoryPatternExample.DataLayer.Entities;
using RepositoryPatternExample.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternExample.DataLayer.DataContexts
{
    //Don't worry about this AdoNetContext
    //It's another database access technology that we won't be using
    //Focus more on the Entity Framework Repository
    public class AdoNetContext : IDisposable
    {
        string _connectionString;
        SqlConnection _conn;

        public AdoNetContext(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
        }

        public List<Student> GetStudents()
        {
            string command = @"SELECT StudentId, FirstName, LastName, Email, PhoneNumber FROM dbo.Student";
            return  GetCollectionFromDatabase<Student>(command);
        }

        public List<StudentAddress> GetAddresses()
        {
            string command = @"SELECT AddressId, StudentId, Street, City, ZipCode, State, Country FROM dbo.Address";
            return GetCollectionFromDatabase<StudentAddress>(command);
        }

        public List<StudentAddress> GetAddresses(int studentId)
        {
            string command = @"SELECT AddressId, StudentId, Street, City, ZipCode, State, Country FROM dbo.Address WHERE StudentId = @studentId";
            return GetCollectionFromDatabase<StudentAddress>(command, "studentId", studentId);
        }

        public List<StudentClass> GetClasses()
        {
            string command = @"SELECT ClassId, ClassName, Time, StudentId FROM dbo.Class";
            return GetCollectionFromDatabase<StudentClass>(command);
        }

        public List<StudentClass> GetClasses(int studentId)
        {
            string command = @"SELECT ClassId, ClassName, Time, StudentId FROM dbo.Class WHERE StudentId = @studentId";
            return GetCollectionFromDatabase<StudentClass>(command, "studentId", studentId);
        }

        public Student GetStudent(int studentId)
        {
            string command = @"SELECT StudentId, FirstName, LastName, Email, PhoneNumber FROM dbo.Student WHERE StudentId = @studentId";
            return GetItemFromDatabase<Student>(command, "studentId", studentId);
        }

        public void AddStudent(Student student)
        {
            _conn.Open();

            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO dbo.Student(FirstName, LastName, Email, PhoneNumber) VALUES(@firstName, @lastName, @email, @phoneNumber)";
                    SqlParameter param1 = new SqlParameter("firstName", SqlDbType.NVarChar, 50);
                    SqlParameter param2 = new SqlParameter("lastName", SqlDbType.NVarChar, 50);
                    SqlParameter param3 = new SqlParameter("email", SqlDbType.NVarChar, 50);
                    SqlParameter param4 = new SqlParameter("phoneNumber", SqlDbType.NVarChar, 50);

                    param1.Value = student.FirstName.Trim();
                    param2.Value = student.LastName.Trim();
                    param3.Value = student.Email.Trim();
                    param4.Value = student.PhoneNumber.Trim();

                    cmd.Parameters.AddRange(new[] { param1, param2, param3, param4 });

                    int result = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conn.Close();
            }
        }

        public void AddAddress(StudentAddress address)
        {
            _conn.Open();

            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO dbo.Address(StudentId, Street, City, ZipCode, State, Country) " +
                        "VALUES(@studentId, @street, @city, @zipCode, @state, @country)";

                    SqlParameter param1 = new SqlParameter("studentId", SqlDbType.Int, 50);
                    SqlParameter param2 = new SqlParameter("street", SqlDbType.NVarChar, 50);
                    SqlParameter param3 = new SqlParameter("city", SqlDbType.NVarChar, 50);
                    SqlParameter param4 = new SqlParameter("zipCode", SqlDbType.NVarChar, 50);
                    SqlParameter param5 = new SqlParameter("state", SqlDbType.NVarChar, 50);
                    SqlParameter param6 = new SqlParameter("country", SqlDbType.NVarChar, 50);

                    param1.Value = address.StudentId;
                    param2.Value = address.Street.Trim();
                    param3.Value = address.City.Trim();
                    param4.Value = address.ZipCode.Trim();
                    param5.Value = address.State.Trim();
                    param6.Value = address.Country.Trim();

                    cmd.Parameters.AddRange(new[] { param1, param2, param3, param4, param5, param6 });

                    int result = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conn.Close();
            }
        }

        public void AddClass(StudentClass studentClass)
        {
            _conn.Open();

            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO dbo.Class(ClassName, Time, StudentId) " +
                        "VALUES(@className, @time, @studentId)";

                    SqlParameter param1 = new SqlParameter("className", SqlDbType.NVarChar, 100);
                    SqlParameter param2 = new SqlParameter("time", SqlDbType.DateTime);
                    SqlParameter param3 = new SqlParameter("studentId", SqlDbType.Int);
                    

                    param1.Value = studentClass.ClassName.Trim();
                    param2.Value = studentClass.Time;
                    param3.Value = studentClass.StudentId;
                   
                    cmd.Parameters.AddRange(new[] { param1, param2, param3 });

                    int result = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteStudent(int studentId)
        {
            string cmd = @"DELETE FROM dbo.Student WHERE StudentId = @studentId";
            DeleteFromDatabase(cmd, "studentId", studentId);
        }

        public void DeleteAddress(int addressId)
        {
            string cmd = @"DELETE FROM dbo.Address WHERE AddressId = @addressId";
            DeleteFromDatabase(cmd, "addressId", addressId);
        }

        public void DeleteClass(int classId)
        {
            string cmd = @"DELETE FROM dbo.Address WHERE ClassId = @classId";
            DeleteFromDatabase(cmd, "classId", classId);
        }

        private void DeleteFromDatabase(string sqlCommand, string paramName, int id)
        {
            _conn.Open();

            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = sqlCommand;

                    SqlParameter param1 = new SqlParameter(paramName, SqlDbType.Int);
                    param1.Value = id;
          
                    cmd.Parameters.Add(param1);
                    int result = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conn.Close();
            }
        }

        private List<T> GetCollectionFromDatabase<T>(string sqlCommand) 
            where T: IDbEntity, new()
        {
            _conn.Open();
            List<T> items = new List<T>();
            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = sqlCommand;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        T item = new T();

                        while (reader.Read())
                        {
                            item.Load(reader);
                            items.Add(item);
                        }
                    }
                }
            }
            finally
            {
                _conn.Close();
            }
            return items;
        }

        private List<T> GetCollectionFromDatabase<T>(string sqlCommand, string paramName, int id)
            where T : IDbEntity, new()
        {
            _conn.Open();
            List<T> items = new List<T>();

            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = sqlCommand;
                    SqlParameter param = new SqlParameter(paramName, SqlDbType.Int);
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        T item = new T();

                        if (reader.HasRows)
                        {
                            item.Load(reader);
                            items.Add(item);
                        }
                    }
                }
            }
            finally
            {
                _conn.Close();
            }
            
            return items;
        }

        private T GetItemFromDatabase<T>(string sqlCommand)
            where T: IDbEntity, new()
        {
            _conn.Open();
            T item = new T();
            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = sqlCommand;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        item.Load(reader);
                    }
                }
            }
            finally
            {
                _conn.Close();
            }
            return item;
        }

        private T GetItemFromDatabase<T>(string sqlCommand, string paramName, int id)
            where T : IDbEntity, new()
        {
            _conn.Open();
            T item = new T();

            try
            {
                using (SqlCommand cmd = _conn.CreateCommand())
                {
                    cmd.CommandText = sqlCommand;
                    SqlParameter param = new SqlParameter(paramName, SqlDbType.Int);
                    param.Value = id;
                    cmd.Parameters.Add(param);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        item.Load(reader);
                    }
                }
            }
            finally
            {
                _conn.Close();
            }
            return item;
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
