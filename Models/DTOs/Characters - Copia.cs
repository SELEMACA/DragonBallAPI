using DragonballAPI.Models;

namespace DragonBallZ.Models.DTOs
{
    public class Characters
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ki { get; set; }

        public string MaxKi { get; set; }

        public string Race { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Affiliation { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Planets OriginPlanet { get; set; }
    }

     }