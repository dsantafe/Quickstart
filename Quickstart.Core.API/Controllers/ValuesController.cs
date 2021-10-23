using Microsoft.AspNetCore.Mvc;
using Quickstart.Core.BL.DTOs;
using System.Collections.Generic;

namespace Quickstart.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var values = new List<ValueDTO>()
            {
                new ValueDTO{ Name = "Name 1" },
                new ValueDTO{ Name = "Name 2" }
            };
            return Ok(values);
        }

        [HttpGet]
        [Route("GetById/{id}/{name}")]
        public IActionResult GetById(int id, string name)
        {
            var values = new List<ValueDTO>()
            {
                new ValueDTO{ Name = $"{id} - {name}" }
            };
            return Ok(values);
        }
    }
}
