using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pilares : MonoBehaviour
{

    private Animator interactuable;
    private bool badera;
    public int idPilar;
    public string elemento;

    private Collider2D _collider;
    //Variable para llamar a los metodos de ManagerPilares
    private ManagerPilares _managerPilares;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        interactuable = GetComponentInChildren<Animator>();
        _managerPilares = GetComponentInParent<ManagerPilares>();
    }

    //Al player entrar en el collider
	private IEnumerator OnTriggerStay2D(Collider2D collision)
	{
        if (badera == false && collision.CompareTag("Player"))
        {
            interactuable.SetBool("aviso", true);
            badera = true;
        }
        
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.V))
		{
            collision.GetComponent<MovimientoPlayer>().enabled = false;
            collision.GetComponent<Animator>().SetBool("Walk", false);
            AudioManager.instancia.Pasos.Stop();

            _managerPilares.GetComponent<ManagerPilares>().activarPanel(elemento);
            _managerPilares.GetComponent<ManagerPilares>().ActivacionPilares(idPilar);
        }
        
        yield return new WaitForSeconds(1f);
        
    }

    //Al player salir en el collider
    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (badera == true)
        {
            if (collision.CompareTag("Player"))
            {
                interactuable.SetBool("aviso", false);
            }
            badera = false;
            yield return new WaitForSeconds(1f);
        }
    }

    //Metodos que son llamados desde ManagerPilares
    public void ActivarPilar()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DesactivarPilar()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    //Metodo llamado desde ManagerPilares
    public void InhabilitarPilar()
    {
        _collider.GetComponent<Collider2D>().enabled = false;
    }

}
