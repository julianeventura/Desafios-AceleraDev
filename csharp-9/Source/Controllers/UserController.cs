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
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (accelerationName != null)
            {
                return Ok(this._service.FindByAccelerationName(accelerationName)
                    .Select(x => _mapper.Map<UserDTO>(x))
                    .ToList());
            }
            else if (companyId.HasValue)
            {
                return Ok(this._service.FindByCompanyId(companyId.Value)
                    .Select(x => _mapper.Map<UserDTO>(x))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            return Ok(_mapper.Map<UserDTO>(_service.FindById(id)));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(_mapper.Map<UserDTO>(this._service
                    .Save(_mapper.Map<User>(value))));
            }
        }   
     
    }
}
