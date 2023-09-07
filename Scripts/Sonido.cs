using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    public int ID;
    private bool Bandera;
    public GameObject Brujo;

    Animator _animador;

    void Awake()
    {
        _animador = Brujo.GetComponent<Animator>();
    }

    void Update()
    {
        QuitarSonidoAlCargar();
    }

    public void LateUpdate()
    {
        if (Experiencia.instancia.asignarDestruccionSonidos == true)
        {
            Experiencia.instancia.asignarDestruccionSonidos = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Bandera == false)
        {
            Bandera = true;
            StartCoroutine(ActivarSonido());
        }
    }

    private IEnumerator ActivarSonido()
    {
        //Identificamos el id de cada audio para cambiar el efecto
        if (ID == 1)
        {
            AudioManager.instancia.PlayAudio(AudioManager.instancia.GritoBruja);
        }
        else if (ID == 2)
        {
            AudioManager.instancia.PlayAudio(AudioManager.instancia.RisaBruja);
        }
        else if (ID == 3)
        {
            AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoTerror);
        }

        //Dejamos que el ID 3 demore mas
        if (ID == 3)
        {
            yield return new WaitForSeconds(3f);
            _animador.SetBool("Hide", true);
        }
        else
        {
            _animador.SetBool("Hide", true);
        }
     
        yield return new WaitForSeconds(3f);
        Bandera = false;
        Destroy(Brujo);
        Experiencia.instancia.SonidoDestruir(ID);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


    public void QuitarSonidoAlCargar()
    {
        for (int i = 0; i < Experiencia.instancia.SonidosDestruibles.Length; i++)
        {
            if (ID == Experiencia.instancia.SonidosDestruibles[i])
            {
                Destroy(this.gameObject);
            }
        }
    }
}
