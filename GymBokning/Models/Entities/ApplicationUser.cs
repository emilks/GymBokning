using Microsoft.AspNetCore.Identity;

namespace GymBokning.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserGymClass> Classes { get; set; }
    }
}
