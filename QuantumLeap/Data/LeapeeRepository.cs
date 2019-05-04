using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Data
{
    public class LeapeeRepository
    {
        const string ConnectionString = "Server = localhost;Database = QuantumLeap; Trusted_Connection = True;";

        public Leapee AddLeapee(string name)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newLeapee = db.QueryFirstOrDefault<Leapee>(@"
                Insert into Leapees (name)
                Output Inserted.*
                Values (@Name)",
                new { name });

                if (newLeapee != null)
                {
                    return newLeapee;
                }
            }
            throw new Exception("No Leapee Created");
        }

        public void DeleteLeapee(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute("Delete From Leapees Where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("That didn't work out.");
                }
            }
        }

        public Leapee UpdateLeapee(Leapee leapeeToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"Update Leapees
                             Set name = @name
                             Where Id = @id", leapeeToUpdate);

                if (rowsAffected == 1)
                    return leapeeToUpdate;
            }
            throw new Exception("Could not update leapee.");
        }


        public IEnumerable<Leapee> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var leapees = db.Query<Leapee>("Select name, id from leapees").ToList();

                return leapees;
            }


        }
    }
}
