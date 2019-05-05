using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Data
{
    public class LeapEventRepository
    {
        const string ConnectionString = "Server = localhost;Database = QuantumLeap; Trusted_Connection = True;";

        public LeapEvent AddLeapEvent(int leaperId, int leapeeEventId, decimal cost)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newLeapEvent = db.QueryFirstOrDefault<LeapEvent>(@"
                Insert into LeapEvents (leaperId, leapeeEventId, cost)
                Output Inserted.*
                Values (@LeaperId, @LeapeeEventId, @Cost)",
                new { leaperId, leapeeEventId, cost });

                if (newLeapEvent != null)
                {
                    return newLeapEvent;
                }
            }
            throw new Exception("No Leap Event Created");
        }

        public void DeleteLeapEvent(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute("Delete From LeapEvents Where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("That didn't work out.");
                }
            }
        }

        public LeapEvent UpdateLeapEvent(LeapEvent leapEventToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"Update LeapEvents
                             Set leaperId = @leaperId,
                             leapeeEventId = @leapeeEventId,
                             cost = @cost
                             Where Id = @id", leapEventToUpdate);

                if (rowsAffected == 1)
                    return leapEventToUpdate;
            }
            throw new Exception("Could not update leapee.");
        }

        public IEnumerable<LeapEvent> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var leapEvents = db.Query<LeapEvent>("Select leaperId," +
                                 "leapeeEventId, cost, id from leapevents").ToList();

                return leapEvents;
            }
        }
    }
}
