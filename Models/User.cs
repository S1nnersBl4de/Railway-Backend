using Railwaybackproject.Enums;

namespace Railwaybackproject.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public Userrole Role { get; set; }

    public ICollection<Booking>? Bookings { get; set; }
}
