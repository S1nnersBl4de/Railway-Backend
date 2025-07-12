using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.DTO.Trains;
using Railwaybackproject.Services.abstractions;
using Railwaybackproject.Services.implementations;

namespace Railwaybackproject.Controllers;

[Authorize] // Require JWT for all endpoints in this controller
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{

    private readonly IBookingservice _bookingService;

    public BookingController(IBookingservice bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("book-seat")]
    public async Task<IActionResult> BookSeat([FromBody] BookingRequest request)
    {
        var result = await _bookingService.BookSeatAsync(request, User); // Pass JWT claims
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("my-bookings")]
    public async Task<IActionResult> GetUserbookings()
    {
        var result = await _bookingService.GetUserBookingsAsync(User);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all-bookings")]
    public async Task<IActionResult> GetAllBookings()
    {
        var result = await _bookingService.AdminGetAllBookingsAsync();
        return result.Success ? Ok(result) : BadRequest(result);
    }




}
