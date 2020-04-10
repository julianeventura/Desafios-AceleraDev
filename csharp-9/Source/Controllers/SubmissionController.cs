using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private ISubmissionService _service;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            if (challengeId.HasValue && accelerationId.HasValue)
            {
                return Ok(this._service.FindByChallengeIdAndAccelerationId(challengeId.Value, accelerationId.Value)
                    .Select(x => _mapper.Map<SubmissionDTO>(x))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{higherScore}")]
        public ActionResult<decimal> GetHigherScore(int challengeId)
        {
            return Ok(_service.FindHigherScoreByChallengeId(challengeId));
        }

        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<SubmissionDTO>(this._service
                    .Save(_mapper.Map<Submission>(value))));
            }
        }
    }
}
