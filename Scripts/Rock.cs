using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    //Velovidad del proyectil
    public float velocidad;
    //Daño del proyectil
    public int Daño = 1;

    //recuperamos al player
    GameObject Player;
    private Rigidbody2D _rigy;
    //Vectores para almacenar el objetivo y su direccion
    Vector3 target, direccion;


    private void Awake()
    {
        _rigy = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        //Recuperamos la posicion del layer y la direccion normalizada
        if(Player != null)
        {
            target = Player.transform.position;
            direccion = (target - transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        //si encontramos un objetivo, movemos la roca a esa posicion
        if(target != Vector3.zero)
        {
            _rigy.MovePosition(transform.position + (direccion * velocidad) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si chocamos contra el player, distruimos este objeto
        if(collision.transform.tag == "Player")
        {
            collision.SendMessageUpwards("AdherirDaño", Daño);

            //Para empujar al player
            if(Player.transform.position.y < transform.position.y)
            {
                collision.SendMessageUpwards("EmpujarAbajo");

            }else if(Player.transform.position.y > transform.position.y)
            {
                collision.SendMessageUpwards("EmpujarArriba");
            }
            if(Player.transform.position.x > transform.position.x)
            {
                collision.SendMessageUpwards("EmpujarDerecha");
            }else if(Player.transform.position.x < transform.position.x){
                collision.SendMessageUpwards("EmpujarIzquierda");
            }




            if (collision.transform.tag == "Attack")
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        //Si se sale de la pantalla borramos la roca
        Destroy(gameObject);
    }
}
