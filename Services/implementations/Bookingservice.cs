using Microsoft.EntityFrameworkCore;
using Railwaybackproject.Data;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.DTO.Trains;
using Railwaybackproject.Models;
using Railwaybackproject.Services.abstractions;
using System.Security.Claims;


namespace Railwaybackproject.Services.implementations;

public class Bookingservice :IBookingservice
{
    private readonly DataContext _context;

    public Bookingservice(DataContext context)
    {
        _context = context;
    }


    public async Task<ApiResponse<string>> BookSeatAsync(BookingRequest request, ClaimsPrincipal userClaims)
    {
        var userIdClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = userClaims.FindFirst(ClaimTypes.Name)?.Value;

        if(string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userName))
        {
            return new ApiResponse<string>(false, "Invalid token data");
        }

        int userId = int.Parse(userIdClaim);

        var train = await _context.Trains.FindAsync(request.TrainId);
        if (train == null)
            return new ApiResponse<string>(false, "Train not found");


        var random = new Random();
        int seatNumber = random.Next(1, train.TotalSeats + 1);

        //checking if seat alrdy booked in this train
        while(await _context.Bookings.AnyAsync(b => b.TrainId == train.Id && b.SeatNumber == seatNumber))
        {
            seatNumber = random.Next(1, train.TotalSeats + 1);
        }

        var booking = new Booking
        {
            TrainId = train.Id,
            UserId = userId,
            UserFullName = userName,
            SeatNumber = seatNumber
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return new ApiResponse<string>(true, $"Ticket booked. Seat #{seatNumber}");
    }


    


    public async Task<ApiResponse<List<UserBookingResponse>>> GetUserBookingsAsync(ClaimsPrincipal user)
    {
        var userId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        var bookings = await _context.Bookings
            .Include(b => b.Train)
            .Where(b => b.UserId == userId)
            .ToListAsync();

        var bookingDtos = bookings.Select(b => new UserBookingResponse
        {
            Id = b.Id,
            TrainName = b.Train.Name,
            Source = b.Train.Source,
            Destination = b.Train.Destination,
            DepartureTime = b.Train.DepartureTime,
            ArrivalTime = b.Train.ArrivalTime,
            SeatNumber = b.SeatNumber
        }).ToList();

        return new ApiResponse<List<UserBookingResponse>>(true, "Your bookings", bookingDtos);
    }


    public async Task<ApiResponse<List<AdminBooking>>> AdminGetAllBookingsAsync()
    {
        var bookings = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Train)
            .ToListAsync();

        var dtoList = bookings.Select(b => new AdminBooking
        {
            Id = b.Id,
            Username = b.User.FullName,
            TrainName = b.Train.Name,
            DepartureTime = b.Train.DepartureTime.ToString("g"),
            SeatNumber = b.SeatNumber
        }).ToList();

        return new ApiResponse<List<AdminBooking>>(true, "All bookings fetched", dtoList);
    }
}
