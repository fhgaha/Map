using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Country : MonoBehaviour
{
    [SerializeField] public int Id;
    [SerializeField] private GameObject soldierPrefab;
    public Node NearestPathNode;
    private Dictionary<int, CountryState> relations;
    public event Action OnWarDeclared;
    private bool conditionToStartWar = true;

    private void Start()
    {
        FillRelations();
    }

    private void Update()
    {
        if (conditionToStartWar)
            DeclareWarOn(this, 1);
    }

    private void FillRelations()
    {
        relations = new Dictionary<int, CountryState>();
        var countries = GameObject.Find("Countries").GetComponentsInChildren<Country>()
            .Where(c => c != this);

        foreach (Country country in countries)
            relations.Add(country.Id, CountryState.Peace);
    }

    public void DeclareWarOn(Country sender, int idEnemy)
    {
        conditionToStartWar = false;
        OnWarDeclared?.Invoke();
        relations[idEnemy] = CountryState.War;

        var soldier = Instantiate(soldierPrefab, NearestPathNode.transform.position, 
            Quaternion.identity, transform);

        soldier.GetComponent<SoldierStateMachine>().enemyCountryId = idEnemy;
        soldier.GetComponent<SoldierStateMachine>().ChangeState(SoldierState.GoToTarget);
        //soldier.GetComponent<FollowState>().GoToCountry(idEnemy);
    }

    public void MakePeaceWith(Country sender, int id)
    {
        relations[id] = CountryState.Peace;
    }
}
