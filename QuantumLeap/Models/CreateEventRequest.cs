using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumLeap.Models
{
    public class CreateEventRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsCorrected { get; set; }
    }
}
