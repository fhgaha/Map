using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowRouteBezier : MonoBehaviour
{
    [SerializeField]
    private Transform[] route;
    private int routeToGo;
    private float tParam;
    private Vector3 soldierPosition;
    private float speedModifier;
    private bool coroutineAllowed;
    
    private void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.25f;
        coroutineAllowed = true;
    }

    private void Update()
    {

        if (coroutineAllowed)
            StartCoroutine(GoByRouteBezier(routeToGo));
    }

    private IEnumerator GoByRouteBezier(int routeNumber)
    {
        coroutineAllowed = false;

        var p0 = route[routeNumber].GetChild(0).position;
        var p1 = route[routeNumber].GetChild(1).position;
        var p2 = route[routeNumber].GetChild(2).position;
        var p3 = route[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            soldierPosition = CustomRoute.GetBezierPoint(tParam, p0, p1, p2, p3);
            transform.position = soldierPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo++;
        if (routeToGo > route.Length - 1)
            yield break;
        //routeToGo = 0;  //
        coroutineAllowed = true;
    }
}
