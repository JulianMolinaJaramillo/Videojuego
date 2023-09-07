using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HablarNPC : MonoBehaviour
{
    public int idNPC;

    public string[] mensaje; /*= { "hola perra",*/  //index 0
    private int ContadorMensajes;                                               //"Dame tu culo," +                          //index 1
                                                                                //        "Suerte Marika"};                  //index 2
    public bool MiroArriba;
    public bool MiroAbajo;
    public bool Miroizquierda;
    public bool MiroDerecha;

    private Animator _animacion;
    private MovimientoAleatorioObjetos _movimiento;

    public static HablarNPC instancia;


    private void Awake()
    {
        _movimiento = GetComponent<MovimientoAleatorioObjetos>();
        _animacion = GetComponent<Animator>();

        if (instancia == null)
        {
            instancia = this;
        }

        ContadorMensajes = 0;
    }

    public string ConversacionNPC()
    {
        //Para aumentar los mensajes
        return mensaje[ContadorMensajes];
    }

    public void PasarPaginas()
    {
        ContadorMensajes += 1;
    }

    public void QuedarmeQuieto()
    {
        _movimiento.GetComponent<MovimientoAleatorioObjetos>().DetenerCurrutinas();
        _movimiento.GetComponent<MovimientoAleatorioObjetos>().ShouldMove = false;
        _animacion.SetBool("Walk", false);

        //Para posicionar al NPC
        if (MiroDerecha == true)
        {
            _animacion.SetFloat("MovimientoY", 0f);
            _animacion.SetFloat("MovimientoX", 1f);
        }
        if (MiroArriba == true)
        {
            _animacion.SetFloat("MovimientoY", 1f);
            _animacion.SetFloat("MovimientoX", 0f);
        }

        if (Miroizquierda == true)
        {
            _animacion.SetFloat("MovimientoY", 0f);
            _animacion.SetFloat("MovimientoX", -1f);
        }
        if (MiroAbajo == true)
        {
            _animacion.SetFloat("MovimientoY", -1f);
            _animacion.SetFloat("MovimientoX", 0f);
        }
    }

    public void PuedoMoverme()
    {
        _movimiento.GetComponent<MovimientoAleatorioObjetos>().ShouldMove = true;
        _animacion.SetBool("Walk", true);
        ContadorMensajes = 0;
    }

}
