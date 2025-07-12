namespace Railwaybackproject.DTO.Trains;

public class UserBookingResponse
{
    public int Id { get; set; }
    public string TrainName { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int SeatNumber { get; set; }
}
