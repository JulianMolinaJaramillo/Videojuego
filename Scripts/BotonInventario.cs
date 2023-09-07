using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class BotonInventario : MonoBehaviour , IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler
{
    public int ID;
    public int IDnpcAguardar;

    Inventario inventary;
    PausaMenu Menupausa;
    ManagerSingleton gameManager;

    public int HealthADar;
    public float MonedasADar;

    public static BotonInventario instancia;


    GameObject Puerta1;
    GameObject Puerta2;
    GameObject NPC;

    void Start()
    {
        Puerta1 = GameObject.FindGameObjectWithTag("Puerta1");
        Puerta2 = GameObject.FindGameObjectWithTag("Puerta2");
        NPC = GameObject.FindGameObjectWithTag("NPCMisiones2");

        if (instancia == null)
        {
            instancia = this;
        }


        gameManager = ManagerSingleton.instancia;
        inventary = gameManager.GetComponent<Inventario>();
        Menupausa = gameManager.GetComponent<PausaMenu>();
    }

    public void NPCMisiones(GameObject NPC)
    {

    }

    public void usaritem()
    {
        inventary.UsarItemsInventario(gameObject.name);
    }

    public void UseBotton()
    {
        if(ID == 0)
        {
            HealthPlayer.instancia.gameObject.GetComponent<HealthPlayer>().AdherirSalud(HealthADar);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Salud);
        }

        if (ID == 1)
        {
            Banco.instancia.Money(MonedasADar);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Coins);
        }

        if (ID == 3 && inventary.GetComponent<Inventario>().EsUsable == true && inventary.GetComponent<Inventario>().IDPuertas == 1)
        {
            AudioManager.instancia.SonidoError.Stop();
            Puerta1.GetComponent<DetectorPuertas>().Abrir();
            AudioManager.instancia.PlayAudio(AudioManager.instancia.PuertaAbierta);
            Menupausa.GetComponent<PausaMenu>().DesactivarDescripcion();
        }

        if (ID == 4 && inventary.GetComponent<Inventario>().EsUsable == true && inventary.GetComponent<Inventario>().IDPuertas == 2)
        {
            Menupausa.GetComponent<PausaMenu>().DesactivarDescripcion();
            AudioManager.instancia.SonidoError.Stop();
            Puerta2.GetComponent<DetectorPuertas>().Abrir();
            AudioManager.instancia.PlayAudio(AudioManager.instancia.PuertaAbierta);
            DeteccionNPCMisiones.instancia.ExperienciaAGanar();           
        }

        if (ID == 5 && inventary.GetComponent<Inventario>().EsUsable == true && inventary.GetComponent<Inventario>().IDPuertas == 3)
        {
            Menupausa.GetComponent<PausaMenu>().Despausar();
            AudioManager.instancia.SonidoError.Stop();
            NPC.GetComponent<HablarNPCMisiones>().MisionCompleted = true;
            Experiencia.instancia.npcMisionesCompletdas(2);           
            DeteccionNPCMisiones.instancia.BotonAvanzarPagina.gameObject.SetActive(false);
            DeteccionNPCMisiones.instancia.BotonAfirmativo.gameObject.SetActive(false);
            DeteccionNPCMisiones.instancia.BotonNegativo.gameObject.SetActive(false);
            DeteccionNPCMisiones.instancia.BotonCerrarPagina.gameObject.SetActive(true);
            NPC.GetComponent<HablarNPCMisiones>().MisionCompletada();
            DeteccionNPCMisiones.instancia.ExperienciaAGanar();
            DeteccionNPCMisiones.instancia._texto.text = NPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
        }


    }

    public void cambiarestado()
    {
        inventary.GetComponent<Inventario>().CambiarEstado();     
    }

    public void cambiarEstado2()
    {
        if (ID == 3)
        {
            inventary.GetComponent<Inventario>().VerificarPuerta(ID);
        }

        if (ID == 4)
        {
            inventary.GetComponent<Inventario>().VerificarPuerta(ID);
        }

        if (ID == 5)
        {
            inventary.GetComponent<Inventario>().VerificarPuerta(ID);
        }
    }

    //public void OnMouseEnter()
    //{
    //    Debug.Log("Mouse dentra");
    //}

    //private void OnMouseDown()
    //{
    //    Debug.Log("Mouse oprime");
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (ID == 0)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Objeto potente de curación, acelera el cicatrizado de las heridas.");
        }

        if (ID == 1)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Piedra de sangre preciosa, pareciera que se puede romper, ¿o tal vez la pueda vender?.");
        }

        if (ID == 3)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Llave antigua, puede que me pueda servir para algo. ¿Me pregunto que abrirá?.");
        }

        if (ID == 4)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Llave extraña, parece que es de algún tipo de bodega.");
        }

        if (ID == 5)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Collar muy valioso, parece pertenecerle a alguien.");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Menupausa.GetComponent<PausaMenu>().DesactivarDescripcion();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Menupausa.GetComponent<PausaMenu>().DesactivarDescripcion();

        if (ID == 3)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Aqui no se puede usar este objeto");
            AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoError);
        }

        if (ID == 4)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Aqui no se puede usar este objeto");
            AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoError);
        }

        if (ID == 5)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Aqui no se puede usar este objeto");
            AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoError);
        }
    }
}
