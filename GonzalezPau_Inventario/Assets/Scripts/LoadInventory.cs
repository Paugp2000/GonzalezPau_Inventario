using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LoadInventory : MonoBehaviour
{
    public Item[] allItems;
    public Inventario inventarioActual;
    public DatabaseInicializer databaseInicializer;
    public Transform[] espacioInventarioParent = new Transform[6];
    public DatabaseSaver puntoDeGuardado;
    public void LoadIfDatabaseExists()
    {
        inventarioActual = puntoDeGuardado.loadInvenario();
    }

}
