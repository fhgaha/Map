using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector3 velocityVector;

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void Update()
    {
        transform.position += velocityVector * moveSpeed * Time.deltaTime;
        //playMoveAnim
    }
}
