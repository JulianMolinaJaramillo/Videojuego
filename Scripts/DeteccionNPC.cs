using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar nuestro rect transform del UI
using UnityEngine.UI;
using TMPro;

public class DeteccionNPC : MonoBehaviour
{
	//Para asegurarnos que solo sea asequible desde el inspector y no desde otros script por error [SerializeField] 
	[SerializeField] GameObject MensajePanel;
	[SerializeField] TextMeshProUGUI textoNPC;
	[SerializeField] Button BotonAfirmativo;
	[SerializeField] Button BotonNegativo;

	//PAra tener una referencia del NPC con el que estamos interactuando en este momento
	GameObject actualNPC;
	private int IDnpcs;

	private int _contador;
	private int _contador2;

	private MovimientoPlayer _movimiento;
	private Animator _animator;



    private void Awake()
    {
		_movimiento = GetComponent<MovimientoPlayer>();
		_animator = GetComponent<Animator>();
	}
    void Start()
	{
		////para asegurarnos de que el Is trigger en el collider este activado
		//GetComponentInChildren<Collider2D>().isTrigger = true;

		//Le decimos que al iniciar el juego, el panel este oculto
		MensajePanel.SetActive(false);


	
	}


	//CUANDO UN OBJETO HA ENTRADO EN CONTACTO CON EL AREA
	private IEnumerator OnTriggerEnter2D(Collider2D colision)
	{
        if (colision.CompareTag("NPC"))
        {
			
			actualNPC = colision.gameObject;
			BotonAfirmativo.gameObject.SetActive(true);
			BotonNegativo.gameObject.SetActive(true);

			//Para posicionar al NPC
			if (transform.position.x > actualNPC.transform.position.x)
			{
				actualNPC.GetComponent<SpriteRenderer>().flipX = false;
			}

			if (transform.position.x < actualNPC.transform.position.x)
			{
				actualNPC.GetComponent<SpriteRenderer>().flipX = true;
			}

			//Para identificr al NPC
			if (actualNPC.GetComponent<MensajeNPC>().idNPC == 1)
            {
				string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
				textoNPC.text = Mensaje;
				MensajeNPC.instancia.textoBoton1.text = "Como lo sabes?";
				MensajeNPC.instancia.textoBoton2.text = "No confío en los extraños";
				IDnpcs = actualNPC.GetComponent<MensajeNPC>().idNPC;
			}

			if (actualNPC.GetComponent<MensajeNPC>().idNPC == 2)
			{
				string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
				textoNPC.text = Mensaje;
                MensajeNPC.instancia.textoBoton1.text = "No mucho, podrías decirme donde buscar?";
                MensajeNPC.instancia.textoBoton2.text = "Son asuntos que no te importan.";
				IDnpcs = actualNPC.GetComponent<MensajeNPC>().idNPC;
			}

			if (actualNPC.GetComponent<MensajeNPC>().idNPC == 3)
			{
				string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
				textoNPC.text = Mensaje;
				MensajeNPC.instancia.textoBoton1.text = "¿A qué te refieres con eso?";
				MensajeNPC.instancia.textoBoton2.text = "Solo hablas mierda";
				IDnpcs = actualNPC.GetComponent<MensajeNPC>().idNPC;
			}

			if (actualNPC.GetComponent<MensajeNPC>().idNPC == 4)
			{
				string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
				textoNPC.text = Mensaje;
				MensajeNPC.instancia.textoBoton1.text = "¿Como sabes que esta cerca?";
				MensajeNPC.instancia.textoBoton2.text = "No quiero que te metas en mi vida";
				IDnpcs = actualNPC.GetComponent<MensajeNPC>().idNPC;
			}

			if (actualNPC.GetComponent<MensajeNPC>().idNPC == 5)
			{
				string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
				textoNPC.text = Mensaje;
				MensajeNPC.instancia.textoBoton1.text = "Gracias haré todo lo necesario.";
				MensajeNPC.instancia.textoBoton2.text = "¿Empezaras con tus adivinanzas?";
				IDnpcs = actualNPC.GetComponent<MensajeNPC>().idNPC;
			}


			_movimiento.GetComponent<MovimientoPlayer>().enabled = false;
			_animator.GetComponent<Animator>().SetBool("Walk", false);
			AudioManager.instancia.Pasos.Stop();

			MensajePanel.SetActive(true);
			while (MensajePanel.GetComponent<CanvasGroup>().alpha < 1f)
			{
				MensajePanel.GetComponent<CanvasGroup>().alpha += 0.05f;

				yield return new WaitForSeconds(0.05f);
			}
		}
	}

