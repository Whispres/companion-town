using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET api/user
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetAsync([FromRoute] string id)
        {
            try
            {
                var user = await _userService.GetAsync(id);

                if (user == null)
                {
                    return this.NotFound();
                }

                return this.Ok(user);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // GET api/user
        [HttpGet]
        [ProducesResponseType(typeof(List<Animal>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var users = await this._userService.GetAsync();

                if (users == null)
                {
                    return this.NotFound();
                }

                return this.Ok(users);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> PostAsync([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequest("Invalid");
                }

                var theUser = await _userService.CreateUserAsync(user);

                return this.Created($"/{user.Identifier}", theUser.Name);
            }
            catch (NotModifiedException ex)
            {
                return this.StatusCode(304, ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}