using System;
using Api.Exceptions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/{user}/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            this._animalService = animalService;
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(typeof(AnimalViewModel), 201)]
        [ProducesResponseType(typeof(string), 304)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult Post([FromRoute] string user, [FromBody] AnimalViewModel animal)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user))
                {
                    return this.BadRequest("Invalid user");
                }

                if (!ModelState.IsValid)
                {
                    return this.BadRequest("Invalid");
                }

                this._animalService.CreateAnimalAsync(animal, user);

                return this.Created($"/{animal.Name}", animal);
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.Message);
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

        // http://hamidmosalla.com/2018/04/14/asp-net-core-api-patch-method-without-using-jsonpatchdocument/
        [HttpPatch("{id}", Name = "PatchAnimal")]
        public IActionResult PatchBook([FromRoute]string user, [FromRoute]string id, [FromBody] AnimalPatch animalPatch)
        {
            return NoContent();
        }
    }
}