using System;
using System.Linq;
using UnityEngine;

internal class SearchForTarget: ISoldierState
{
    private Soldier soldier;
    public int TargetId;

    public SearchForTarget(Soldier soldier, int targetId)
    {
        this.soldier = soldier;
        TargetId = targetId;
    }
    
    public void Tick()
    {
        soldier.TargetCountry = ChooseTarget(TargetId);
    }

    private Country ChooseTarget(int targetId)
    {
        return GameObject.Find("Countries")
            .GetComponentsInChildren<Country>()
            .Single(c => c.Id == targetId);
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}