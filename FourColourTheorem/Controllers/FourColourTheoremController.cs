using FourColourTheorem.Models;
using FourColourTheorem.Services;
using Microsoft.AspNetCore.Mvc;

namespace FourColourTheorem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FourColourTheoremController : ControllerBase
    {
        /// <summary>
        /// This method returns a possible colour arrangement for a flat map using 4 or fewer colours, based on the provided adjacency matrix.
        /// Returns an error if the matrix is not a valid flat map.
        /// </summary>
        /// <param name="input">The adjacency matrix where a 1 represents that two regions share a border and a 0 indicates that they do not.</param>
        /// <returns>An array with the region Id (iterative integer) and colour (integer)</returns>
        [HttpPost(Name = "GetColouring")]
        [ProducesResponseType<IEnumerable<Region>>(StatusCodes.Status200OK)]
        [ProducesResponseType<Error>(StatusCodes.Status400BadRequest)]
        public IActionResult Get(Input input)
        {
            var length = input.AdjacencyMatrix.Length;
            var result = new Region[length];

            // converts matrix
            for(var i = 0; i < length; i++) {
                if (input.AdjacencyMatrix[i].Length != length)
                {
                    return BadRequest(new Error { Message = $"Matrix length is {length} but the length of row {i} is {input.AdjacencyMatrix[i].Length}." });
                }

                // For ease of working, I am saving the colour and neighbours of each region in a Region object
                var region = new Region { Id = i, Neighbours = [] };
                for(var j = 0; j <length; j++)
                {
                    if (input.AdjacencyMatrix[i][j] == 1)
                    {
                        region.Neighbours = region.Neighbours.Append( new Neighbour { Id = j }).ToArray();

                    }
                }


                result[i] = region;
            }

            foreach(var region in result)
            {
                var colour = region.DetermineFirstAvailableColour();
                if (colour > 3)
                {
                    // I am not sure if this way of iterating and assinging colours can come to the conclusion it needs more than 4
                    // even when the input matrix is a flat map, I did not have the time to find that out.
                    return BadRequest(new Error { Message = "We need more than four colours. Are you sure your input matrix is a flat map?" });
                }

                region.Colour = colour;

                // Updates the neighbours
                // In another language like C++ I would have used a reference so the colour inside Neighbours refers to the colour of the region, 
                // which I tried doing with delegates here but did not manage. 
                // Another option would be to use the whole matrix when finding neighbour colours and only store the neighbour Id. 
                foreach(var neighbour in region.Neighbours)
                {
                    foreach(var neighbouringRegion in  result[neighbour.Id].Neighbours)
                    {
                        if (neighbouringRegion.Id == region.Id)
                        {
                            neighbouringRegion.Colour = colour;
                        }
                    }
                }
            }

            return Ok(result.ToOutput());
        }
    }
}
