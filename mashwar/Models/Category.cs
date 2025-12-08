namespace mashwar.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        // FK → Place
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
