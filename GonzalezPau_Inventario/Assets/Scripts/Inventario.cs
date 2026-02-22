using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario 
{
    public Inventario(int id)
    {
        this.id_inventario = id;    
    }
    public int id_inventario { get; set; }

    public List<Item> items { get; set; }
}
