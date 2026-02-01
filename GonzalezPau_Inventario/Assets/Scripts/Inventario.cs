using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Inventario 
{
    public Inventario(int id)
    {
        this.id_inventario = id;    
    }
    [PrimaryKey, AutoIncrement] public int id_inventario { get; set; }
    [Indexed] public int id_item { get; set; }

    public List<Item> items { get; set; }
}
