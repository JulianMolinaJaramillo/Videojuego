using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    public string NombreObjeto;
    public int Reastauracion = 1;
    public float MonedasADar;

    public int IDColeccionable;
    private bool DestruccionColeccionable;


    [SerializeField] GameObject ParticulasLuz;
    [SerializeField] GameObject BurshParticulas;
    public GameObject AudioInterno;

    private SpriteRenderer _rederer;
    private Collider2D _colider;

    //public Collider2D _coliderAudio;
    // Start is called before the first frame update

    private void Awake()
    {
        _rederer = GetComponent<SpriteRenderer>();
        _colider = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && NombreObjeto == "Corazon")
        {
            //Salud del player
            collision.SendMessageUpwards("AdherirSalud", Reastauracion);
            //Llamamos el sonido
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Salud);

            //deshabilitar collider
            _colider.enabled = false;

            _rederer.enabled = false;
            ParticulasLuz.SetActive(false);
            BurshParticulas.SetActive(true);


            //StartCoroutine("ActivarObjeto");        
            StartCoroutine("DestruirObjeto");

        }

        if (collision.CompareTag("Player") && NombreObjeto == "Cristal")
        {
            
            Banco.instancia.Money(MonedasADar);
            //Llamamos el sonido
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Coins);

            // deshabilitar collider
            _colider.enabled = false;

            _rederer.enabled = false;
            ParticulasLuz.SetActive(false);
            BurshParticulas.SetActive(true);

            //StartCoroutine("ActivarObjeto");
            StartCoroutine("DestruirObjeto");

        }
    }


    //private IEnumerator ActivarObjeto()
    //{
    //    yield return new WaitForSeconds(5f);
    //    this.gameObject.SetActive(true);
    //    _rederer.enabled = true;
    //    _colider.enabled = true;
    //    ParticulasLuz.SetActive(true);
    //    BurshParticulas.SetActive(false);
    //}

    private IEnumerator DestruirObjeto()
    {
        AudioInterno.gameObject.GetComponent<AudioSource>().enabled = false;      
        yield return new WaitForSeconds(2f);    
        Experiencia.instancia.ColeccionablesDestruidos(IDColeccionable);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (DestruccionColeccionable == true)
        {
            Destroy(this.gameObject);
        }

        DestruccionColeccionableAlCargar();
    }

    public void DestruccionColeccionableAlCargar()
    {

        for (int i = 0; i < Experiencia.instancia.ColeccionablesDestruibles.Length; i++)
        {
            if (IDColeccionable == Experiencia.instancia.ColeccionablesDestruibles[i])
            {
                DestruccionColeccionable = true;

            }
        }

    }

    public void LateUpdate()
    {
        if (Experiencia.instancia.asignarDestruccionColeccionable == true)
        {

            Experiencia.instancia.asignarDestruccionColeccionable = false;
        }
    }
}
