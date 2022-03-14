using Api.Helper;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {

        [HttpGet]
        public IActionResult GenJwt()
        {
            var user = new User
            {
                UserName = "testUser",
                Email = "test"
            };

            var token = JwtToken.GenerateJwtToken(user, new List<Role> { new Role { Name = "test" } });

            return Ok(new { token = token });
        }
    }
}
