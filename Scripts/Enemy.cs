using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    //Variables para gestionar el radio de vision, ataque y velocidad
    public float RadioVision;
    public float RaiioAtaque;
    public float Velocidad;


    //Variables para utilizar el ataque
    public GameObject PrefabProyectil;
    public float VelocidadAtaque = 2f;
    public bool atacando;


    public float ExperienciaADar;
    public bool DebeRespaunear;
    public int ID;

    //Variables para administrar la vida de los enemigos
    //Maximo de vida
    public int maxHP;
    //>Vida actual
    public int HP;
    public GameObject EfectoGolpe;
    public GameObject EfectoGolpe2;
    public GameObject TextDamage;

    //Para el sistema de loot
    public GameObject[] looeitems;


    //Almacenamos al player
    GameObject Player;
    //Guardamos nuestra posicion inicial
    Vector3 PosicionInicial, target;

    Animator _Animador;
    Rigidbody2D _Rigy;
    SpriteRenderer _Sprye;
    Enemy _enemigo;
    Collider2D _collider;


    public static Enemy instancia;

    void Awake()
    {

        if (instancia == null)
        {
            instancia = this;
        }


        _Animador = GetComponent<Animator>();
        _Rigy = GetComponent<Rigidbody2D>();
        _Sprye = GetComponent<SpriteRenderer>();
        _enemigo = GetComponent<Enemy>();
        _collider = GetComponent<Collider2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Recuperamos al jugador por el tag
        Player = GameObject.FindGameObjectWithTag("Player");
        
        //Guaramos la posicion inicial
        PosicionInicial = transform.position;

        //Iniciamos la vida
        HP = maxHP;

        //SI queremos cambiar el orden de las Sorting Layers
        //Renderer myrenderer = GetComponent<Renderer>();
        //myrenderer.sortingLayerID = SortingLayer.layers[2].id;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Por defecto nuestro target, siempre será la posicion inicial
        Vector3 target = PosicionInicial;

        //Hacemos un Raycast desde el enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, RadioVision, 1 << LayerMask.NameToLayer("Default"));
        //Enemigo en layer disttinta a default
        //a los objetos y slash una layer attack

        //Aqui podemos debugear el raycast
        Vector3 forward = transform.TransformDirection(Player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        //Si el Raycast encuentra al Player, asignamos este a target
        if(hit.collider != null)
        {
            
            if (hit.collider.tag == "Player")
            {
                target = Player.transform.position;
            }
        }

        //Calculamos la distancia y direccion hasta el target
        float distancia = Vector3.Distance(target, transform.position);
        Vector3 direccion = (target - transform.position).normalized;

        //Si encuentra al objetivo, nos paramos y atacamos
        if(target != PosicionInicial && distancia < RaiioAtaque)
        {
            _Animador.SetFloat("MovX", direccion.x);
            _Animador.SetFloat("MovY", direccion.y);

            
          
            //Debug.Log("Player encontrado");
            _Animador.Play("Walking", -1, 0);//congelamos la animacion al andar

            //En caso contrario, nos movemos hacia el
            if (!atacando) StartCoroutine(Atacar(VelocidadAtaque));
        }
        else
        {
            _Rigy.MovePosition(transform.position + direccion * Velocidad * Time.deltaTime);

            //Al movernos establecemos las animaciones
            _Animador.speed = 1;
            _Animador.SetFloat("MovX", direccion.x);
            _Animador.SetFloat("MovY", direccion.y);
            _Animador.SetBool("Walking", true);
        }

        //Una ultima comprobacion para evitar bugs, forzando la posicion inicial
        if(target == PosicionInicial && distancia < 0.02f)
        {
            transform.position = PosicionInicial;
            //Camibamos la animacion a Idle
            _Animador.SetBool("Walking", false);
        }


        //Un debug optativo con una linea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
    }


    //Podemos dibujar el radio de ataque y vision sobre la escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, RadioVision);
        Gizmos.DrawWireSphere(transform.position, RaiioAtaque);
    }

    IEnumerator Atacar(float segundos)
    {
        //Activamos la bandera
        atacando = true;
        //si encontramos al objetivo y el prefab no es null, atacamos
        if(target != PosicionInicial && PrefabProyectil != null)
        {
            Instantiate(PrefabProyectil, transform.position, transform.rotation);
            //Llamamos el sonido
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Disparo);
            yield return new WaitForSeconds(segundos);
        }
        //Desactivamos la bandera
        atacando = false;
    }

    //Si nos atacan, restamos vida al enemigo
    public void Atacado(int ataque)
    {
        
        
        HP -= ataque;

        if (ataque >= Atacarhit.instancia.ataque + 7)
        {
            TextDamage.GetComponent<TextMeshPro>().SetText(ataque.ToString() + " CRITICO!");

        }
        else
        {
            TextDamage.GetComponent<TextMeshPro>().SetText(ataque.ToString());
        }
      
        GameObject TextoUp = Instantiate(TextDamage, transform.position, Quaternion.identity);

        StartCoroutine(Movertexto(TextoUp));
        StartCoroutine("Dañado");
        

        if (HP <= 0)
        {
            
            Experiencia.instancia.ModificadorExperiencia(ExperienciaADar);

            //Rareza del loot que nos deja el enemigo
            int lootRareza = Random.Range(0, 101);
            if(lootRareza > 20)
            {
                //Le damos el primer item
                Instantiate(looeitems[0].gameObject, transform.position, Quaternion.identity);
            }
            else
            {
                //Le damos el segundo item
                Instantiate(looeitems[1].gameObject, transform.position, Quaternion.identity);
            }


            StartCoroutine("Muerte");


        }
        else if (DebeRespaunear == false && HP <= 0)
        {
            StartCoroutine("Muerte");
        }
  
    
    }

    //Para verificar si destruimos o no el enemigo
    private IEnumerator Muerte()
    {
        if (DebeRespaunear == false)
        {
            _Animador.SetBool("Daño", true);
            _enemigo.GetComponent<Enemy>().enabled = false;
            _collider.GetComponent<Collider2D>().enabled = false;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.MaderaCrujiente);
            yield return new WaitForSeconds(80f);
            Destroy(gameObject);
        }
        else
        {
            _Animador.SetBool("Daño", true);
            _enemigo.GetComponent<Enemy>().enabled = false;
            _collider.GetComponent<Collider2D>().enabled = false;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.MaderaCrujiente);
            HP = 0;
            yield return new WaitForSeconds(10f);
            _enemigo.GetComponent<Enemy>().enabled = true;
            _collider.GetComponent<Collider2D>().enabled = true;
            _Animador.SetBool("Daño", false);
            transform.GetComponentInParent<Respawn>().StartCoroutine(GetComponentInParent<Respawn>().RespawnEnemigo(ID));
        }     
    }

    private IEnumerator Dañado()
    {
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Maderahurt);
        _Sprye.GetComponent<SpriteRenderer>().color = Color.red;

        Instantiate(EfectoGolpe, transform.position, Quaternion.identity);
        Instantiate(EfectoGolpe2, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);
        _Sprye.GetComponent<SpriteRenderer>().color = Color.white;
    }


 
    IEnumerator Movertexto(GameObject texto)
    {
        Vector2 inicial = new Vector2(texto.transform.position.x, texto.transform.position.y);
        Vector2 Final = new Vector2(texto.transform.position.x + 50, texto.transform.position.y + 50);
        int upTimes = 0;

        while (upTimes < 6)
        {
            upTimes++;
            //Para mover el objeto hacia una posicion
            //Para que cada vez que pasa un frame se mueva al loop del While
            yield return new WaitForEndOfFrame();
            texto.transform.position = Vector2.MoveTowards(inicial, Final, 35f * Time.deltaTime);
            
        }
        
    }


    ////Dibujamos la vida de los enemigos en una barra
    //private void OnGUI()
    //{
    //    //Guardamos la posicion del enemigo, en el mundo respecto a la camara
    //    Vector2 posicion = Camera.main.WorldToScreenPoint(transform.position);

    //    //Dibujamos el cuadrado con el texto, bajo el enemigo
    //    GUI.Box(new Rect(posicion.x - 30, Screen.height - posicion.y + 60, 50, 25), HP + "/" + maxHP);
    //    //Posicion X de la barra
    //    //Posicion y de la barra
    //    //Anchura de la barra
    //    //Altura de la barra
    //    //Texto de la barra
    //}

    //public void DestruirObjeto()
    //{
    //    this.gameObject.SetActive(false);
    //}
}
