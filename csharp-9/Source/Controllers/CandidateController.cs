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
    public class CandidateController : ControllerBase
    {
        private ICandidateService _service;
        private IMapper _mapper;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? companyId = null, int? accelerationId = null)
        {
            if (companyId.HasValue)
            {
                return Ok(this._service.FindByCompanyId(companyId.Value)
                    .Select(x => _mapper.Map<CandidateDTO>(x))
                    .ToList());
            }
            else if (accelerationId.HasValue)
            {
                return Ok(this._service.FindByAccelerationId(accelerationId.Value)
                    .Select(x => _mapper.Map<CandidateDTO>(x))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            return Ok(_mapper.Map<CandidateDTO>(_service.FindById(userId, accelerationId, companyId)));
        }

        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<CandidateDTO>(this._service
                    .Save(_mapper.Map<Candidate>(value))));
            }
        }
    }
}
