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
            string? path = _mapService.GetShortestRoute(from.ToUpper(), to.ToUpper());
            return path != null ? Ok(path) : NotFound("No path");
        }

        [HttpGet("ShortestDistance")]
        public IActionResult ShortestDistance([FromQuery] string from, [FromQuery] string to)
        {
            int dist = _mapService.GetShortestDistance(from.ToUpper(), to.ToUpper());
            return dist != -1 ? Ok(dist) : NotFound("No path");
        }
    }
}
