﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DashboardApi.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Welcome()
        {
            return Ok(new {message = "Wellcome"});
        }
        

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Case>>> GetAllCases()
        // {
        //     return await _dbContext.Cases.ToListAsync();
        // }
    }
}
