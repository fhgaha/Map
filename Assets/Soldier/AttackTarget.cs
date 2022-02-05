using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackTarget : ISoldierState
{
    private Soldier soldier;

    public AttackTarget(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public void OnEnter()
    {
        Debug.Log("attact target on enter");
    }

    public void OnExit()
    {
        Debug.Log("attact target on exit");
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}
