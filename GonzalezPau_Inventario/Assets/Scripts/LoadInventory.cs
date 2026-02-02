using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInventory : MonoBehaviour
{
    private List<Item> itemsInInventory = new List<Item>();
    public Item[] allItems;
    public DatabaseInicializer databaseInicializer;
    private FindObjectsSortMode findObjectsSortMode;
    public Transform[] espacioInventarioParent = new Transform[6];

    /*void Start()
    {
        allItems = FindObjectsByType<Item>(findObjectsSortMode);
         
        foreach (Item item in allItems)
        {
            foreach (Transform t in espacioInventarioParent)
            {
                if (item.transform.parent = t.transform.parent)
                {
                    item.transform.position = t.transform.position; 
                }
            }
        }
    }*/
}
