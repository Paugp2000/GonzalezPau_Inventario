using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario 
{
    public Inventario(int id)
    {
        id = this.idInventario;
    }
    public int idInventario;
    public List<Item> items;
}
