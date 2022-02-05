using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersSpawner : MonoBehaviour
{
    public GameObject countries;
    public GameObject soldierPrefab;

    void Awake()
    {
        foreach (Country country in countries.GetComponentsInChildren<Country>())
            country.OnWarDeclared += SpawnSoldier;
    }

    private void SpawnSoldier(Country homeCountry, Country enemyCountry, Vector3 position)
    {
        var soldier = Instantiate(soldierPrefab, position, Quaternion.identity, transform);
        soldier.GetComponent<Soldier>().HomeCountry = homeCountry;
        soldier.GetComponent<Soldier>().Target = enemyCountry;
    }
}
