using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    public GameObject Target;

    //Para controlar si empeiza o no una transicion
    bool star = false;
    //Para controlar si la transicion es de entrada o salida
    bool isFadeIn = false;
    //opacidad inicial del cuadrado de transicion
    float alfa = 0;
    //Transicion de 1 segundo
    public float fadeTime = 2f;
    //Nombre del area
    public string NombreArea;
    public int IDWarp;
    public bool EsEnInterior;
    


    GameObject Area;
    public GameObject Player;
    //GameObject PanelTransicion;


    //Para desactivar y activar las camaras
    public GameObject _camara_enable;
    public GameObject _camara_disable;

    //Para no permitir el pausar
    ManagerSingleton GameManager;
    PausaMenu Pausa;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        Area = GameObject.FindGameObjectWithTag("Area");
        //PanelTransicion = GameObject.FindGameObjectWithTag("Transicion");
    }

    private void Start()
    {
        GameManager = ManagerSingleton.instancia;
        Pausa = GameManager.GetComponent<PausaMenu>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Player")
        {
            Pausa.GetComponent<PausaMenu>().enabled = false;

            if (EsEnInterior == true)
            {
                AudioManager.instancia.PlayAudio(AudioManager.instancia.AbrirYCerrar);
            }
            collision.GetComponent<Collider2D>().enabled = false;

            Player.GetComponent<MovimientoPlayer>().enabled = false;
            Player.GetComponent<Animator>().enabled = false;

            AudioManager.instancia.Pasos.Stop();
            AudioManager.instancia.PasosInteriores.Stop();

            FadeIn();

            //Sino es el Warp especial
            if (IDWarp != 5)
            {
                yield return new WaitForSeconds(0.6f);
                _camara_enable.gameObject.SetActive(true);
                _camara_disable.gameObject.SetActive(false);

                //Movimos al player de la posicion donde esta a la del target pero del hijo, utilizando el metodo GetChild
                collision.transform.position = Target.transform.GetChild(0).transform.position;

                StartCoroutine("currutina");

                //Para acomodar al player al cruzar de zona
                if (IDWarp == 1)
                {
                    collision.GetComponent<Animator>().SetFloat("MovimientoY", 1f);
                    collision.GetComponent<Animator>().SetFloat("MovimientoX", 0f);
                }

                if (IDWarp == 2)
                {
                    collision.GetComponent<Animator>().SetFloat("MovimientoY", -1f);
                    collision.GetComponent<Animator>().SetFloat("MovimientoX", 0f);
                }

                if (IDWarp == 3)
                {
                    collision.GetComponent<Animator>().SetFloat("MovimientoY", 0f);
                    collision.GetComponent<Animator>().SetFloat("MovimientoX", 1f);
                }

                if (IDWarp == 4)
                {
                    collision.GetComponent<Animator>().SetFloat("MovimientoY", 0f);
                    collision.GetComponent<Animator>().SetFloat("MovimientoX", -1f);
                }

                yield return new WaitForSeconds(fadeTime);

                StartCoroutine(Area.GetComponent<Area>().AreaShow(NombreArea,1));
                FadeOut();

                Player.GetComponent<MovimientoPlayer>().enabled = true;
                Pausa.GetComponent<PausaMenu>().enabled = true;
                yield return new WaitForSeconds(0.2f);
                collision.GetComponent<Collider2D>().enabled = true;
            }

            //Solo si es el Warp especial de fin del juego
            else if (IDWarp == 5)
            {
                AudioManager.instancia.Bosque.Stop();
                AudioManager.instancia.PlayAudio(AudioManager.instancia.FinalJuego);
                yield return new WaitForSeconds(0.6f);
                _camara_enable.gameObject.SetActive(true);
                _camara_disable.gameObject.SetActive(false);
                //Movimos al player de la posicion donde esta a la del target pero del hijo, utilizando el metodo GetChild
                collision.transform.position = Target.transform.GetChild(0).transform.position;
                //Ubicamos al jugador
                Player.GetComponent<Animator>().enabled = true;
                Player.GetComponent<Animator>().SetBool("Walk", false);
                collision.GetComponent<Animator>().SetFloat("MovimientoY", -1f);
                collision.GetComponent<Animator>().SetFloat("MovimientoX", 0f);
                //Pausa.GetComponent<PausaMenu>().enabled = true;
                yield return new WaitForSeconds(fadeTime);
                StartCoroutine(Area.GetComponent<Area>().AreaShow(NombreArea,10));
                FadeOut();
                yield return new WaitForSeconds(6f);
                SceneManager.LoadScene(0);
            }
            
        }
            

    }


    private IEnumerator currutina()
    {
        
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Animator>().enabled = true;
    }

    //Dibujamos un cuadrado con opacidad encima de la pantalla OnGUI

    private void OnGUI()
    {
        //Sino empieza la transicion salimos del evento directamente
        if (!star) return;

        //Si ha empezado creamos un color con opacidad inicial de cero
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alfa);

        //Creamos una textura temporal para rellenar la pantalla
        Texture2D textura;
        textura = new Texture2D(1, 1);
        textura.SetPixel(0, 0, Color.black);
        textura.Apply();
        

        //Dibujamos la textura sobre toda la pantalla
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), textura);

        //Controlamos la apariencia
        if (isFadeIn)
        {
            //Si es la de aparecer le sumamos opacidad
            alfa = Mathf.Lerp(alfa, 1.1f, fadeTime * Time.deltaTime);
            
        }
        else
        {
            //Si es la de desaparecer le restamos opacidad
            alfa = Mathf.Lerp(alfa, -0.1f, fadeTime * Time.deltaTime);

            //Si la opacidad llega a cero desactivamos la transicion
            if (alfa < 0) star = false;
        }
        
    }
    //Metodo para activar la transicion de entrada
    void FadeIn()
    {
        star = true;
        isFadeIn = true;
        
    }

    void FadeOut()
    {
        isFadeIn = false;
        
    }
}
