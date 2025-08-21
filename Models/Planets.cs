namespace DragonballAPI.Models
{
    public class Planets
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDestroyed { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool? DeletedAt { get; set; } 

    }
}
