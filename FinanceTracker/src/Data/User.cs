using Microsoft.EntityFrameworkCore;


namespace Tracker.Data
{
    [Index(nameof(Name), nameof(Email))]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
    