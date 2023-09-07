using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Libros : MonoBehaviour
{
	public GameObject PanelAviso;
	public GameObject AnimacionLibro;

	private bool badera;
	[SerializeField] string[] Paginas;
	public bool dosHojas;
	public int ContadorPaginas;

	public TextMeshProUGUI _texto;
	public TextMeshProUGUI _texto1;
	public TextMeshProUGUI _texto2;

	public Button _botonpasarPagina;
	public Button _botonCerrarPagina;

	public Collider2D _colider;
	private Animator _animacion;
	public GameObject Player;

	private void Awake()
	{
		_colider = GetComponent<Collider2D>();
		_animacion = GetComponent<Animator>();
	}

	private IEnumerator OnTriggerStay2D(Collider2D collision)
	{
        if (badera == false)
        {
			AnimacionLibro.gameObject.SetActive(true);
		}
		
		if (collision.CompareTag("Player") && Input.GetKey(KeyCode.V))
		{
			if (Paginas.Length == 1)
			{
				_botonpasarPagina.gameObject.SetActive(false);
				_botonCerrarPagina.gameObject.SetActive(true);
			}

			if (ContadorPaginas == 0)
			{
				_texto.gameObject.SetActive(true);
				_texto.text = Paginas[0];
			}

			if (Paginas.Length == 2)
			{
				_botonCerrarPagina.gameObject.SetActive(false);
				_texto1.text = Paginas[1];
				dosHojas = true;
			}

			if (Paginas.Length > 2)
			{
				_botonCerrarPagina.gameObject.SetActive(false);
				for (int i = 0; i < Paginas.Length; i++)
				{
					_texto1.text = Paginas[1];
					_texto2.text = Paginas[2];
				}
			}

			_animacion.SetBool("Aviso", true);
			AnimacionLibro.gameObject.SetActive(false);
			AudioManager.instancia.PlayAudio(AudioManager.instancia.PasoPagina);
			badera = true;

			collision.GetComponent<MovimientoPlayer>().enabled = false;
			collision.GetComponent<Animator>().enabled = false;

			AudioManager.instancia.PasosInteriores.Stop();
			AudioManager.instancia.Pasos.Stop();

			PanelAviso.gameObject.SetActive(true);

			while (PanelAviso.GetComponent<CanvasGroup>().alpha < 1f)
			{
				PanelAviso.GetComponent<CanvasGroup>().alpha += 0.1f;

				yield return new WaitForSeconds(0.05f);
			}

		}
	}


	//CUANDO SALE EL OBJETO DEL TRIGGER
	private IEnumerator OnTriggerExit2D(Collider2D colision)
	{
		AnimacionLibro.gameObject.SetActive(false);
		dosHojas = false;
		if (badera == true)
		{
			if (colision.CompareTag("Player"))
			{
                _texto.gameObject.SetActive(false);
                _texto.gameObject.SetActive(false);
                _texto2.gameObject.SetActive(false);

				_animacion.SetBool("Aviso", false);

				_botonpasarPagina.gameObject.SetActive(true);

				yield return new WaitForSeconds(1f);
				
				badera = false;
			}
		}
	}

	//Metodo llamado desde el BotonPasarPagina en el objeto Libros
	public void Pasarpagina()
    {
		ContadorPaginas += 1;

		if (ContadorPaginas == 1)
		{
			_texto.gameObject.SetActive(false);
			_texto1.gameObject.SetActive(true);
			AudioManager.instancia.PlayAudio(AudioManager.instancia.PasoPagina);

            if (dosHojas == true)
            {
				_botonpasarPagina.gameObject.SetActive(false);
				_botonCerrarPagina.gameObject.SetActive(true);
			}
		}

		if (ContadorPaginas == 2)
		{
			_texto2.gameObject.SetActive(true);
			_texto1.gameObject.SetActive(false);
			AudioManager.instancia.PlayAudio(AudioManager.instancia.PasoPagina);       
		}

		if (ContadorPaginas + 1 == 3)
		{
			_botonpasarPagina.gameObject.SetActive(false);
			_botonCerrarPagina.gameObject.SetActive(true);
		}

	}

	//Metodo llamado desde el BotonCerrarPagina en el objeto Libros
	public void CerrarLibro()
    {	
		_botonCerrarPagina.gameObject.SetActive(false);
		_texto1.gameObject.SetActive(false);
		StartCoroutine(ContadorDePaginas());
	}

	private IEnumerator ContadorDePaginas()
    {
		_colider.GetComponent<Collider2D>().enabled = false;
		_botonpasarPagina.gameObject.SetActive(false);

		while (PanelAviso.GetComponent<CanvasGroup>().alpha != 0f)
		{
			PanelAviso.GetComponent<CanvasGroup>().alpha -= 0.05f;
			yield return new WaitForSeconds(0.1f);
		}

		Player.GetComponent<Animator>().enabled = true;
		Player.GetComponent<MovimientoPlayer>().enabled = true;

		ContadorPaginas = 0;
		PanelAviso.gameObject.SetActive(false);

		yield return new WaitForSeconds(2f);
		_colider.GetComponent<Collider2D>().enabled = true;
		_botonpasarPagina.gameObject.SetActive(true);
	}

}
