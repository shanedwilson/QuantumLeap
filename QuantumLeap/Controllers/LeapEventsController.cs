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
    public class LeapEventsController : ControllerBase
    {
        readonly LeapEventRepository _leapEventRepository;

        public LeapEventsController()
        {
            _leapEventRepository = new LeapEventRepository();
        }

        [HttpPost]
        public ActionResult AddLeapEvent(CreateLeapEventRequest createRequest)
        {
            var newLeapEvent = _leapEventRepository.AddLeapEvent(createRequest.LeaperId,
                            createRequest.LeapeeEventId, createRequest.Cost);

            return Created($"api/leapers/{newLeapEvent.Id}", newLeapEvent);
        }

        [HttpDelete("{leapEventId}")]
        public ActionResult DeleteLeapEvent(int leapEventId)
        {
            _leapEventRepository.DeleteLeapEvent(leapEventId);

            return Ok();
        }

        [HttpPut("{leapEventId}")]
        public ActionResult UpdateLeapEvent(LeapEvent leapEvent)
        {
            var updatedLeapEvent = _leapEventRepository.UpdateLeapEvent(leapEvent);

            return Ok(updatedLeapEvent);
        }

        [HttpGet]
        public ActionResult GetAllLeapEvents()
        {
            var leapEvents = _leapEventRepository.GetAll();

            return Ok(leapEvents);
        }
    }
}