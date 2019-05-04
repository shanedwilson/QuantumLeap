using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Data
{
    public class EventRepository
    {
        const string ConnectionString = "Server = localhost;Database = QuantumLeap; Trusted_Connection = True;";

        public Event AddEvent(string name, DateTime date, bool isCorrected)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newEvent = db.QueryFirstOrDefault<Event>(@"
                Insert into Events (name, date, isCorrected)
                Output Inserted.*
                Values (@Name, @Date, @IsCorrected)",
                new { name, date, isCorrected });

                if (newEvent != null)
                {
                    return newEvent;
                }
            }
            throw new Exception("No Event Created");
        }

        public void DeleteEvent(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute("Delete From Events Where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("That didn't work out.");
                }
            }
        }

        public Event UpdateEvent(Event eventToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"Update Events
                             Set name = @name,
                             date = @date,
                             isCorrected = @isCorrected
                             Where Id = @id", eventToUpdate);

                if (rowsAffected == 1)
                    return eventToUpdate;
            }
            throw new Exception("Could not update event.");
        }

        public IEnumerable<Event> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var events = db.Query<Event>("Select name, isCorrected, id from events").ToList();

                return events;
            }
        }
    }
}
