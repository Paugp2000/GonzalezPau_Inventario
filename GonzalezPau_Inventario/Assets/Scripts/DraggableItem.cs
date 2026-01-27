using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform; 
    private CanvasGroup canvasGroup;
    public  Transform objetosEquipablesParent;
    public Transform espacioInventarioParent;
    private GameObject[] dropzones;
    private string dropzoneTag = "dropZone";

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent == espacioInventarioParent)
        {
            transform.parent = objetosEquipablesParent;
        }
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
        canvasGroup.blocksRaycasts = true;
        if (transform.parent == canvas.transform) 
        { 
            transform.SetParent(objetosEquipablesParent);
            canvasGroup.alpha = 1f;
        }
        if (transform.parent == espacioInventarioParent) 
        {
            rectTransform.anchoredPosition = espacioInventarioParent.GetComponentInChildren<RectTransform>().position;
            canvasGroup.alpha = 1f;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponentInChildren<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        dropzones = GameObject.FindGameObjectsWithTag(dropzoneTag);
    }
    
}
