using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoldierFollower : MonoBehaviour
{
    [SerializeField] private float speedModifier;

    public bool coroutineAllowed;
    private List<Node> path;
    public event Action OnEndOfPathIsReached;

    private void Start()
    {
        speedModifier = 1f;
        //coroutineAllowed = false;
    }

    private void Update()
    {
        //if (path != null)
            if (coroutineAllowed)
                StartCoroutine(GoByRoute());
    }

    public void GoToCountry(int id)
    {
        var countries = GameObject.Find("Countries").GetComponentsInChildren<Country>();
        Country myCountry = GetComponentInParent<Country>();
        Country targetCountry = countries.Single(c => c.Id == id);
        //path = FindPath(myCountry.NearestPathNode, enemyCountry.NearestPathNode);
        var start = GetNearestNode();
        var end = targetCountry.NearestPathNode;
        path = FindPath(myCountry.NearestPathNode, end);
        coroutineAllowed = true;
    }

    private Node GetNearestNode()
    {
        var nodes = GameObject.Find("Paths").GetComponent<Paths>().GetAllNodes();
        var minDistance = float.MaxValue;
        Node nearestNode = null;
        foreach (Node n in nodes)
        {
            var currentMinDistance = Vector3.Distance(n.transform.position, transform.position);
            if (currentMinDistance < minDistance)
            {
                minDistance = currentMinDistance;
                nearestNode = n;
            }
        }
        return nearestNode;
    }

    private IEnumerator GoByRoute()
    {
        coroutineAllowed = false;

        var currentPoint = path.First();
        var currentPointPos = currentPoint.transform.position;

        while (true)
        {
            if (Vector2.Distance(transform.position, path.Last().transform.position) <= 0.1)
            {
                path = null;
                OnEndOfPathIsReached?.Invoke();
                yield break;
            }

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
