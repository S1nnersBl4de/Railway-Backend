namespace Railwaybackproject.Models;

public class Train
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Source { get; set; }

    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int TotalSeats { get; set; }

    public int AvailableSeats { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
