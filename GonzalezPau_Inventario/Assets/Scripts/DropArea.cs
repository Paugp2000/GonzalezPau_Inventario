using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public string requiredTag = "Draggable";
    public int numberOfDropArea;
    private Item itemResutante;
    private Inventario inventario;
    public DatabaseInicializer databaseInicializer;
    public LoadInventory inventory;
    public Canvas canvas;
    public DatabaseSaver sistemaDeGuardado;

    public void EstablecerInventario(Inventario inventarioActual)
    {
        inventario = inventarioActual;
        inventario.items = inventarioActual.items;
        inventario.items = new List<Item>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            itemResutante = dropped.GetComponent<Item>();
            Debug.Log("Dropped " + itemResutante.nombre + ", Adding to DB");
            
            inventario.items.Add(itemResutante);
            sistemaDeGuardado.AddItemToDatabase(itemResutante);

            Debug.Log("Added to DB");

            if (dropped.transform.parent == canvas.transform)
            {
                inventario.items.Remove(itemResutante);
                Debug.Log("Remove?");
                sistemaDeGuardado.RemoveItemFromDatabase(itemResutante);
                Debug.Log("RemoveFromDB?");
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
