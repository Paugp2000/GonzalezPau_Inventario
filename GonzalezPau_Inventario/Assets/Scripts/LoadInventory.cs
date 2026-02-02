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


}
