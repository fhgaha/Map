using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soldier : MonoBehaviour, ITarget
{
    private SoldierStateMachine _stateMachine;
    public Country HomeCountry;

    public Country Target;  //maybe ITarget -> Coutnry, Soldier

    private void Start()
    {
        var follower = GetComponent<SoldierFollower>();

        _stateMachine = new SoldierStateMachine();

        ///states
        var search = new SearchForTarget(this);
        var moveToSelected = new MoveToSelected(this, follower, Target.Id);
        var attack = new AttackTarget(this);

        ///transitions
        At(search, moveToSelected, HasTarget());
        At(moveToSelected, attack, ReachedTarget());
        At(attack, search, TargetSoldierDiedOrCountryConquered());

        _stateMachine.SetState(search);

        void At(ISoldierState from, ISoldierState to, Func<bool> condition) =>
            _stateMachine.AddTransition(from, to, condition);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> ReachedTarget() => () => Target != null 
            && Vector3.Distance(transform.position, Target.transform.position) <= 1;
        Func<bool> TargetSoldierDiedOrCountryConquered() => () => Target == null;
    }

    private void Update() => _stateMachine.Tick();
}
