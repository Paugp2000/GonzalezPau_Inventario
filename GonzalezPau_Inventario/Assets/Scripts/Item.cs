using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public Item(int id, string name, int fuerza)
    {
        this.id = id;
        this.name = name;
        this.fuerza = fuerza;
    }
    
    public int id { get; set; }
    public string name { get; set; }
    public int fuerza { get; set; }
}
