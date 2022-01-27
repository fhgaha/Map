using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Paths : MonoBehaviour
{
    public static List<Node> Nodes { get; private set; }

    private void OnDrawGizmos()
    {
        if (Nodes == null)
        {
            Nodes = new List<Node>();
            GetComponentsInChildren<Node>().ToList()
                .ForEach(child => Nodes.Add(child));
        }
    }

    /// <summary>
    /// Returns all children nodes 
    /// </summary>
    public static List<Node> GetAllNodes()
    {
        if (Nodes.Count == 0)
            throw new System.ArgumentNullException("Nodes is empty");
        return Nodes;
    }
}
