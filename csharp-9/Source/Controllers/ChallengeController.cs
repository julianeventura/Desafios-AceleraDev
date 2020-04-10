using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private IChallengeService _service;
        private readonly IMapper _mapper;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue && userId.HasValue)
            {
                return Ok(this._service.FindByAccelerationIdAndUserId(accelerationId.Value, userId.Value)
                    .Select(x => _mapper.Map<ChallengeDTO>(x))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody] ChallengeDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<ChallengeDTO>(_service
                    .Save(_mapper.Map<Models.Challenge>(value))));
            }
        }
    }
}
