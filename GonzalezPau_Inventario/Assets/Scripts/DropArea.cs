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
    private Item itemGuardado;
    private Inventario inventario;
    public DatabaseInicializer databaseInicializer;
    public LoadInventory inventory;
    public Canvas canvas;
    public DatabaseSaver sistemaDeGuardado;
    public Transform canvasTransform;
    public GameObject itemPrefab;
    private Vector3 position;
    public void EstablecerInventario()
    {
        inventario = sistemaDeGuardado.devolverInventario();

        if (inventario.items == null)
        {
            inventario.items = new List<Item>();
            return;
        }

        foreach (Item item in inventario.items)
        {
            // Solo cargar los items que pertenecen a esta DropArea
            if (item.dropZoneGuardado == numberOfDropArea)
            {
                // itemGuardado es simplemente el item del inventario
                itemGuardado = item;

                // Instanciar correctamente
                GameObject instancia = Instantiate(itemPrefab, position, Quaternion.identity);

                // Asignar datos a la instancia
                Item itemInstancia = instancia.GetComponent<Item>();
                itemInstancia.id_item = itemGuardado.id_item;
                itemInstancia.nombre = itemGuardado.nombre;
                itemInstancia.equipado = itemGuardado.equipado;
                itemInstancia.fuerza = itemGuardado.fuerza;
                itemInstancia.dropZoneGuardado = itemGuardado.dropZoneGuardado;

               
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null && dropped.CompareTag(requiredTag))
        {
            dropped.transform.SetParent(transform);
            dropped.GetComponentInChildren<RectTransform>().anchoredPosition = Vector2.zero;
            position = dropped.transform.position + new Vector3 (1,0,0);  
            itemResutante = dropped.GetComponent<Item>();
            itemResutante.dropZoneGuardado = numberOfDropArea;
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
