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
    public int id_inventario { get; set; }

    public List<ClassItem> items { get; set; }
}
