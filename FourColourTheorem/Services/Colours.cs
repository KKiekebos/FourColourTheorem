using FourColourTheorem.Models;

namespace FourColourTheorem.Services
{
    public static class Colours
    {
        public static int DetermineFirstAvailableColour(this Region region)
        {
            var current = 0;
            while (true)
            {
                if (region.Neighbours.Any(nb => nb.Colour == current))
                {
                    current++;
                } else
                {
                    return current;
                }
            }
        }
    }
}
