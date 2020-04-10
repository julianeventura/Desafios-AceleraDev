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
    public class CompanyController : ControllerBase
    {
        private ICompanyService _service;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue)
            {
                return Ok(this._service.FindByAccelerationId(accelerationId.Value)
                    .Select(x => _mapper.Map<CompanyDTO>(x))
                    .ToList());
            }
            else if (userId.HasValue)
            {
                return Ok(this._service.FindByUserId(userId.Value)
                    .Select(x => _mapper.Map<CompanyDTO>(x))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            return Ok(_mapper.Map<CompanyDTO>(_service.FindById(id)));
        }

        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_mapper.Map<CompanyDTO>(this._service
                    .Save(_mapper.Map<Company>(value))));
            }
        }
    }
}
