using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassItem 
{
    public ClassItem(int id, string name, bool equip, int fuerza)
    {
        id = this.id_item;
        name = this.nombre;
        equip = this.equipado;
        fuerza = this.fuerza;   
    }

    public int id_item;

    public string nombre;

    public bool equipado;

    public int fuerza;
}
