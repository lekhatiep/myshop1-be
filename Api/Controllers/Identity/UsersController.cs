using Api.Dtos.Identity;
using Api.Extensions;
using Api.Helper;
using Api.Services.Authenticate;
using AutoMapper;
using Domain.Entities.Identity;
using Infastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        public UsersController( IMapper mapper, IAuthenticateService authenticateService)
        {
            _mapper = mapper;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Users.Add(_mapper.Map<User>(user));
            //_context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticateService.Authenticate(login);

            return Ok(result);
        }
    }
}
