using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : MonoBehaviour, ISoldierState
{
    private void Start()
    {
        GetComponent<SoldierFollower>().OnEndOfPathIsReached += Attack;
    }

    public void Attack()
    {
        Debug.Log(this + " attack method");
    }

}
