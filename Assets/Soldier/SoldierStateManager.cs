using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierState { }

public class SoldierStateManager : MonoBehaviour
{
    private SoldierState state;
    public int homeCountryId;
    public int enemyCountryId;
    private bool going = false;

    void Start()
    {
        
    }

    void Update()
    {
        switch (state)
        {
            case SoldierState.Idle:
                break;
            case SoldierState.GoToTarget:
                if (!going)
                {
                    GetComponent<FollowState>().GoToCountry(enemyCountryId);
                    going = true;
                }
                break;
            case SoldierState.Attack:
                GetComponent<AttackState>().Attack();
                break;
            case SoldierState.GoHome:
                GetComponent<FollowState>().GoToCountry(homeCountryId);
                break;
        }
    }

    public void ChangeState(SoldierState state)
    {
        this.state = state;
    }
}
