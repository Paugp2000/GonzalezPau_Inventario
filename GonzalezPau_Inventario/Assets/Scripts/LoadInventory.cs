using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LoadInventory : MonoBehaviour
{
    
    public Inventario inventarioActual;
    public DatabaseInicializer databaseInicializer;
    public DatabaseSaver puntoDeGuardado;
    public DropArea [] dropArea;
    public void LoadIfDatabaseExists()
    {
        inventarioActual = puntoDeGuardado.loadInvenario();
        foreach (DropArea dropAreaO in dropArea)
        {
            dropAreaO.EstablecerInventario();
        }
      
    }
  

}
