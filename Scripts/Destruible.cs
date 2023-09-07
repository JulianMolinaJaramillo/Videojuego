using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruible : MonoBehaviour
{
    //Para saber cual es el estado al que debe pasar
    public string EstadoDestruccion;
    //Para saber cuantos segundos esperamos antes de que el collider se destruya
    public float TimeForDisable;


    //Para saber si queremos que instancie
    public bool EsInstanciador;

    public int ID = 1;
    private bool Destruccion;
 

    //Para el sistema de loot
    public GameObject[] lootitems;


    private Animator _Animator;
    public static Destruible instancia;

    private void Start()
    {
        
    }

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }

        _Animator = GetComponent<Animator>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        //Si es un ataque
        if(collision.tag == "Attack")
        {
            //Reproducimos la animacion de destruccion y esperamos
            _Animator.Play(EstadoDestruccion);


            //Colocamos el sonido depende de lo que destruimos
            if (EstadoDestruccion == "JarronDestruido") 
            {
                AudioManager.instancia.PlayAudio(AudioManager.instancia.JarronRoto);
                float cambioPitch = Random.Range(1f, 1.50f);
                AudioManager.instancia.JarronRoto.pitch = cambioPitch;

            }
            if (EstadoDestruccion == "PlantaDestruida")
            {
                AudioManager.instancia.PlayAudio(AudioManager.instancia.CespedCortado);
                float cambioPitch = Random.Range(1f, 1.50f);
                AudioManager.instancia.CespedCortado.pitch = cambioPitch;
            }
            

            if (EsInstanciador == true)
            {
                Instantiate(lootitems[0].gameObject, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(TimeForDisable);

            //Pasados los segundos de espera, desactivamos los colliders
            foreach(Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
    }

    private void Update()
    {
        //Destruimos el objeto despues de la animacion
        AnimatorStateInfo stateInfo = _Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(EstadoDestruccion) && stateInfo.normalizedTime >= 1)
        {
            if (EsInstanciador == true)
            {
                Experiencia.instancia.ObjetosDestruidos(ID);

            }

            Destroy(gameObject);
        }

        if (Destruccion == true)
        {
            Destroy(gameObject);
        }

        DestruccionAlCargar();

    }

    public void LateUpdate()
    {
        if (Experiencia.instancia.asignarDestruccion == true)
        {
            Experiencia.instancia.asignarDestruccion = false;
        }
    }

    public void DestruccionAlCargar()
    {
        if (EsInstanciador == true)
        {
            for (int i = 0; i < Experiencia.instancia.objetosDestruibles.Length; i++)
            {
                if (ID == Experiencia.instancia.objetosDestruibles[i])
                {
                    Destruccion = true;

                }
            }
        }
           
    }
}
