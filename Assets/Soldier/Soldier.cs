using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class Soldier : MonoBehaviour, ITarget
{
    public Country HomeCountry;
    public Country Target;  //maybe ITarget -> Coutnry, Soldier
    private SoldierStateMachine _stateMachine;
    public int Damage;
    public int Hp;

    private void Start()
    {
        Damage = 1;
        Hp = 3;

        SetUpStateMachine();
    }

    private void SetUpStateMachine()
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
        At(attack, moveToSelected, TargetConquered());

        _stateMachine.SetState(search);

        void At(ISoldierState from, ISoldierState to, Func<bool> condition) =>
            _stateMachine.AddTransition(from, to, condition);

        Func<bool> HasTarget() => () => Target != null;
        Func<bool> ReachedTarget() => () => Target != null
            && Vector3.Distance(transform.position, Target.NearestPathNode.transform.position) <= 0.1f;

        Func<bool> TargetConquered() => () => Target == HomeCountry;
    }

    private void Update() => _stateMachine.Tick();

    internal void StrikeTarget()
    {
        Target.Hp -= Damage;
        Debug.Log(Target.Hp);

        if (Target.Hp <= 0)
            Target = HomeCountry;
    }

    public void CheckIfItsTimeToDie()
    {
        if (Hp <= 0) Destroy(this.gameObject, 1f);

        if ((Target == HomeCountry || Target == null) 
            && (Vector3.Distance(transform.position, HomeCountry.NearestPathNode.transform.position) <= 1f))
            Destroy(this.gameObject, 1f);
    }
}
