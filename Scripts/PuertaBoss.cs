using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaBoss : MonoBehaviour
{
    public GameObject Camara1;
    public GameObject Camara2;
    public GameObject Camara3;
    public GameObject Camara4;
    public GameObject Player;
    public GameObject Boss;
    public GameObject Niebla;
    public GameObject Niebla2;
    public GameObject WarpFinal;

    //Para la demo
    public GameObject ManagerPilares;

    //Metodo que es llamado de ManagerPilares
    public void ActivacionPuertas()
    {
        StartCoroutine(PreparacionBoss());
    }

    //Secuencia de activacion del Boos
    private IEnumerator PreparacionBoss()
    {
        AudioManager.instancia.Bosque.Stop();
        AudioManager.instancia.PlayAudio(AudioManager.instancia.PeleaBoos);
        Player.GetComponent<MovimientoPlayer>().enabled = false;
        Niebla.gameObject.SetActive(false);
        Niebla2.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        Camara1.gameObject.SetActive(false);
        Camara2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        AudioManager.instancia.PlayAudio(AudioManager.instancia.Ramas);
        transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Crecer", true);
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("Crecer", true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        AudioManager.instancia.Ramas.Stop();
        Camara2.gameObject.SetActive(false);
        Camara3.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        //activamos bos
        Boss.GetComponent<ArbolBoss>().ActivarArbol = true;

        yield return new WaitForSeconds(3f);

        Camara3.gameObject.SetActive(false);
        Camara4.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        Player.GetComponent<MovimientoPlayer>().enabled = true;
    }

    //Metodo llamado desde BoosHealth
    public void DetenerSecuencia()
    {  
        StartCoroutine(DetencionBoss());
    }

    private IEnumerator DetencionBoss()
    {
        Player.GetComponent<MovimientoPlayer>().enabled = false;
        Player.GetComponent<Animator>().SetBool("Walk", false);
        AudioManager.instancia.Pasos.Stop();
        

        Camara4.gameObject.SetActive(false);
        Camara3.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.7f);

        Boss.GetComponent<Animator>().SetBool("Dead", true);
        AudioManager.instancia.Volumen(1);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Bosque);

        yield return new WaitForSeconds(4f);

        Camara3.gameObject.SetActive(false);
        Camara2.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        AudioManager.instancia.PlayAudio(AudioManager.instancia.Ramas);
        transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Crecer", false);
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("Crecer", false);
        transform.GetChild(2).gameObject.GetComponent<Animator>().SetBool("Hide", true);
        transform.GetChild(2).gameObject.GetComponent<Collider2D>().enabled = false;
        transform.GetChild(3).gameObject.GetComponent<Animator>().SetBool("Hide", true);
        Niebla.gameObject.SetActive(true);
        Niebla2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        Camara2.gameObject.SetActive(false);
        Camara1.gameObject.SetActive(true);
        Player.GetComponent<MovimientoPlayer>().enabled = true;
        ManagerPilares.GetComponent<ManagerPilares>().CancelarPilares();
        WarpFinal.gameObject.SetActive(true);
    }

}
