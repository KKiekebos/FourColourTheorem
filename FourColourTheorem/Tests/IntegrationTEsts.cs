using FluentAssertions;
using FourColourTheorem.Controllers;
using FourColourTheorem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FourColourTheorem.Tests
{
    [TestClass]
    public class IntegrationTEsts
    {
        private FourColourTheoremController controller = new FourColourTheoremController();

        [TestMethod]
        public void WhenGettingColoursWithProvidedExample_ColoursShouldMatchExpectedOutput()
        {
            // Arrange
            var input = new Input
            {
                AdjacencyMatrix = [
                    [
                      0, 1, 1, 1, 0
                    ],
                    [
                      1, 0, 1, 0, 0
                    ],
                    [
                      1, 1, 0, 1, 0
                    ],
                    [
                      1, 0, 1, 0, 1
                    ],
                    [
                      0, 0, 0, 1, 0
                    ]
                ]
            };

            // Act
            var result = ((controller.Get(input) as OkObjectResult).Value as IEnumerable<Colouring>).ToArray();

            // Assert
            result[0].Colour.Should().Be(0);
            result[1].Colour.Should().Be(1);
            result[2].Colour.Should().Be(2);
            result[3].Colour.Should().Be(1);
            result[4].Colour.Should().Be(0);
        }

        [TestMethod]
        public void WhenGettingColoursWithProvidedExample_ShouldThrowError_IfMatrixIsNotAFlatMap()
        {
            // Arrange
            var input = new Input
            {
                AdjacencyMatrix = [
                    [
                      1, 1, 1, 1, 1
                    ],
                    [
                      1, 1, 1, 1, 1
                    ],
                    [
                      1, 1, 1, 1, 1
                    ],
                    [
                      1, 1, 1, 1, 1
                    ],
                    [
                      1, 1, 1, 1, 1
                    ]
                ]
            };

            // Act & Assert
            var result = (controller.Get(input) as BadRequestObjectResult).Value as Error;

            // Assert
            result.Message.Should().Be("We need more than four colours. Are you sure your input matrix is a flat map?");
        }
    }
}
