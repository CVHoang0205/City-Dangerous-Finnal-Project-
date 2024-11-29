using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> hairs = new List<GameObject>();
    public List<Material> pantsMaterial = new List<Material>();
    public List<GameObject> shields = new List<GameObject>();
    public List<Bullet> bullets = new List<Bullet>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetWeaponsById(int id)
    {
        return weapons[id];
    }

    public GameObject GetHairsById(int id)
    {
        return hairs[id];
    }

    public Material GetPantsMaterialById(int id)
    {
        return pantsMaterial[id];
    }

    public GameObject GetShieldsById(int id)
    {
        return shields[id];
    }
}
