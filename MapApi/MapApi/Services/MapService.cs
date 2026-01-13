using MapApi.Models;

namespace MapApi.Services
{
    public class MapService : IMapService
    {
        private Graph? _currentGraph;
        private Dictionary<string, Dictionary<string, int>>? _adjacency;

        public void SetMap(Graph graph)
        {
            _currentGraph = graph;
            _adjacency = new();
            foreach (var edge in graph.Edges)
            {
                if (!_adjacency.ContainsKey(edge.FromId))
                    _adjacency[edge.FromId] = new();
                _adjacency[edge.FromId][edge.ToId] = edge.Weight;

                if (!_adjacency.ContainsKey(edge.ToId))
                    _adjacency[edge.ToId] = new();
                _adjacency[edge.ToId][edge.FromId] = edge.Weight; // Bi-directional
            }
        }

        public Graph GetMap() => _currentGraph ?? new();

        public string? GetShortestRoute(string from, string to)
        {
            ArgumentException.ThrowIfNullOrEmpty(from);
            ArgumentException.ThrowIfNullOrEmpty(to);
            if (_adjacency == null)
                throw new InvalidOperationException("Map has not been set");
            if (!_adjacency.ContainsKey(from) || !_adjacency.ContainsKey(to))
                throw new InvalidOperationException("Unknown node");
            if (_adjacency?.ContainsKey(from) != true || !_adjacency.ContainsKey(to)) return null;
            var (_, prev) = Dijkstra(from);
            if (!prev.ContainsKey(to)) return null;
            var path = new List<string> { to };
            for (var at = to; at != from; at = prev[at])
                path.Add(prev[at]);
            path.Add(from);
            path.Reverse();
            return string.Join("", path);
        }

        public int GetShortestDistance(string from, string to)
        {
            ArgumentException.ThrowIfNullOrEmpty(from);
            ArgumentException.ThrowIfNullOrEmpty(to);
            if (_adjacency == null)
                throw new InvalidOperationException("Map has not been set");
            if (!_adjacency.ContainsKey(from) || !_adjacency.ContainsKey(to))
                throw new InvalidOperationException("Unknown node");
            if (_adjacency?.ContainsKey(from) != true || !_adjacency.ContainsKey(to)) return -1;
            var (dist, _) = Dijkstra(from);
            return dist.TryGetValue(to, out var d) ? d : -1;
        }

        private (Dictionary<string, int> dist, Dictionary<string, string> prev) Dijkstra(string start)
        {
            var dist = new Dictionary<string, int> { [start] = 0 };
            var prev = new Dictionary<string, string>();
            var pq = new PriorityQueue<string, int>();

            foreach (var node in _adjacency!.Keys)
                if (node != start) dist[node] = int.MaxValue;

            pq.Enqueue(start, 0);

            while (pq.Count > 0)
            {
                pq.TryDequeue(out var u, out int priority);
                if (priority > dist[u]) continue;

                if (!_adjacency.TryGetValue(u, out var neighbors)) continue;
                foreach (var (v, weight) in neighbors)
                {
                    int alt = dist[u] + weight;
                    if (alt < dist.GetValueOrDefault(v, int.MaxValue))
                    {
                        dist[v] = alt;
                        prev[v] = u;
                        pq.Enqueue(v, alt);
                    }
                }
            }
            return (dist, prev);
        }
    }
}
