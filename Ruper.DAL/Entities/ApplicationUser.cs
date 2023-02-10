using Microsoft.AspNetCore.Identity;

namespace Ruper.DAL.Entities
{

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
