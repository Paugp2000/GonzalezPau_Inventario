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
    public Canvas canvas;

    void Start()
    {
         inventario = new Inventario(databaseInicializer.idUsuarioInventario); 
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            nombreObjetoIntroducido = dropped.GetComponent<DraggableItem>().getNombreObjeto();
            fuerzaObjeto = dropped.GetComponent<DraggableItem>().getFuerza();
            Debug.Log(nombreObjetoIntroducido);
            itemResutante = new Item(numberOfDropArea, nombreObjetoIntroducido, fuerzaObjeto);
            inventario.items.Add(itemResutante); 
            if(dropped.transform.parent = canvas.transform)
            {
                inventario.items.Remove(itemResutante);
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }
 
}
