using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropBasura : MonoBehaviour, IDropHandler
{
    public string requiredTag = "Draggable";
    private Item itemResutante;
    private Inventario inventario;
    public DatabaseSaver sistemaDeGuardado;
   
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            itemResutante = dropped.GetComponent<Item>();
            inventario = sistemaDeGuardado.loadInvenario();
            inventario.items.Remove(itemResutante);
            Debug.Log("Removed");
            sistemaDeGuardado.RemoveItemFromDatabase(itemResutante);
            Debug.Log("RemoveFromDB");
        }
    }
}
