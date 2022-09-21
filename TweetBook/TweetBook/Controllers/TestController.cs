﻿using Microsoft.AspNetCore.Mvc;


public class TestController : Controller
{
    [HttpGet("api/user")]
    public IActionResult Get()
    {
        return Ok(new { name = "Nick" });
    }
}