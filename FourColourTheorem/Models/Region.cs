namespace FourColourTheorem.Models
{
    public class Region
    {
        public int Id { get; set; }

        public int? Colour { get; set; }

        public Neighbour[]? Neighbours { get; set; }
    }
}
