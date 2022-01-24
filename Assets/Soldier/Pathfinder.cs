using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    public List<Node> Path { get; private set; }
    private List<Node> nodes;

    void Start()
    {
        Path = new List<Node>();
    }

    void Update()
    {
        if (Path.Count == 0)
        {
            if (nodes is null) nodes = Paths.GetAllNodes();
            Path = FindPath(nodes.First(), nodes.Last());
        }
    }

    private List<Node> FindPath(Node start, Node end)
    {
        var track = new Dictionary<Node, Node>();
        track[start] = null;
        var queue = new Queue<Node>();
        queue.Enqueue(start);
        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            foreach (var nextNode in node.IncidentNodes)
            {
                if (track.ContainsKey(nextNode)) continue;
                track[nextNode] = node;
                queue.Enqueue(nextNode);
            }
            if (track.ContainsKey(end)) break;
        }
        var pathItem = end;
        var result = new List<Node>();
        while (pathItem != null)
        {
            result.Add(pathItem);
            pathItem = track[pathItem];
        }
        result.Reverse();
        return result;
    }
}
