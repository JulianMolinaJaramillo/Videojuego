using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausaMenu : MonoBehaviour
{

    public GameObject PauseMenu;
    public GameObject PaneldescripcionObjetos;
    public TextMeshProUGUI Descripcion;

    public bool Pausado;
    public bool PuedeActivarPanel;
    //public TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Pausado = false;
    }



    // Update is called once per frame
    void Update()
    {
        Pausa();
    }

    public void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Pausado)
        {
            Pausar();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Pausado)
        {
            Despausar();        
        }
    }

    public void Pausar()
    {
        if (PuedeActivarPanel)
        {
            DeteccionNPCMisiones.instancia.MensajePanel.gameObject.SetActive(false);
        }

        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        Pausado = true;
    }
    public void Despausar()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Pausado = false;
        DesactivarDescripcion();

        if (PuedeActivarPanel)
        {
            DeteccionNPCMisiones.instancia.MensajePanel.gameObject.SetActive(true);
        }
    }

    public void PausarTienda()
    {
        PauseMenu.SetActive(true);
        Pausado = true;
    }

    public void DespausarTienda()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Pausado = false;
    }


    public void ActivarDescripcion(string texto)
    {
        PaneldescripcionObjetos.gameObject.SetActive(true);
        Descripcion.text = texto;
    }

    public void DesactivarDescripcion()
    {
        Descripcion.text = "";
        PaneldescripcionObjetos.gameObject.SetActive(false);
    }
}
