using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar nuestro rect transform del UI
using UnityEngine.UI;


public class HealthPlayer : MonoBehaviour
{
    // Scrpt para controlar la vida del player

    public int saludTotal = 3;
    //Para no alterara la cantidad inicial de salud
    public int Salud;
    //Para interactuar con el HUD referenciando 
    public RectTransform healthHUD;
    //para activar GameOver 
    public GameObject gameOverMenu;
    private int aumento = 100;
    //Para desactivar sonidos ambiente
    public GameObject ManagerSonidos;

    //Para detectar si estoy muerto y bajar la transparencia del canvas Game Over
    public bool EstoyMuerto;

    public Image Imagen;
    public float corazonHUD = 105f;
    public static HealthPlayer instancia;

    private SpriteRenderer _renderer;

    private Animator _animator;
    private MovimientoPlayer _controlador;


    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;
        }

        //Cargamos el componente animator en la Variable para poderse utilizar
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controlador = GetComponent<MovimientoPlayer>();
    }


    void Start()
    {
        //Salud = saludTotal;
        _animator.GetComponent<Animator>().enabled = true;
        _controlador.GetComponent<MovimientoPlayer>().enabled = true;
        gameOverMenu.SetActive(false);
        if (!EstoyMuerto)
        {
            gameOverMenu.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void AdherirDaño(int cantidad)
    {
        Salud = Salud - cantidad;
        //_animator.SetTrigger("Herir");
        
        StartCoroutine("VisualFeedback1");
        
        //preguntamos y asignamos 0 para evitar que se vuelva un numero negativo
        if (Salud <= 0)
        {
            StartCoroutine("VisualFeedback");
            
        }
        if(Salud <= 0)
        {
            Salud = 0;
        }
        //Le estamos seteando un nuevo valor de corazon al HUD
        healthHUD.sizeDelta = new Vector2(corazonHUD * Salud, corazonHUD);
        


        Debug.Log("Dañado" + Salud);
    }

    public void AdherirSalud(int cantidad)
    {
        Salud = Salud + cantidad;
        //preguntamos para no ecceder el numero inicial de vida
        if (Salud > saludTotal)
        {
            Salud = saludTotal;
        }
        //Le estamos seteando un nuevo valor de corazon al HUD sizeDelta en la X miltiplicamos el tamaño del corazon por la salud actual y en la X la altura normal del corazon
        healthHUD.sizeDelta = new Vector2(corazonHUD * Salud, corazonHUD);
        Debug.Log("Vida" + Salud);
    }

    public void EstoyDead()
    {
        if (EstoyMuerto)
        {
            //Desactivamos todo lo que no queremos activo mientras la muerte
            AudioManager.instancia.Pasos.Stop();
            AudioManager.instancia.PasosInteriores.Stop();
            _animator.GetComponent<Animator>().enabled = false;
            _controlador.GetComponent<MovimientoPlayer>().enabled = false;
            ManagerSonidos.GetComponent<ManagerSonidoAmbiente>().DesactivarSonidos();
            //Detenemos los sonidos del jefe
            AudioManager.instancia.Risaboos.Stop();
            AudioManager.instancia.Disparo1Boos.Stop();
            AudioManager.instancia.Disparo2Boos.Stop();
            AudioManager.instancia.SonidoTierra.Stop();
            AudioManager.instancia.AuraCargando.Stop();
            AudioManager.instancia.PeleaBoos.Stop();

            Time.timeScale = 0;

            //Enemy.instancia.DestruirObjeto();
            gameOverMenu.SetActive(true);
            AudioManager.instancia.Bosque.Stop();      
            AudioManager.instancia.PlayAudio(AudioManager.instancia.GameOver);


        }
    }



    private IEnumerator VisualFeedback1()
    {
        Imagen.color = Color.red;
        _renderer.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white;
        Imagen.color = Color.white;
    }

    private IEnumerator VisualFeedback()
    {
        //_animator.SetTrigger("Muerte");
        
        yield return new WaitForSeconds(0.3f);
        EstoyMuerto = true;
        EstoyDead();

        while (gameOverMenu.GetComponent<CanvasGroup>().alpha < 1f)
        {
            gameOverMenu.GetComponent<CanvasGroup>().alpha += 0.05f;
        }
        
        gameObject.SetActive(false);
    }


    //Direcciones de empuje
    public void EmpujarAbajo()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
    }

    public void EmpujarArriba()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
    }
    public void EmpujarDerecha()
    {
        transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y);
    }
    public void EmpujarIzquierda()
    {
        transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y);
    }



    public void NormalizarPlayer()
    {

       
        OnDisable();
    }
    public void OnEnable()
    {
        Salud = saludTotal;
  
    }

    public void OnDisable()
    {
        //gameOverMenu.gameObject.SetActive(true);
   
        //_animator.enabled = true;
        ////_controlador.enabled = false;
        ////_ReiniciarScore.ReiniciarCOnteo();


        //Salud = 6;
        //healthHUD.sizeDelta = new Vector2(corazonHUD * Salud, corazonHUD);


        ////Volvemos a Setear al player en el punto de Origen
        //_controlador.transform.position = new Vector2(PuntoOrigen.position.x, PuntoOrigen.position.y);
        //_animator.SetBool("Esperando", true);
        
    }
}
