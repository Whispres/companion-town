using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
        [ProducesResponseType(typeof(AnimalPost), 201)]
        [ProducesResponseType(typeof(string), 304)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult Post([FromRoute] string user, [FromBody] AnimalPost animal)
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
                Log.Error(ex, $"{nameof(AnimalController)}-{nameof(Post)} fails");

                return this.BadRequest(ex.Message);
            }
        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchBook([FromRoute]string user, [FromRoute]string id, [FromBody] List<AnimalPatch> animalPatch)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user))
                {
                    return this.BadRequest("Invalid user");
                }

                if (string.IsNullOrWhiteSpace(id))
                {
                    return this.BadRequest("Invalid animal");
                }

                if (ModelState.IsValid)
                {
                    var result = await this._animalService.PatchAnimalAsync(animalPatch, user, id);

                    return this.Ok(result);
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                        .Where(y => y.Count > 0)
                        .ToList();

                    return this.BadRequest(errors);
                }
            }
            catch (BadRequestException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}