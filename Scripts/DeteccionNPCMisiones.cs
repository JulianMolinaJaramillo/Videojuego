using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar nuestro rect transform del UI
using UnityEngine.UI;
using TMPro;

public class DeteccionNPCMisiones : MonoBehaviour
{
	ManagerSingleton gameManager;
	Inventario inventary;
	PausaMenu MenuPausa;

	//Para asegurarnos que solo sea asequible desde el inspector y no desde otros script por error [SerializeField] 
	public GameObject MensajePanel;
	public TextMeshProUGUI _texto;
	public Button BotonAvanzarPagina;
	public Button BotonCerrarPagina;
	public Button BotonAfirmativo;
	public Button BotonNegativo;

	//PAra tener una referencia del NPC con el que estamos interactuando en este momento
	GameObject actualNPC;
	public int IDnpcsMisiones;
	private bool identificarMision;

	private int _contador = 1;
	private bool badera;
	

	private MovimientoPlayer _movimiento;
	private Animator _animator;

	public static DeteccionNPCMisiones instancia;

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
		inventary = gameManager.GetComponent<Inventario>();
		MenuPausa = gameManager.GetComponent<PausaMenu>();
	}


    private IEnumerator OnTriggerStay2D(Collider2D colision)
    {
		if (colision.CompareTag("NPCMisiones") || colision.CompareTag("NPCMisiones2"))
		{
			actualNPC = colision.gameObject;
            if (badera == false)
            {
				actualNPC.transform.GetChild(0).gameObject.SetActive(true);
			}

			COmpletarMisionesGuardadas(actualNPC.GetComponent<HablarNPCMisiones>().idNPC);
			actualNPC.GetComponent<HablarNPCMisiones>().MisionCompletada();
			
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
				actualNPC.GetComponent<HablarNPCMisiones>().QuedarmeQuieto();
				while (MensajePanel.GetComponent<CanvasGroup>().alpha < 1f)
				{
					MensajePanel.GetComponent<CanvasGroup>().alpha += 0.05f;

					yield return new WaitForSeconds(0.05f);
				}

				//Para identificr al NPC
				if (actualNPC.GetComponent<HablarNPCMisiones>().idNPC == 1)
				{
                    if (actualNPC.GetComponent<HablarNPCMisiones>().MisionCompleted == true)
                    {
						BotonCerrarPagina.gameObject.SetActive(true);
						string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
						_texto.text = Mensaje;
					}else
                    if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == true)
                    {
						BotonCerrarPagina.gameObject.SetActive(true);
						string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
						_texto.text = Mensaje;
					}
                    else
                    {
						BotonAvanzarPagina.gameObject.SetActive(true);
						string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().ConversacionNPC();
						_texto.text = Mensaje;
					}
					
				}

				if (actualNPC.GetComponent<HablarNPCMisiones>().idNPC == 2)
				{
					inventary.GetComponent<Inventario>().EsUsable = true;
					inventary.GetComponent<Inventario>().IDPuertas = 3;

					if (actualNPC.GetComponent<HablarNPCMisiones>().MisionCompleted == true)
					{
						BotonCerrarPagina.gameObject.SetActive(true);
						string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
						_texto.text = Mensaje;
					}
					else
					if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == true)
					{
						BotonCerrarPagina.gameObject.SetActive(true);
						string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
						_texto.text = Mensaje;                     
					}
					else
					{
						BotonAvanzarPagina.gameObject.SetActive(true);
						string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().ConversacionNPC();
						_texto.text = Mensaje;
					}					
				}

                if (actualNPC.GetComponent<HablarNPCMisiones>().idNPC == 3)
                {
                    if (actualNPC.GetComponent<HablarNPCMisiones>().MisionCompleted == true)
                    {
                        BotonCerrarPagina.gameObject.SetActive(true);
                        string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
                        _texto.text = Mensaje;
                    }
                    else
                    if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == true)
                    {
                        BotonCerrarPagina.gameObject.SetActive(true);
                        string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
                        _texto.text = Mensaje;
                    }
                    else
                    {
                        BotonAvanzarPagina.gameObject.SetActive(true);
                        string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().ConversacionNPC();
                        _texto.text = Mensaje;
                    }

                }

            }
		}
	}

	//CUANDO SALE EL OBJETO DEL TRIGGER
	private void OnTriggerExit2D(Collider2D colision)
	{
		if (colision.CompareTag("NPCMisiones") || colision.CompareTag("NPCMisiones2"))
		{
            //Verificamos que tengamos un actual NPC y efectuamos la accion pero no actualiza el mensaje
            if (actualNPC)
            {
				actualNPC.transform.GetChild(0).gameObject.SetActive(false);
				actualNPC.GetComponent<HablarNPCMisiones>().PuedoMoverme();

				if (actualNPC.GetComponent<HablarNPCMisiones>().idNPC == 2)
				{
					inventary.GetComponent<Inventario>().EsUsable = false;
					MenuPausa.GetComponent<PausaMenu>().PuedeActivarPanel = false;
				}
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
			actualNPC.GetComponent<HablarNPCMisiones>().PasarPaginas();
			//volvemos a actualizar el texto
			string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().ConversacionNPC();
			_texto.text = Mensaje;

            if (_contador == actualNPC.GetComponent<HablarNPCMisiones>().mensaje.Length)
            {
				BotonAvanzarPagina.gameObject.SetActive(false);
				BotonAfirmativo.gameObject.SetActive(true);
				BotonNegativo.gameObject.SetActive(true);
			}
		}
	}

	public void CerrarConversacion()
    {
        if (actualNPC)
        {
			identificarMision = true;
			StartCoroutine("CerrarPanel", actualNPC);
		}	
	}

	public void BotonAceptarMision()
	{
		//Verificamos que tengamos un actual NPC y efectuamos la accion pero no actualiza el mensaje
		if (actualNPC)
		{
			actualNPC.GetComponent<HablarNPCMisiones>().BotonAfirmativo();
			//volvemos a actualizar el texto
			string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionAceptada;
			_texto.text = Mensaje;
		
			StartCoroutine("CerrarPanel", actualNPC);
			IDnpcsMisiones = actualNPC.GetComponent<HablarNPCMisiones>().idNPC;
            Experiencia.instancia.npcMisionesAceptadas(IDnpcsMisiones);		
		}

	}

	//metodo llamado desde boton rechazar mision
	public void BotonPosponerMision()
	{
		//Verificamos que tengamos un actual NPC
		if (actualNPC)
		{
			actualNPC.GetComponent<HablarNPCMisiones>().BotonNegativo();
			//volvemos a actualizar el texto
			string Mensaje = actualNPC.GetComponent<HablarNPCMisiones>().MisionRechazada;
			_texto.text = Mensaje;

			StartCoroutine("CerrarPanel", actualNPC);
		}
	}

	private IEnumerator CerrarPanel(GameObject objeto)
	{
		BotonAfirmativo.gameObject.SetActive(false);
		BotonNegativo.gameObject.SetActive(false);
		BotonCerrarPagina.gameObject.SetActive(false);

        if (identificarMision == true)
        {
			yield return new WaitForSeconds(1f);
			identificarMision = false;
		}
        else
        {
			yield return new WaitForSeconds(7f);
		}
		
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

	public void COmpletarMisionesGuardadas(int ID)
    {
		for (int i = 0; i < Experiencia.instancia.npcMisiones.Length; i++)
		{
			if (ID == Experiencia.instancia.npcMisiones[i])
			{
				actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted = true;			
			}

			if (ID == Experiencia.instancia.npcMisionesCompleted[i])
			{
				actualNPC.GetComponent<HablarNPCMisiones>().MisionCompleted = true;
			}
		}
	}

	public void ExperienciaAGanar()
	{
        if (actualNPC)
        {
			if (actualNPC.GetComponent<HablarNPCMisiones>().idNPC == 1)
			{
				if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == true)
				{
					Experiencia.instancia.ModificadorExperiencia(100);
				}
				if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == false)
				{
					Experiencia.instancia.ModificadorExperiencia(50);
				}
			}

			if (actualNPC.GetComponent<HablarNPCMisiones>().idNPC == 2)
			{
				if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == true)
				{
					Experiencia.instancia.ModificadorExperiencia(100);
				}
				if (actualNPC.GetComponent<HablarNPCMisiones>().MisionAcepted == false)
				{
					Experiencia.instancia.ModificadorExperiencia(50);
				}
			}
		}
	}
}
