using System;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/{user}/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        // http://hamidmosalla.com/2018/04/14/asp-net-core-api-patch-method-without-using-jsonpatchdocument/
        // POST api/user
        [HttpPost]
        [ProducesResponseType(typeof(Animal), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult Post([FromRoute] string user, [FromBody] Animal animal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.BadRequest("Invalid");
                }

                return this.Created($"/{animal.Name}", animal);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}", Name = "PatchAnimal")]
        public IActionResult PatchBook([FromRoute]string user, [FromRoute]string id, [FromBody] PatchDto patchDtos)
        {
            return NoContent();
        }
    }

    public class PatchDto
    {
        public string PropertyName { get; set; }

        public int PropertyValue { get; set; }
    }
}