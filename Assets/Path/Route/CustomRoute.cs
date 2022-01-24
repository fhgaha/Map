using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private Vector2 gizmosPosition;

    private void Start()
    {
        var list = new List<Transform>();
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            list.Add(child);
        }
        controlPoints = list.ToArray();
    }

    private void OnDrawGizmos()
    {
        var prevGizmosPosition = gizmosPosition;
        
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = GetBezierPoint(t,
                controlPoints[0].position,
                controlPoints[1].position,
                controlPoints[2].position,
                controlPoints[3].position);

            Gizmos.DrawSphere(gizmosPosition, 0.05f);
        }

        var handleFrom = new Vector2(controlPoints[0].position.x, controlPoints[0].position.y);
        var handleTo = new Vector2(controlPoints[1].position.x, controlPoints[1].position.y);
        Gizmos.DrawLine(handleFrom, handleTo);

        handleFrom = new Vector2(controlPoints[2].position.x, controlPoints[2].position.y);
        handleTo = new Vector2(controlPoints[3].position.x, controlPoints[3].position.y);
        Gizmos.DrawLine(handleFrom, handleTo);
    }

    public static Vector2 GetBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return
            Mathf.Pow(1 - t, 3) * p0 +
            3 * Mathf.Pow(1 - t, 2) * t * p1 +
            3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
            Mathf.Pow(t, 3) * p3;
    }
}
