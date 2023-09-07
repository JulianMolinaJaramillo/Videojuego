using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class texto : MonoBehaviour
{
    public TextMeshProUGUI textoToggle;
    private bool vendido = true;

    private void Awake()
    {
        textoToggle = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void itemVendido()
    {
        if(vendido == true)
        {
            textoToggle.text = "Vender Objetos";
            vendido = false;
        }else if(vendido == false)
        {
            textoToggle.text = "Comprar Objetos";
            vendido = true;
        }
         
    }

}
