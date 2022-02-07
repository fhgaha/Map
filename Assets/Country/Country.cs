using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Country : MonoBehaviour, ITarget
{
    public int Hp;
    [SerializeField] public int Id;
    [SerializeField] private GameObject soldierPrefab;
    public Node NearestPathNode;
    private Dictionary<int, CountryState> relations;
    public event Action<Country, Country, Vector3> OnWarDeclared;
    private bool conditionToStartWar = false;

    private void Awake()
    {
        Hp = 5;
    }

    private void Start()
    {
        FillRelations();
        if (Id == 0) conditionToStartWar = true;
    }

    private void Update()
    {
        if (conditionToStartWar)
            DeclareWarOn(this, 1);

        var collider = GetComponent<PolygonCollider2D>();


    }

    private void OnTriggerStay2D(Collider2D collider)
    {
            //Debug.Log("ontrigg");
        //if (collider.tag == "Soldier")
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
        relations[idEnemy] = CountryState.War;
        var cntrsParent = transform.parent;
        var cntrs = cntrsParent.GetComponentsInChildren<Country>();

        var enemyCountry = cntrs
            .Single(c => c.Id == idEnemy);

        OnWarDeclared?.Invoke(this, enemyCountry, NearestPathNode.transform.position);
    }

    public void MakePeaceWith(Country sender, int id)
    {
        relations[id] = CountryState.Peace;
    }
}
