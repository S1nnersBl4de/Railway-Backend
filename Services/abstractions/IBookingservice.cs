using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.DTO.Trains;
using Railwaybackproject.Models;
using Railwaybackproject.Helper;
using System.Security.Claims;
using Railwaybackproject.DTO.Trains;


namespace Railwaybackproject.Services.abstractions;


public interface IBookingservice
{
    Task<ApiResponse<string>> BookSeatAsync(BookingRequest request, ClaimsPrincipal userClaims);
    Task<ApiResponse<List<UserBookingResponse>>> GetUserBookingsAsync(ClaimsPrincipal user);

    Task<ApiResponse<List<AdminBooking>>> AdminGetAllBookingsAsync();
}
