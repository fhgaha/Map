using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 2;
    float distanceTraveled;
    private Paths paths;

    private void Start()
    {
    }

    private void Update() 
    {
        distanceTraveled += speed * Time.deltaTime;
        //var points = Paths.Points.ToArray();
        //transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
    }
}
