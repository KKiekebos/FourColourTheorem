using FourColourTheorem.Models;

namespace FourColourTheorem.Services
{
    public static class Mapping
    {
        public static IEnumerable<Colouring> ToOutput(this Region[] regions)
        {
            return regions.Select(r => { return new Colouring { RegionId = r.Id, Colour = r.Colour ?? 0 }; });
        }
    }
}
