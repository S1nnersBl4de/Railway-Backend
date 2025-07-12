using System.Diagnostics;

namespace Railwaybackproject.Models;

public class Booking
{
    public int Id { get; set; }

    public int TrainId { get; set; }
    public Train Train { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public string UserFullName { get; set; }
    public int SeatNumber { get; set; }

    public DateTime BookingTime { get; set; } = DateTime.Now;
}
