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
    public class LeapersController : ControllerBase
    {
        readonly LeaperRepository _leaperRepository;

        public LeapersController()
        {
            _leaperRepository = new LeaperRepository();
        }

        [HttpPost]
        public ActionResult AddLeaper(CreateLeaperRequest createRequest)
        {
            var newLeaper = _leaperRepository.AddLeaper(createRequest.Name, createRequest.Budget);

            return Created($"api/leapers/{newLeaper.Id}", newLeaper);
        }

        [HttpDelete("{leaperId}")]
        public ActionResult DeleteLeaper(int leaperId)
        {
            _leaperRepository.DeleteLeaper(leaperId);

            return Ok();
        }

        [HttpPut("{leaperId}")]
        public ActionResult UpdateLeaper(Leaper leaper)
        {
            var updatedLeaper = _leaperRepository.UpdateLeaper(leaper);

            return Ok(updatedLeaper);
        }

        [HttpGet]
        public ActionResult GetAllLeapers()
        {
            var leapers = _leaperRepository.GetAll();

            return Ok(leapers);
        }
    }
}