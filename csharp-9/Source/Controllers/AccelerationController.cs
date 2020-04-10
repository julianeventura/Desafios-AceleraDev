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
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService _service;
        private readonly IMapper _mapper;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId.HasValue)
            {
                return Ok(this._service.FindByCompanyId(companyId.Value)
                    .Select(x => _mapper.Map<AccelerationDTO>(x))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            return Ok(_mapper.Map<AccelerationDTO>(_service.FindById(id)));
        }

        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(_mapper.Map<AccelerationDTO>(this._service
                    .Save(_mapper.Map<Acceleration>(value))));
            }
        }
    }
}
