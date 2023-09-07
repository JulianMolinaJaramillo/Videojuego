using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar nuestro rect transform del UI
using UnityEngine.UI;
using TMPro;

public class DeteccionAldeanos : MonoBehaviour
{
	ManagerSingleton gameManager;
	PausaMenu MenuPausa;

	//Para asegurarnos que solo sea asequible desde el inspector y no desde otros script por error [SerializeField] 
	public GameObject MensajePanel;
	public TextMeshProUGUI _texto;
	public Button BotonAvanzarPagina;
	public Button BotonCerrarPagina;

	//PAra tener una referencia del NPC con el que estamos interactuando en este momento
	GameObject actualNPC;

	private int _contador = 1;
	private bool badera;

	private MovimientoPlayer _movimiento;
	private Animator _animator;

	public static DeteccionAldeanos instancia;

	private void Awake()
	{
		_movimiento = GetComponent<MovimientoPlayer>();
		_animator = GetComponent<Animator>();
	}
	void Start()
	{
		if (instancia == null)
		{
			instancia = this;
		}
		////para asegurarnos de que el Is trigger en el collider este activado
		//GetComponentInChildren<Collider2D>().isTrigger = true;
		gameManager = ManagerSingleton.instancia;
		MenuPausa = gameManager.GetComponent<PausaMenu>();
	}


	private IEnumerator OnTriggerStay2D(Collider2D colision)
	{
		if (colision.CompareTag("NPCAldeanos"))
		{
			actualNPC = colision.gameObject;
			if (badera == false)
			{
				actualNPC.transform.GetChild(0).gameObject.SetActive(true);
			}

			if (Input.GetKey(KeyCode.V))
			{
				MenuPausa.GetComponent<PausaMenu>().PuedeActivarPanel = true;
				_movimiento.GetComponent<MovimientoPlayer>().enabled = false;
				_animator.GetComponent<Animator>().SetBool("Walk", false);
				AudioManager.instancia.Pasos.Stop();
				AudioManager.instancia.PasosInteriores.Stop();

				actualNPC.transform.GetChild(0).gameObject.SetActive(false);
				badera = true;
				MensajePanel.SetActive(true);
				actualNPC.GetComponent<HablarNPC>().QuedarmeQuieto();

				while (MensajePanel.GetComponent<CanvasGroup>().alpha < 1f)
				{
					MensajePanel.GetComponent<CanvasGroup>().alpha += 0.05f;

					yield return new WaitForSeconds(0.05f);
				}

				BotonAvanzarPagina.gameObject.SetActive(true);
				string Mensaje = actualNPC.GetComponent<HablarNPC>().ConversacionNPC();
				_texto.text = Mensaje;
				//Para identificr al NPC
			}
		}
	}

	//CUANDO SALE EL OBJETO DEL TRIGGER
	private void OnTriggerExit2D(Collider2D colision)
	{
		if (colision.CompareTag("NPCAldeanos"))
		{
			//Verificamos que tengamos un actual NPC y efectuamos la accion pero no actualiza el mensaje
			if (actualNPC)
			{
				actualNPC.transform.GetChild(0).gameObject.SetActive(false);
				actualNPC.GetComponent<HablarNPC>().PuedoMoverme();
			}
		}
	}

	//CUANDO OPRIMIMOS LOS BOTONES SE EJECUTAN ESTOS CODIGOS
	//metodos llamados desde los botones del panel
	public void AdelantePagina()
	{
		if (actualNPC)
		{
			_contador += 1;
			actualNPC.GetComponent<HablarNPC>().PasarPaginas();
			//volvemos a actualizar el texto
			string Mensaje = actualNPC.GetComponent<HablarNPC>().ConversacionNPC();
			_texto.text = Mensaje;

			if (_contador == actualNPC.GetComponent<HablarNPC>().mensaje.Length)
			{
				BotonAvanzarPagina.gameObject.SetActive(false);
				BotonCerrarPagina.gameObject.SetActive(true);
			}
		}
	}

	public void CerrarConversacion()
	{
		if (actualNPC)
		{
			StartCoroutine("CerrarPanel", actualNPC);
		}
	}

	private IEnumerator CerrarPanel(GameObject objeto)
	{
		BotonCerrarPagina.gameObject.SetActive(false);

		while (MensajePanel.GetComponent<CanvasGroup>().alpha != 0f)
		{
			MensajePanel.GetComponent<CanvasGroup>().alpha -= 0.05f;
			yield return new WaitForSeconds(0.05f);
		}

		_texto.text = "";
		MensajePanel.SetActive(false);

		if (_movimiento.GetComponent<MovimientoPlayer>().EstoyEnInterior == false)
		{
			AudioManager.instancia.PlayAudio(AudioManager.instancia.Pasos);
		}
		else
		{
			AudioManager.instancia.PlayAudio(AudioManager.instancia.PasosInteriores);
		}

		_movimiento.GetComponent<MovimientoPlayer>().enabled = true;

		yield return new WaitForSeconds(0.5f);

		objeto.GetComponent<Collider2D>().enabled = false;
		badera = false;

		yield return new WaitForSeconds(2f);
		//Reestablecemos contador y collider
		_contador = 1;
		objeto.GetComponent<Collider2D>().enabled = true;


	}

}
