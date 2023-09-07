using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPuertas : MonoBehaviour
{
	ManagerSingleton gameManager;
	Inventario inventary;
	public int IDPuerta;
	bool ValidarID;
	bool DestruccionPuertas;


	private Collider2D _Collider;
	private BotonInventario _BotonInventario;

	public static DetectorPuertas instancia;

    private void Awake()
    {
		_Collider = GetComponent<Collider2D>();
		_BotonInventario = GetComponent<BotonInventario>();
	}
    private void Start()
	{
	
		if (instancia == null)
		{
			instancia = this;
		}

		gameManager = ManagerSingleton.instancia;
		inventary = gameManager.GetComponent<Inventario>();
	}

	private void Update()
	{
		if (DestruccionPuertas == true)
		{
			Abrir();
		}

		AbrirPuertaAlCargar();
	}

	public void LateUpdate()
	{
		if (Experiencia.instancia.asignarDestruccionPuertas == true)
		{
			Experiencia.instancia.asignarDestruccionPuertas = false;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.CompareTag("Player"))
		{
			transform.GetChild(3).gameObject.SetActive(true);
			inventary.GetComponent<Inventario>().EsUsable = true;
            inventary.GetComponent<Inventario>().IDPuertas = IDPuerta;
		}

		if (Input.GetKey(KeyCode.V))
		{
			AudioManager.instancia.PlayAudio(AudioManager.instancia.PuertaBloqueada);
		}
	}


    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.CompareTag("Player"))
		{
			transform.GetChild(3).gameObject.SetActive(false);
			inventary.GetComponent<Inventario>().EsUsable = false;
		}
	}

	public void Abrir()
    {
		if (ValidarID == false)
		{
			this.transform.GetChild(0).gameObject.SetActive(false);
			this.transform.GetChild(1).gameObject.SetActive(true);
			this.transform.GetChild(2).gameObject.SetActive(true);
			this.transform.GetChild(3).gameObject.SetActive(false);
			StartCoroutine(DisableCollider());
			
            if (DestruccionPuertas == false)
            {
				Experiencia.instancia.PuertaDestruir(IDPuerta);
			}		
			ValidarID = true;
			
		}
	}

	private IEnumerator DisableCollider()
    {
		yield return new WaitForSeconds(0.01f);
		_Collider.GetComponent<Collider2D>().enabled = false;
		
	}

	public void AbrirPuertaAlCargar()
    {
		for (int i = 0; i < Experiencia.instancia.PuertasDestruibles.Length; i++)
		{
			if (IDPuerta == Experiencia.instancia.PuertasDestruibles[i])
			{
				DestruccionPuertas = true;
			}
		}
	}
}
