using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Paths : MonoBehaviour
{
    [SerializeField]
    private List<Node> nodes;

    private void Awake()
    {
        GetAllNodes();
    }

    private void OnDrawGizmos()
    {
        GetAllNodes();
    }

    /// <summary>
    /// Returns all children nodes 
    /// </summary>
    public List<Node> GetAllNodes()
    {
        if (nodes == null || nodes.Count != transform.childCount)
        {
            nodes = new List<Node>();
            GetComponentsInChildren<Node>().ToList()
                .ForEach(child => nodes.Add(child));
        }
        return nodes;
    }
}

