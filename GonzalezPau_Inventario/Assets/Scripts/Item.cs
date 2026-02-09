using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id_item;
    
    public string nombre;

    public bool equipado;

    public int fuerza;

    public ClassItem createItem()
    {
         return new ClassItem (id_item, nombre, equipado, fuerza);
    }
}
