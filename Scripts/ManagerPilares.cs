using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerPilares : MonoBehaviour
{
    //Variables para el orden de los pilares
    private int identificador;
    private int contador;
    private bool ordenCorrecto;
    private bool ordenCorrecto2;
    private bool ordenCorrecto3;

    //Variables para traer referencia a los objetos
    public bool pilarTierra;
    public bool pilarAgua;
    public bool pilarAire;
    public bool pilarFuego;
    public GameObject plTierra;
    public GameObject plAgua;
    public GameObject plAire;
    public GameObject plFuego;
    public GameObject Player;

    //Variables para manipular la activacion del Boss
    public GameObject Puerta;
    
    //Variables para manipular el HUB
    public GameObject PanelAviso;
    public Button BtnActivar;
    public Button BtnDesactivar;
    public TextMeshProUGUI _texto;


    //Creamos los metodos para ser llamados desde la clase Pilares
    public void activarPanel(string elemnto)
    {
        PanelAviso.SetActive(true);
        StartCoroutine(abrirPanel(elemnto));
    }

    //Abrimos el panel y activamos sus componenetes
    private IEnumerator abrirPanel(string elemento)
    {
        _texto.text = $"Es un pilar antiguo y desgastado con el tiempo, su simbología representa el elemento {elemento}, ¿deseas activarlo?";
        BtnActivar.gameObject.SetActive(true);
        BtnDesactivar.gameObject.SetActive(true);

        while (PanelAviso.GetComponent<CanvasGroup>().alpha < 1f)
        {
            PanelAviso.GetComponent<CanvasGroup>().alpha += 0.05f;

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void DesactivarPanel()
    {   
        StartCoroutine(cerrarPanel());  
    }

    //Cerramos el panel y desactivamos sus componenetes
    private IEnumerator cerrarPanel()
    {
        _texto.text = "";
        BtnActivar.gameObject.SetActive(false);
        BtnDesactivar.gameObject.SetActive(false);
        while (PanelAviso.GetComponent<CanvasGroup>().alpha != 0f)
        {
            PanelAviso.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }      
        PanelAviso.SetActive(false);
    }


    //Activacion pilares para identificar que pilar esta activando el player
    public void ActivacionPilares(int idPilar)
    {
        identificador = idPilar;
        if (idPilar == 1)
        {
            pilarTierra = true;
        }
        if (idPilar == 2)
        {
            pilarAgua = true;
        }
        if (idPilar == 3)
        {
            pilarAire = true;
        }
        if (idPilar == 4)
        {
            pilarFuego = true;
        }    
    }

    //Metodos que son llamados desde el boton ActivarPilar
    public void EncenderPilares()
    {
        if (pilarTierra == true)
        {
            plTierra.GetComponent<Pilares>().ActivarPilar();
        }
        if (pilarAgua == true)
        {
            plAgua.GetComponent<Pilares>().ActivarPilar();
        }
        if (pilarAire == true)
        {
            plAire.GetComponent<Pilares>().ActivarPilar();
        }
        if (pilarFuego == true)
        {
            plFuego.GetComponent<Pilares>().ActivarPilar();
        }
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Pilar);

        //retomamos movimiento del player que se deactivó en script Pílares
        Player.GetComponent<MovimientoPlayer>().enabled = true;
        DesactivarPanel();
    }

    public void IdentificarOrdenPilares()
    {
        if (pilarTierra == true && pilarAgua == false && pilarAire == false && pilarFuego == false)
        {
            //sonido
            ordenCorrecto = true;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Pilar);
            Debug.Log("1");
        }
        else if (pilarTierra == true && pilarAgua == true && pilarAire == false && pilarFuego == false && ordenCorrecto == true)
        {
            //sonido
            ordenCorrecto2 = true;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Pilar);
            contador -= 1;
            Debug.Log("2");
        }
        else if (pilarTierra == true && pilarAgua == true && pilarAire == true && pilarFuego == false && ordenCorrecto == true  && ordenCorrecto2 == true)
        {
            //sonido
            ordenCorrecto3 = true;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Pilar);
            contador -= 1;
            Debug.Log("3");        
        }
        else if (pilarTierra == true && pilarAgua == true && pilarAire == true && pilarFuego == true && ordenCorrecto == true && ordenCorrecto2 == true  && ordenCorrecto3 == true)
        {
            //sonido
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Pilar);
            Debug.Log("4");
            Puerta.GetComponent<PuertaBoss>().ActivacionPuertas();
        }

        contador += 1;

        //Si ninguna de las condiciones se cumplen
        
        if(ordenCorrecto == false || ordenCorrecto2 == false || ordenCorrecto3 == false)
        {
            if (contador >= 2)
            {
                AudioManager.instancia.Pilar.Stop();
                AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoError);
                plTierra.GetComponent<Pilares>().DesactivarPilar();
                plAgua.GetComponent<Pilares>().DesactivarPilar();
                plAire.GetComponent<Pilares>().DesactivarPilar();
                plFuego.GetComponent<Pilares>().DesactivarPilar();
                pilarTierra = false;
                pilarAgua = false;
                pilarAire = false;
                pilarFuego = false;
                contador = 0;
                ordenCorrecto = false;
                ordenCorrecto2 = false;
                ordenCorrecto3 = false;
            }         
        }
    }

    //Metodos que son llamados desde el boton DesactivarPilar para desactivar los pilares
    public void CancelarActivacion()
    {
        if (identificador == 1)
        {
            plTierra.GetComponent<Pilares>().DesactivarPilar();
            pilarTierra = false;
            ordenCorrecto = false;
        }
        if (identificador == 2)
        {
            plAgua.GetComponent<Pilares>().DesactivarPilar();
            pilarAgua = false;
            ordenCorrecto2 = false;
        }
        if (identificador == 3)
        {
            plAire.GetComponent<Pilares>().DesactivarPilar();
            pilarAire = false;
            ordenCorrecto3 = false;
        }
        if (identificador == 4)
        {
            plFuego.GetComponent<Pilares>().DesactivarPilar();
            pilarFuego = false;
        }
        AudioManager.instancia.PlayAudio(AudioManager.instancia.ApagarPilar);
        if (contador >= 1)
        {
            contador = 0;
        }

        //retomamos movimiento del player
        Player.GetComponent<MovimientoPlayer>().enabled = true;
        DesactivarPanel();       
    }

    //Metodo llamado desde puerta Boos
    public void CancelarPilares()
    {
        plTierra.GetComponent<Pilares>().InhabilitarPilar();
        plAgua.GetComponent<Pilares>().InhabilitarPilar();
        plAire.GetComponent<Pilares>().InhabilitarPilar();
        plFuego.GetComponent<Pilares>().InhabilitarPilar();
    }
    
}
