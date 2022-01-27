using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum NodeType
{
    Default,
    Border, //should not exist in when map will be complete
    Intersection
}

public class Node : MonoBehaviour
{
    [SerializeField]
    private NodeType type;

    public List<Node> IncidentNodes;

    private void OnDrawGizmos()
    {
        var nodes = GetComponentInParent<Paths>().GetAllNodes();

        if (this.type != NodeType.Intersection)
            IncidentNodes = GetNearestNodes(nodes).ToList().Where(n => n != null).ToList();

        if (IncidentNodes != null)
        {
            foreach (Node n in IncidentNodes)
                DrawArrow(transform.position, n.transform.position - transform.position, 0.05f);
        }
    }

    private void Start()
    {
    }

    private Node[] GetNearestNodes(List<Node> nodes)
    {
        var TwoNearestNodes = new Node[2];

        var minDistance1 = float.MaxValue;
        var minDistance2 = float.MaxValue;
        Node first = null;
        Node second = null;

        foreach (Node n in nodes)
        {
            if (n == this) continue;

            var currentMinDistance = Vector3.Distance(n.transform.position, transform.position);

            if (currentMinDistance < minDistance1)
            {
                minDistance2 = minDistance1;
                minDistance1 = currentMinDistance;

                second = first;
                first = n;
            }
            else if (currentMinDistance < minDistance2 && currentMinDistance != minDistance1)
            {
                minDistance2 = currentMinDistance;
                second = n;
            }
        }

        TwoNearestNodes[0] = first;

        if (this.type == NodeType.Default)
            TwoNearestNodes[1] = second;

        return TwoNearestNodes;
    }

    public static void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(pos, direction);
        DrawArrowEnd(true, pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle);
    }

    private static void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 up = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back;
        Vector3 down = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back;

        if (gizmos)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, up * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, down * arrowHeadLength);
        }
        else
        {
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, up * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, down * arrowHeadLength, color);
        }
    }
}
