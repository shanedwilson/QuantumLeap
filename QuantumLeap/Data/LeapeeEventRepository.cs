using Dapper;
using QuantumLeap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Data
{
    public class LeapeeEventRepository
    {
        const string ConnectionString = "Server = localhost;Database = QuantumLeap; Trusted_Connection = True;";

        public LeapeeEvent AddLeapeeEvent(int leapeeId, int eventId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newLeapeeEvent = db.QueryFirstOrDefault<LeapeeEvent>(@"
                Insert into LeapeeEvents (leapeeId, eventId)
                Output Inserted.*
                Values (@LeapeeId, @EventId)",
                new { leapeeId, eventId });

                if (newLeapeeEvent != null)
                {
                    return newLeapeeEvent;
                }
            }
            throw new Exception("No Leapee Event Created");
        }

        public void DeleteLeapeeEvent(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute("Delete From LeapeeEvents Where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("That didn't work out.");
                }
            }
        }

        public LeapeeEvent UpdateLeapeeEvent(LeapeeEvent leapeeEventToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"Update LeapeeEvents
                             Set leapeeId = @leapeeId,
                             eventId = @leapeeEventId,
                             Where Id = @id", leapeeEventToUpdate);

                if (rowsAffected == 1)
                    return leapeeEventToUpdate;
            }
            throw new Exception("Could not update leapee.");
        }

        public IEnumerable<LeapeeEvent> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var leapeeEvents = db.Query<LeapeeEvent>("Select leapeeId," +
                                 "eventId, id from leapeeevents").ToList();

                return leapeeEvents;
            }
        }

        public LeapeeEvent GetRandomLeapeeEvent()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var randomLeapeeEvent = db.QueryFirstOrDefault<LeapeeEvent>(@"Select TOP(1) le.* 
                                                                       From LeapeeEvents as le
                                                                       Order By NEWID()");

                return randomLeapeeEvent;
            }
        }
    }
}
