using System;
using System.Linq;
using UnityEngine;

internal class SearchForTarget : ISoldierState
{
    private Soldier soldier;

    public SearchForTarget(Soldier soldier)
    {
        this.soldier = soldier;
        //soldier.Target = ChooseTarget();
    }

    public void Tick()
    {
        //soldier.Target = ChooseTarget();
    }

    //private Country ChooseTarget()
    //{
        //var target = GameObject.Find("Countries")
        //    .GetComponentsInChildren<Country>()
        //    .Where(c => c.Id == soldier.Target.Id)
        //    .FirstOrDefault();

        //foreach (Country country in GameObject.Find("Countries").GetComponentsInChildren<Country>())
        //    if (country.Id == soldier.Target.Id)
        //        return country;

        //return null;
    //}

    public void OnEnter() { }
    public void OnExit() { }
}