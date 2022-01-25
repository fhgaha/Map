using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowCustomRoute : MonoBehaviour
{
    private float speedModifier;
    private bool coroutineAllowed;
    private List<Node> nodes;
    private List<Node> path;

    private void Start()
    {
        speedModifier = 0.25f;
        coroutineAllowed = true;
        nodes = Paths.GetAllNodes();
        path = FindPath(nodes.First(), nodes.Last());
    }

    private void Update()
    {

        if (coroutineAllowed)
            StartCoroutine(GoByRoute());
    }

    private IEnumerator GoByRoute()
    {
        coroutineAllowed = false;

        var currentPoint = path.First();
        var currentPointPos = currentPoint.transform.position;

        while (true)
        {
            if (Vector2.Distance(transform.position, path.Last().transform.position) <= 0.1)
                yield break;

            if (Vector2.Distance(transform.position, currentPointPos) <= 0.1)
                if (currentPoint != path.Last())
                {
                    currentPoint = path[path.IndexOf(currentPoint) + 1];
                    currentPointPos = currentPoint.transform.position;
                }

            var moveDir = (currentPointPos - transform.position).normalized;
            transform.position += moveDir * speedModifier * Time.deltaTime;
            yield return new WaitForEndOfFrame();
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
