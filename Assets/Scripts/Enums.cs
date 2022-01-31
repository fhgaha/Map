using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Default,
    Border, //should not exist in when map will be complete
    Intersection
}

public enum CountryState
{
    Peace,
    War
}

public enum SoldierState
{
    Idle,
    GoToTarget,
    Attack,
    GoHome
}
