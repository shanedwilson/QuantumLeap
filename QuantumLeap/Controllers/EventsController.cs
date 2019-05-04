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
    public class EventsController : ControllerBase
    {
        readonly EventRepository _eventRepository;

        public EventsController()
        {
            _eventRepository = new EventRepository();
        }

        [HttpPost]
        public ActionResult AddEvent(CreateEventRequest createRequest)
        {
            var newEvent = _eventRepository.AddEvent(createRequest.Name, createRequest.Date, createRequest.IsCorrected);

            return Created($"api/leapers/{newEvent.Id}", newEvent);
        }

        [HttpDelete("{eventId}")]
        public ActionResult DeleteEvent(int eventId)
        {
            _eventRepository.DeleteEvent(eventId);

            return Ok();
        }

        [HttpPut("{eventId}")]
        public ActionResult UpdateEvent(Event eventUpdate)
        {
            var updatedEvent = _eventRepository.UpdateEvent(eventUpdate);

            return Ok(updatedEvent);
        }

        [HttpGet]
        public ActionResult GetAllEvents()
        {
            var events = _eventRepository.GetAll();

            return Ok(events);
        }
    }
}