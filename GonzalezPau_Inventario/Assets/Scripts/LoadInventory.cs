using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LoadInventory : MonoBehaviour
{
    public Item[] allItems;
    public Inventario inventarioActual;
    public DatabaseInicializer databaseInicializer;
    public DatabaseSaver puntoDeGuardado;
    public DropArea dropArea;
    public void LoadIfDatabaseExists()
    {
        inventarioActual = puntoDeGuardado.loadInvenario();
        dropArea.EstablecerInventario (inventarioActual);
    }
    public void loadEmpthyInventary(Inventario inventario)
    {
        inventarioActual = inventario;
    }
    

}
