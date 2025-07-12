using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.DTO.Trains;
using Railwaybackproject.Models;
using Railwaybackproject.Services.abstractions;
using Railwaybackproject.Services.abstractions;
using Railwaybackproject.Services.implementations;

namespace Railwaybackproject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainController : ControllerBase
{
    private readonly ITrainservice _trainService;

    public TrainController(ITrainservice trainService)
    {
        _trainService = trainService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTrain(TrainRequest request) //admin can add trains
    {
        var result = await _trainService.AddTrain(request);

        if 
            (!result.Success) return BadRequest(result);


        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrain(int id, TrainRequest request) //updates train properties
    {
        var result = await _trainService.UpdateTrain(id, request);

        if
            (!result.Success) return NotFound(result);


        return Ok(result);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _trainService.GetAllTrains();

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> search([FromQuery] string source, [FromQuery] string destination) //searching from source to destination
    {
        var result = await _trainService.SearchTrains(source, destination);

        return Ok(result);
    }
}
