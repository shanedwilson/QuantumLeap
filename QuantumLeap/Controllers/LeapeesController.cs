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
    public class LeapeesController : ControllerBase
    {
        readonly LeapeeRepository _leapeeRepository;

        public LeapeesController()
        {
            _leapeeRepository = new LeapeeRepository();
        }

        [HttpPost]
        public ActionResult AddLeapee(CreateLeapeeRequest createRequest)
        {
            var newLeapee = _leapeeRepository.AddLeapee(createRequest.Name);

            return Created($"api/leapees/{newLeapee.Id}", newLeapee);
        }

        [HttpDelete("{leapeeId}")]
        public ActionResult DeleteLeapee(int leapeeId)
        {
            _leapeeRepository.DeleteLeapee(leapeeId);

            return Ok();
        }

        [HttpPut("{leapeeId}")]
        public ActionResult UpdateLeapee(Leapee leapee)
        {
            var updatedLeapee = _leapeeRepository.UpdateLeapee(leapee);

            return Ok(updatedLeapee);
        }

        [HttpGet]
        public ActionResult GetAllLeapees()
        {
            var leapees = _leapeeRepository.GetAll();

            return Ok(leapees);
        }
    }
}