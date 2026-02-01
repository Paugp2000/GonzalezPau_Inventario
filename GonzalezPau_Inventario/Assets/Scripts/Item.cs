using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Item 
{
    public Item(int id, string name, bool acumulable, int fuerza, int cantidad)
    {
        this.id_item = id;
        this.nombre = name;
        this.acumulable = acumulable;
        this.fuerza = fuerza;
        this.cantidad = cantidad;
        this.cantidad = cantidad;
     }
    [PrimaryKey, AutoIncrement] public int id_item { get; set; }

    public string nombre { get; set; }

    public bool acumulable { get; set; }
    public int fuerza { get; set; }

    public int cantidad { get; set; }
}
