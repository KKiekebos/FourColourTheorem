using FluentAssertions;
using FourColourTheorem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourColourTheorem.Tests
{
    [TestClass]
    public class ColouringTests
    {
        [TestMethod]
        public void FindFirstAvailableColour_ShouldReturn0_IfRegionHasNoNeighbours()
        {
            // Arrange
            var region = new Region { Id = 0, Neighbours = [] };

            // Act
            var result = Services.Colours.DetermineFirstAvailableColour(region);

            // Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void FindFirstAvailableColour_ShouldReturn1_IfRegionHasNeighbourWithColour0()
        {
            // Arrange
            var region = new Region { Id = 0, Neighbours = [ new Neighbour { Id = 1, Colour = 0 } ] };

            // Act
            var result = Services.Colours.DetermineFirstAvailableColour(region);

            // Assert
            result.Should().Be(1);
        }

        [TestMethod]
        public void FindFirstAvailableColour_ShouldAllowForNumberHigherThan3()
        {
            // Arrange
            var region = new Region { Id = 0, Neighbours = 
                [
                    new Neighbour { Id = 1, Colour = 0 },
                    new Neighbour { Id = 2, Colour = 1 },
                    new Neighbour { Id = 3, Colour = 2 },
                    new Neighbour { Id = 4, Colour = 3 },
                ] };

            // Act
            var result = Services.Colours.DetermineFirstAvailableColour(region);

            // Assert
            result.Should().Be(4);
        }
    }
}
