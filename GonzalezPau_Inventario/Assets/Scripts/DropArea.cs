using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DropArea : MonoBehaviour, IDropHandler
{
    public string requiredTag = "Draggable";
    public int numberOfDropArea;
    private Item itemResutante;
    public static Item itemAssignado;
    private Inventario inventario;
    public DatabaseInicializer databaseInicializer;
    public LoadInventory inventory;
    public Canvas canvas;
    public DatabaseSaver sistemaDeGuardado;
    public Transform canvasTransform;

    public void EstablecerInventario()
    {
        inventario = sistemaDeGuardado.devolverInventario();
        inventario.items = sistemaDeGuardado.loadItems();   
        if (inventario.items != null )
        {
            foreach (Item item in inventario.items)
            {
                if (itemAssignado.dropZoneGuardado == item.dropZoneGuardado)
                {
                    itemAssignado = item;   
                    item.transform.position = this.transform.position;
                }
            }
        }
        else
        {
            inventario.items = new List<Item>();
        }
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            itemResutante = dropped.GetComponent<Item>();
            itemResutante.dropZoneGuardado = numberOfDropArea;
            itemAssignado = itemResutante;
            Debug.Log("Dropped " + itemResutante.nombre + ", Adding to DB");
            
            inventario.items.Add(itemResutante);
            sistemaDeGuardado.AddItemToDatabase(itemResutante);

            Debug.Log("Added to DB");
 
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
