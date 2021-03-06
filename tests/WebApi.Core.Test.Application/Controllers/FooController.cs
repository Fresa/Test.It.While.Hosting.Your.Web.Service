﻿using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Test.Application.Services;

namespace WebApi.Core.Test.Application.Controllers
{
    [ApiController]
    public class FooController : ControllerBase
    {
        private readonly IService _service;

        public FooController(IService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("foo/{id}/bar", Name = "Bar")]
        public ActionResult<BarResponse> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return _service.Get(id);
        }
    }
}
