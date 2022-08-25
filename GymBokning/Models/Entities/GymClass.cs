using System.ComponentModel.DataAnnotations;

namespace GymBokning.Models.Entities
{
    public class GymClass
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public  DateTime EndTime { get { return StartTime + Duration; } }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }
        public ICollection<ApplicationUserGymClass> Users { get; set; } = new List<ApplicationUserGymClass>();

    }
}
