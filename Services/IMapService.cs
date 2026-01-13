using MapApi.Models;

namespace MapApi.Services
{
    public interface IMapService
    {
        void SetMap(Graph graph);
        Graph GetMap();
        string? GetShortestRoute(string from, string to);
        int GetShortestDistance(string from, string to);
    }
}
