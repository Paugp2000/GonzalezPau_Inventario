using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform; 
    private CanvasGroup canvasGroup;
    public Transform objetosEquipablesParent;
    public Transform [] espacioInventarioParent = new Transform [6];
    public string nombreObjeto;
    public int fuerza;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < espacioInventarioParent.Length; i++) {
            canvasGroup.blocksRaycasts = true;
            if (transform.parent == canvas.transform)
            {
                transform.SetParent(objetosEquipablesParent);
                canvasGroup.alpha = 1f;
            }
            else if (transform.parent == espacioInventarioParent[i])
            {
                transform.position = espacioInventarioParent[i].position;
                canvasGroup.alpha = 1f;
            }
        }
    }

    private void Awake()
    {
        rectTransform = GetComponentInChildren<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    public string getNombreObjeto()
    {
        return nombreObjeto;
    }
    public int getFuerza()
    {
        return fuerza;
    }
    
}
