using Microsoft.EntityFrameworkCore;
using Railwaybackproject.Data;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.DTO.Trains;
using Railwaybackproject.Enums;
using Railwaybackproject.Models;
using Railwaybackproject.Services.abstractions;
using System.IdentityModel.Tokens.Jwt;


namespace Railwaybackproject.Services.implementations;

public class Trainservice : ITrainservice
{

    private readonly DataContext _context;

    public Trainservice(DataContext context)
    {
        _context = context;
    }


    public async Task<ApiResponse<string>> AddTrain(TrainRequest request)
    {
        var train = new Train
        {
            Name = request.Name,
            Source = request.Source,
            Destination = request.Destination,
            DepartureTime = request.DepartureTime,
            ArrivalTime = request.ArrivalTime,
            TotalSeats = request.TotalSeats,
            AvailableSeats = request.TotalSeats
        };

        _context.Trains.Add(train);
        await _context.SaveChangesAsync();

        return new ApiResponse<string>(true, "Train added successfully");
    }


    public async Task<ApiResponse<string>> UpdateTrain(int id, TrainRequest request)
    {
        var train = await _context.Trains.FindAsync(id);
        if (train == null)
        {
            return new ApiResponse<string>(false, "Train not found");
        }

        train.Name = request.Name;
        train.Source = request.Source;
        train.Destination = request.Destination;
        train.DepartureTime = request.DepartureTime;
        train.ArrivalTime = request.ArrivalTime;
        train.TotalSeats = request.TotalSeats;
        train.AvailableSeats = request.TotalSeats;

        await _context.SaveChangesAsync();
        return new ApiResponse<string>(true, "Train updated successfully");
    }


    public async Task<List<Train>> GetAllTrains()
    {
        return await _context.Trains.ToListAsync();
    }

    public async Task<List<Train>> SearchTrains(string source, string destination)
    {
        return await _context.Trains
            .Where(t => t.Source == source && t.Destination == destination)
            .ToListAsync();
    }
}
