using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using QuantumLeap.Models;

namespace QuantumLeap.Data
{
    public class LeaperRepository
    {
        const string ConnectionString = "Server = localhost;Database = QuantumLeap; Trusted_Connection = True;";

        public Leaper AddLeaper(string name, decimal budget)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newLeaper = db.QueryFirstOrDefault<Leaper>(@"
                    Insert into Leapers(name, budget)
                    Output Inserted.*
                    Values(@Name, @Budget)",
                    new { name, budget });

                if (newLeaper != null)
                {
                    return newLeaper;
                }
            }

            throw new Exception("No Leaper Created");
        }

        public void DeleteLeaper(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute("Delete From Leapers Where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("That didn't work out.");
                }
            }
        }

        public Leaper UpdateLeaper(Leaper leaperToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"Update Leapers
                             Set name = @name,
                                 budget = @budget
                             Where Id = @id", leaperToUpdate);

                if (rowsAffected == 1)
                    return leaperToUpdate;
            }
            throw new Exception("Could not update leaper.");
        }


        public IEnumerable<Leaper> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var leapers = db.Query<Leaper>("Select name, budget, id from leapers").ToList();

                return leapers;
            }


        }
    }
}
