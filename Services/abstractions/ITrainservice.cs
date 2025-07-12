using Railwaybackproject.Models;
using Railwaybackproject.DTO.Trains;

namespace Railwaybackproject.Services.abstractions;

public interface ITrainservice
{
    Task<ApiResponse<string>> AddTrain(TrainRequest request);
    Task<ApiResponse<string>> UpdateTrain(int id, TrainRequest request);
    Task<List<Train>> GetAllTrains();
    Task<List<Train>> SearchTrains(string source, string destination);

}
