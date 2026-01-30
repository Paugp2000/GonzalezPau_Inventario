using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public string requiredTag = "Draggable";
    public Transform objetoEquipableParent;
    private string nombreObjetoIntroducido;
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            nombreObjetoIntroducido = dropped.GetComponent<DraggableItem>().getNombreObjeto();
            Debug.Log(nombreObjetoIntroducido);
        }
        else
        {
            Debug.Log("Error");
        }
    }
 
}
