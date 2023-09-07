using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //VELOCIDAD A LA QUE QUEREMOS QUE SE MUEVA NUESTRA BALA
    public float velocidad = 2f;
    //PARA MARCAR HACIA DONDE SE MUEVE EL SCRIP EN EL SCENARIO A LA VELOCIDAD ANTERIOR
    public Vector2 direccion;
    //PARA DICTAR UN TIEMPO DE VIDA DEL PROYECTIL
    public float tiempoVuelo;
    //CANTIDAD DE DAÑO
    public int daño = 1;
    private bool PlayerTocado;
    private bool PlayerNoTocado;

    public GameObject ExplosionROca;
    private Rigidbody2D _rigibogy;
    private SpriteRenderer _sprite;
    private Collider2D _collier;


    //public GameObject efectoExplocion;
    //GUARDAR CUANDO SE HA INSTANCIADO EL OBJETO
    private float startingTime;

    void Awake()
    {
        _collier = GetComponent<Collider2D>();
        _rigibogy = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //GUARDAR EL TIEMPO EN EL MOEMNTO QUE SE HA INSTANCIADO LA BALA
        startingTime = Time.time;

        //METODO QUE NOS DA UNITY PARA DESTRUIR UN OBJETO
        //1er parametro el objeto a destruir 2do parametro el tiempo de destruccion
        //Destroy(this.gameObject, tiempoVuelo);
        StartCoroutine(EfectoExplosion(tiempoVuelo));
    }


    private void FixedUpdate()
    {
        //Movimiento con RIGIBODY
        Vector2 movimiento = direccion.normalized * velocidad;
        _rigibogy.velocity = movimiento;

    }

    private IEnumerator EfectoExplosion(float tiempoVuelo)
    {
        yield return new WaitForSeconds(tiempoVuelo);
        if (PlayerTocado == false)
        {
            GameObject explosion = Instantiate(ExplosionROca, transform.position, Quaternion.identity);
            _sprite.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlayerNoTocado = true;
            velocidad = 0;
            yield return new WaitForSeconds(1f);
            _collier.GetComponent<Collider2D>().enabled = false;
            Destroy(explosion);
        }      
        yield return new WaitForSeconds(1f);    
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            colision.SendMessageUpwards("AdherirDaño", daño);

            ////CREAMOS UNA INSTANCIA DE EXPLOSION
            //GameObject MiBala = Instantiate(efectoExplocion, transform.position, Quaternion.identity) as GameObject;
            //DestruirExplosiones.Destruyela();
            if (PlayerNoTocado == false)
            {
                StartCoroutine(EfectoGolpearPlayer());
                PlayerTocado = true;
            }          
        }
    }

    private IEnumerator EfectoGolpearPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject explosion = Instantiate(ExplosionROca, transform.position, Quaternion.identity);
        _sprite.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        _collier.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        Destroy(explosion);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
