using System;
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


        // POST api/user
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<User>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult Get([FromQuery] int page, int take)
        {
            try
            {
                return this.Ok(_userService.GetPaged(page, take));
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
        public ActionResult Post([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.BadRequest("Invalid");
                }

                return this.Created($"/{user.Name}",_userService.CreateUser(user));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}