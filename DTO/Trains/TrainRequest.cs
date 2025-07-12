namespace Railwaybackproject.DTO.Trains;

public class TrainRequest
{
    public string Name { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int TotalSeats { get; set; }
}
