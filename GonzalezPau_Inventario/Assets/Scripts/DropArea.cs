using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public string requiredTag = "Draggable";
    public Transform objetoEquipableParent;
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
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            itemResutante = dropped.GetComponent<Item>();
            Debug.Log(itemResutante.nombre);
            inventario.items.Add(itemResutante.GetComponent<Item>());
            sistemaDeGuardado.AddItemToDatabase(itemResutante);

            if(dropped.transform.parent = canvas.transform)
            {
                inventario.items.Remove(itemResutante);
                sistemaDeGuardado.RemoveItemFromDatabase(itemResutante);    
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
