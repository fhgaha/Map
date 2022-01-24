using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCustomRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] route;
    private int routeToGo;
    private float tParam;
    private Vector2 soldierPosition;
    private float speedModifier;
    private bool coroutineAllowed;
    private Transform[] routeFromPaths;

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
            StartCoroutine(GoByRoute());
        //StartCoroutine(GoByRouteBezier(routeToGo));
    }

    private IEnumerator GoByRoute()
    {
        //var path = new Pathfinder().Path;
        //You are trying to create a MonoBehaviour using the 'new' keyword.This is not allowed.
        //MonoBehaviours can only be added using AddComponent(). Alternatively, your script can inherit
        //from ScriptableObject or no base class at all

        yield break;
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
