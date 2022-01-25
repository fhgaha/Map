using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour
{
    private Vector3 movePosition;

    public void SetPosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
    }

    private void Update()
    {
        var moveDir = (movePosition - transform.position).normalized;
        GetComponent<MoveTransformVelocity>().SetVelocity(moveDir);
    }
}
