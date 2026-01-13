using MapApi.Models;
using MapApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService) => _mapService = mapService;

        [HttpPost("SetMap")]
        public IActionResult SetMap([FromBody] Graph graph)
        {
            _mapService.SetMap(graph);
            return Ok();
        }

        [HttpGet("GetMap")]
        public IActionResult GetMap() => Ok(_mapService.GetMap());

        [HttpGet("ShortestRoute")]
        public IActionResult ShortestRoute([FromQuery] string from, [FromQuery] string to)
        {
            try
            {
                string? path = _mapService.GetShortestRoute(from.ToUpper(), to.ToUpper());
                return Ok(path);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ShortestDistance")]
        public IActionResult ShortestDistance([FromQuery] string from, [FromQuery] string to)
        {
            try
            {
                int dist = _mapService.GetShortestDistance(from.ToUpper(), to.ToUpper());
                return Ok(dist);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
