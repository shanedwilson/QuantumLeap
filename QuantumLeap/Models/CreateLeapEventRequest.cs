using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Models
{
    public class CreateLeapEventRequest
    {
        public int LeaperId { get; set; }
        public int LeapeeEventId { get; set; }
        public decimal Cost { get; set; }
    }
}
