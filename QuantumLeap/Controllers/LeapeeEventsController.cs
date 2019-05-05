using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantumLeap.Data;
using QuantumLeap.Models;

namespace QuantumLeap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeapeeEventsController : ControllerBase
    {
        readonly LeapeeEventRepository _leapeeEventRepository;

        public LeapeeEventsController()
        {
            _leapeeEventRepository = new LeapeeEventRepository();
        }

        [HttpPost]
        public ActionResult AddLeapEvent(CreateLeapeeEventRequest createRequest)
        {
            var newLeapeeEvent = _leapeeEventRepository.AddLeapeeEvent(createRequest.LeapeeId,
                            createRequest.EventId);

            return Created($"api/leapeeevents/{newLeapeeEvent.Id}", newLeapeeEvent);
        }

        [HttpDelete("{leapeeEventId}")]
        public ActionResult DeleteLeapeeEvent(int leapeeEventId)
        {
            _leapeeEventRepository.DeleteLeapeeEvent(leapeeEventId);

            return Ok();
        }

        [HttpPut("{leapEventId}")]
        public ActionResult UpdateLeapeeEvent(LeapeeEvent leapeeEvent)
        {
            var updatedLeapeeEvent = _leapeeEventRepository.UpdateLeapeeEvent(leapeeEvent);

            return Ok(updatedLeapeeEvent);
        }

        [HttpGet]
        public ActionResult GetAllLeapeeEvents()
        {
            var leapeeEvents = _leapeeEventRepository.GetAll();

            return Ok(leapeeEvents);
        }
    }
}