	//CUANDO SALE EL OBJETO DEL TRIGGER
	private void OnTriggerExit2D(Collider2D colision)
	{
		if (colision.CompareTag("NPC"))
		{
			////Verificamos que tengamos un actual NPC y efectuamos la accion pero no actualiza el mensaje
			//if (actualNPC)
			//{
			//	actualNPC.GetComponent<MensajeNPC>().ReiniciarTexto();
			//	//volvemos a actualizar el texto
			//	string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
			//	textoNPC.text = Mensaje;

			//}
			_contador = 0;
			_contador2 = 0;
			//MensajePanel.SetActive(false);
			
		}
	}

	//CUANDO OPRIMIMOS LOS BOTONES SE EJECUTAN ESTOS CODIGOS

	public void BotonOK()
    {
        //Verificamos que tengamos un actual NPC y efectuamos la accion pero no actualiza el mensaje
        if (actualNPC)
        {
			actualNPC.GetComponent<MensajeNPC>().BotonAfirmativo();
			//volvemos a actualizar el texto
			string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
			textoNPC.text = Mensaje;
			_contador += 1;
			if (_contador == 2)
			{
				MensajePanel.SetActive(false);
				_movimiento.GetComponent<MovimientoPlayer>().enabled = true;
				StartCoroutine("DestruirNPC", actualNPC);
            }

			//Para cerrar la conversacion a la primera respuesta negativa
			if (_contador2 == 1 && _contador == 1)
			{
                MensajePanel.SetActive(false);
                _movimiento.GetComponent<MovimientoPlayer>().enabled = true;
				StartCoroutine("DestruirNPC", actualNPC);
			}
		}

		
    }

	public void BotonCancelar()
	{
		//Verificamos que tengamos un actual NPC
		if (actualNPC)
		{
			actualNPC.GetComponent<MensajeNPC>().BotonNegativo();
			//volvemos a actualizar el texto
			string Mensaje = actualNPC.GetComponent<MensajeNPC>().ConversacionNPC();
			textoNPC.text = Mensaje;
			_contador2 += 1;

			if (_contador2 == 2)
			{
				MensajePanel.SetActive(false);
				_movimiento.GetComponent<MovimientoPlayer>().enabled = true;
				StartCoroutine("DestruirNPC", actualNPC);
				
			}

		}
	}

	//Llamado desde Boton OK en esta misma clase
	private IEnumerator DestruirNPC(GameObject objeto)
    {
		objeto.GetComponent<Animator>().SetTrigger("Desaparecer");
		AudioManager.instancia.PlayAudio(AudioManager.instancia.Desaparecer);
		//Particulas
		actualNPC.transform.GetChild(0).gameObject.SetActive(true);
		objeto.GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(1f);

		objeto.GetComponent<SpriteRenderer>().enabled = false;
        
        yield return new WaitForSeconds(1.2f);
		actualNPC.transform.GetChild(2).gameObject.SetActive(false);
		AudioManager.instancia.GuardadoExplosion.pitch = 1.68f;
		AudioManager.instancia.PlayAudio(AudioManager.instancia.GuardadoExplosion);
		actualNPC.transform.GetChild(1).gameObject.SetActive(true);

		yield return new WaitForSeconds(2f);
		//Pasamos el id a guardar en experiencia
		Experiencia.instancia.npcDestruidos(IDnpcs);
		Destroy(actualNPC);
	}

}
