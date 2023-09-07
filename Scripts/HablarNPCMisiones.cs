using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HablarNPCMisiones : MonoBehaviour
{
    public int idNPC;

    public TextMeshProUGUI textoBoton1;
    public TextMeshProUGUI textoBoton2;

    public bool MisionAcepted;
    public bool MisionCompleted;
    public string MisionAceptada;
    public string MisionRechazada;
    public string[] mensaje; /*= { "hola perra",*/  //index 0
    private int ContadorMensajes;                                               //"Dame tu culo," +                          //index 1

    // Para un salto de linea en string \n


    public bool MiroArriba;
    public bool MiroAbajo;
    public bool Miroizquierda;
    public bool MiroDerecha;

    private Animator _animacion;
    private MovimientoAleatorioObjetos _movimiento;

    public static HablarNPCMisiones instancia;


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
        if (idNPC == 1)
        {
            textoBoton1.text = "De acuerdo";
            textoBoton2.text = "Me lo pensaré";
        }

        if (idNPC == 2)
        {
            textoBoton1.text = "Lo intentaré";
            textoBoton2.text = "No tengo tiempo";
        }

        if (idNPC == 3)
        {
            textoBoton1.text = "Sin programar";
            textoBoton2.text = "Sin programar";
        }
    }

    public void BotonAfirmativo()
    {
        MisionAcepted = true;
        ContadorMensajes = 0;
    }

    public void BotonNegativo()
    {
        ContadorMensajes = 0;
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
    }

    public void MisionCompletada()
    {
        if (idNPC == 1 && MisionCompleted == true)
        {
            if (MisionAcepted == true)
            {
                MisionAceptada = "¿Que había uno de esos monstruos?, no puede ser.Es posible que mi hermano . . . .  No no puede ser verdad, seguramente se fuera al ver el monstruo, voy a esperar a que vuelva. Gracias";
            }
            if (MisionAcepted == false)
            {
                MisionAceptada = "Gracias por abrir, hacia mucho que no podía, lastima no poderte recompensar ya que lo hiciste sin mi permiso.";
            }
        }

        if (idNPC == 2 && MisionCompleted == true)
        {
            if (MisionAcepted == true)
            {
                MisionAceptada = "Este collar pertenecía a mi esposa, ¿la encontraste sobre un cadáver?, noooo no puede ser, se lo advertí mucho a la muy tonta. Gracias no es culpa tuya, pero déjame solo.";
            }
            if (MisionAcepted == false)
            {
                MisionAceptada = "Este collar pertenecía a mi esposa, no se porque lo tienes tu pero gracias por entregármelo, voy a guárdalo hasta que ella vuelva para devolvérselo. ";
            }
        }

        if (idNPC == 3 && MisionCompleted == true)
        {
            if (MisionAcepted == true)
            {
                MisionAceptada = "Sin programar";
            }
            if (MisionAcepted == false)
            {
                MisionAceptada = "Sin programar";
            }
        }
    }
}
