using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuerzaTotalCalculator : MonoBehaviour
{
    public int fuerzaTotal;
    public List <Item> itemList;
    public TextMeshProUGUI fuerzaNum;
    public GameObject canvasPanelfuerza;
    public DatabaseSaver puntoDeGuardado;

    private void Start()
    {
        puntoDeGuardado.loadItems().Clear();
    }
    public void CalcularFuerzaToral()
    {
        fuerzaTotal = 0;    
        itemList = puntoDeGuardado.loadItems();
        foreach (Item item in itemList) 
        {
            fuerzaTotal = fuerzaTotal + item.fuerza;
        }
        fuerzaNum.text = fuerzaTotal.ToString();    
        canvasPanelfuerza.SetActive(true);
    }
    public void EsconderPanelFuerza()
    {
        canvasPanelfuerza.SetActive (false);
    }
}
