using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public string requiredTag = "Draggable";
    public Transform objetoEquipableParent;
    private string nombreObjetoIntroducido;
    private int fuerzaObjeto;
    public int numberOfDropArea;
    private Item itemResutante;
    private Inventario inventario;
    public DatabaseInicializer databaseInicializer;
    public LoadInventory inventory;
    public Canvas canvas;
    public DatabaseSaver sistemaDeGuardado;

    void Start()
    {
        inventario = inventory.inventarioActual;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            Debug.Log(nombreObjetoIntroducido);
            itemResutante = dropped.GetComponent<Item>();
            inventario.items.Add(itemResutante);
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
