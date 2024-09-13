using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSkin : MonoBehaviour
{
    [SerializeField] Transform weapons;
    [SerializeField] Transform head;
    [SerializeField] Transform shields;
    [SerializeField] SkinnedMeshRenderer pants;

    public int weaponId = 0;
    public GameObject weaponItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerEquipItems()
    {
        foreach (Transform obj in weapons)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in head)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in shields)
        {
            Destroy(obj.gameObject);
        }

        int idWeapons = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Weapons");
        if (idWeapons > 0)
        {
            InitWeapons(idWeapons - 1);
        }
        int idHat = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Hat");
        if (idHat > 0)
        {
            InitHairs(idHat - 1);
        }
        int idShield = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Shield");
        if (idShield > 0)
        {
            InitShields(idShield - 1);
        }
        int idPants = ItemJsonDatabase.Instance.GetIdOfItemsEquiped("Pants");
        if (idPants > 0)
        {
            InitPants(idPants - 1);
        }
    }

    public void RandomEquipItems()
    {
        InitWeapons(Random.Range(0, ItemDatabase.Instance.weapons.Count));
        InitPants(Random.Range(0, ItemDatabase.Instance.pantsMaterial.Count));
        InitHairs(Random.Range(0, ItemDatabase.Instance.hairs.Count));
        InitShields(Random.Range(0, ItemDatabase.Instance.shields.Count));
    }

    public void InitWeapons(int id)
    {
        weaponId = id;
        GameObject weapon = ItemDatabase.Instance.GetWeaponsById(id);
        weaponItem = Instantiate(weapon, weapons);
    }

    public void InitPants(int id)
    {
        Material pant = ItemDatabase.Instance.GetPantsMaterialById(id);
        pants.material = pant;
    }

    public void InitHairs(int id)
    {
        GameObject hair = ItemDatabase.Instance.GetHairsById(id);
        Instantiate(hair, head);
    }

    public void InitShields(int id)
    {
        GameObject shield = ItemDatabase.Instance.GetShieldsById(id);
        Instantiate(shield, shields);
    }
}
