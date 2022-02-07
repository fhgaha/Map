using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackTarget : ISoldierState
{
    private Soldier soldier;
    private float nextStrikeTime = 1;

    public AttackTarget(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        if (soldier.Target != null)
            if (nextStrikeTime <= Time.time)
            {
                //strike every 2 sec
                nextStrikeTime = Time.time + 1f;
                soldier.StrikeTarget();
            }
    }
}
