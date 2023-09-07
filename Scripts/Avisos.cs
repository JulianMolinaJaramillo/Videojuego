using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Avisos : MonoBehaviour
{
    public GameObject PanelAviso;
	public int IDAviso;
	public GameObject BotonAviso;
	private bool badera;
	public TextMeshProUGUI _texto;
	public Image _imagen;


	public Collider2D _colider;
	private Animator _animacion;
    private void Awake()
    {
		_colider = GetComponent<Collider2D>();
		_animacion = GetComponent<Animator>();
    }


	//public enum TipoDeAviso
 //   {
	//	Avisos,
	//	Libros,
 //   }

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
			BotonAviso.gameObject.SetActive(true);
		}
		
		if (collision.CompareTag("Player") && Input.GetKey(KeyCode.V))
		{
			if (IDAviso == 0)
			{
				_texto.text = "¡¡ ATAQUE PRINCIPAL !!\n\n PRESIONA LA TECLA \n\nPARA UTILIZAR TU ESPADA";
				_imagen.gameObject.SetActive(true);
			}

			if (IDAviso == 1)
			{
				_texto.text = "¡¡ Utiliza la tecla V \n\n     para interactuar ¡¡";
				_imagen.gameObject.SetActive(false);
			}

			if (IDAviso == 2)
			{
				_texto.text = "¡¡Utilizar item  \n\n  abre el inventario con la tecla ESC ¡¡";
				_imagen.gameObject.SetActive(false);
			}

			badera = true;
			_animacion.SetBool("Aviso", true);

			collision.GetComponent<MovimientoPlayer>().enabled = false;
			collision.GetComponent<Animator>().enabled = false;
            AudioManager.instancia.Pasos.Stop();

            PanelAviso.gameObject.SetActive(true);

			while (PanelAviso.GetComponent<CanvasGroup>().alpha < 1f)
			{
			   PanelAviso.GetComponent<CanvasGroup>().alpha += 0.05f;
				   
			yield return new WaitForSeconds(0.05f);					
			}

			    

            yield return new WaitForSeconds(2f);
			    
			collision.GetComponent<MovimientoPlayer>().enabled = true;
			collision.GetComponent<Animator>().enabled = true;
		}
	}


    //CUANDO SALE EL OBJETO DEL TRIGGER
    private IEnumerator OnTriggerExit2D(Collider2D colision)
	{
        if (colision.CompareTag("Player"))
        {
			BotonAviso.gameObject.SetActive(false);
		}

		if (badera == true)
        {
			if (colision.CompareTag("Player"))
			{

				while (PanelAviso.GetComponent<CanvasGroup>().alpha != 0f)
				{
					PanelAviso.GetComponent<CanvasGroup>().alpha -= 0.05f;
					_colider.GetComponent<Collider2D>().enabled = false;
					yield return new WaitForSeconds(0.05f);

				}

				PanelAviso.gameObject.SetActive(false);

				yield return new WaitForSeconds(2f);
				_colider.GetComponent<Collider2D>().enabled = true;
				_animacion.SetBool("Aviso", false);


				badera = false;
			}
		}
		
	}
}
