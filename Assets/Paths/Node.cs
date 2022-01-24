using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform Value { get; set; }
    public List<Node> IncidentNodes;

    public Node(Transform value)
    {
        Value = value;
    }

    private void OnDrawGizmos()
    {
        foreach (Node n in IncidentNodes)
            Gizmos.DrawLine(n.transform.position, this.transform.position);
    }
}
