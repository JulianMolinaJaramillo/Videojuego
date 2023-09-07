using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreTesoro : MonoBehaviour
{
    private Animator _animador;
    private CofreTesoro _Cofre;
    public Collider2D _colliderTrigger;

    public GameObject Animacion;
    public GameObject LuzTesoro, ParticulaTesoro, ParticulasBotella, ParticulaVacio;
    public GameObject ObjetoInstanciar;
    Vector3 PosicionDeInstancia;
    private bool EstaAbierto;
    public int IDCofre;

    private void Awake()
    {
        _animador = GetComponent<Animator>();
        _Cofre = GetComponent<CofreTesoro>();
        _colliderTrigger = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PosicionDeInstancia = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y + 1.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {     
        AbrirCofreAlCargar();
    }

    public void LateUpdate()
    {
        if (Experiencia.instancia.asignarDestruccionCofres == true)
        {
            Experiencia.instancia.asignarDestruccionCofres = false;
        }
    }

    public void AbrirCofreAlCargar()
    {
       for (int i = 0; i < Experiencia.instancia.CofresDestruibles.Length; i++)
       {
          if (IDCofre == Experiencia.instancia.CofresDestruibles[i])
          {
                _colliderTrigger.GetComponent<Collider2D>().enabled = false;
                _animador.SetBool("VaciarCofre", true);
                StartCoroutine(EsperarParaDestruccion());
          }
       }      
    }

    private IEnumerator EsperarParaDestruccion()
    {
        yield return new WaitForSeconds(15f);
        ParticulaVacio.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Destroy(LuzTesoro);
        Destroy(ParticulaTesoro);
        Destroy(Animacion);
        Destroy(ParticulasBotella);
        _Cofre.GetComponent<CofreTesoro>().enabled = false;
    }


    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (EstaAbierto == false)
        {
            Animacion.gameObject.SetActive(true);
        }
        
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.V))
		{
            
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Tesoro);
            _colliderTrigger.GetComponent<Collider2D>().enabled = false;
            _animador.SetBool("AbrirCofre", true);

            yield return new WaitForSeconds(0.8f);
            LuzTesoro.gameObject.SetActive(true);
            ParticulaTesoro.gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            if (EstaAbierto == false)
            {
                Instantiate(ObjetoInstanciar, PosicionDeInstancia, Quaternion.identity);
                AudioManager.instancia.PlayAudio(AudioManager.instancia.AparicionBotella);
                ParticulasBotella.gameObject.SetActive(true);
                Experiencia.instancia.CofreDestruir(IDCofre);
            }      

            EstaAbierto = true;

        }
	}

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animacion.gameObject.SetActive(false);


            yield return new WaitForSeconds(1f);

        }
    }


}
