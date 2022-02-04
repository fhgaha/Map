using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private SoldierStateMachine _stateMachine;

    public Country TargetCountry;  //maybe ITarget -> Coutnry, Soldier

    private void Awake()
    {
        _stateMachine = new SoldierStateMachine();

        ///states
        var search = new SearchForTarget(this, 2);
        var moveToSelected = new
    }

    private void Update() => _stateMachine.Tick();
}
