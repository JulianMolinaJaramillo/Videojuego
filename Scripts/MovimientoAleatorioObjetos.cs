using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAleatorioObjetos : MonoBehaviour
{
    public Transform PuntoA, PuntoB, PuntoC, PuntoD;
    public float Velocidad;
    public float TiempoEspera;
    private int PosicionAleatoriaAnterior;

    public bool ShouldWait;
    public bool ShouldMove;

    private bool MoveToA;
    private bool MoveToB;
    private bool MoveToC;
    private bool MoveToD;
    private bool EstoyEnA;
    private bool EstoyEnB;
    private bool EstoyEnC;
    private bool EstoyEnD;

    private Animator _Animador;

    private void Awake()
    {
        _Animador = GetComponent<Animator>();
        _Animador.SetBool("Walk", true);
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveToA = true;
        MoveToB = false;
        MoveToC = false;
        MoveToD = false;      
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldMove)
        {
            MovimientoObjeto();
        }
    }


    private void MovimientoObjeto()
    {
        float DistanciaDeA = Vector2.Distance(transform.position, PuntoA.position);
        float DistanciaDeB = Vector2.Distance(transform.position, PuntoB.position);
        float DistanciaDeC = Vector2.Distance(transform.position, PuntoC.position);
        float DistanciaDeD = Vector2.Distance(transform.position, PuntoD.position);

        //Para verificar las distancias y poder movernos
        if (DistanciaDeA > 0.1f && MoveToA == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoA.position, Velocidad * Time.deltaTime);
            //Para verificar las posiciones del animator
            EstoyEnA = true;
            EstoyEnD = false;

            if (EstoyEnB == true || EstoyEnC == true)
            {
                _Animador.SetFloat("MovimientoY", 0f);
                _Animador.SetFloat("MovimientoX", -1f);
            }
            else
            {
                _Animador.SetFloat("MovimientoY", 1f);
                _Animador.SetFloat("MovimientoX", 0f);
            }
            

            if (DistanciaDeA < 0.3f && ShouldMove == true)
            {
                EstoyEnC = false;
                EstoyEnB = false;
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MovimientoAleatorio();
                }
                else
                {
                    MovimientoAleatorio();
                }

            }
        }

        if (DistanciaDeB > 0.1f && MoveToB == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoB.position, Velocidad * Time.deltaTime);
            EstoyEnB = true;
            EstoyEnA = false;
            EstoyEnD = false;


            //Para organizar el movimiento del animator
            if (EstoyEnC == true)
            {
                _Animador.SetFloat("MovimientoY", 1f);
                _Animador.SetFloat("MovimientoX", 0f);
            }
            else
            {
                _Animador.SetFloat("MovimientoY", 0f);
                _Animador.SetFloat("MovimientoX", 1f);
            }
            

            if (DistanciaDeB < 0.3f && ShouldMove == true)
            {
                EstoyEnC = false;
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MovimientoAleatorio();
                }
                else
                {
                    MovimientoAleatorio();
                }

            }
        }

        if (DistanciaDeC > 0.1f && MoveToC == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoC.position, Velocidad * Time.deltaTime);
            EstoyEnC = true;
            EstoyEnB = false;


            if (EstoyEnD == true || EstoyEnA == true)
            {
                _Animador.SetFloat("MovimientoY", 0f);
                _Animador.SetFloat("MovimientoX", 1f);
            }
            else
            {
                _Animador.SetFloat("MovimientoY", -1f);
                _Animador.SetFloat("MovimientoX", 0f);
            }
            

            if (DistanciaDeC < 0.3f && ShouldMove == true)
            {
                EstoyEnA = false;
                EstoyEnD = false;
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MovimientoAleatorio();
                }
                else
                {
                    MovimientoAleatorio();
                }

            }
        }

        if (DistanciaDeD > 0.1f && MoveToD == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoD.position, Velocidad * Time.deltaTime);
            EstoyEnD = true;
            EstoyEnB = false;
            EstoyEnC = false;

            if (EstoyEnA == true)
            {
                _Animador.SetFloat("MovimientoY", -1f);
                _Animador.SetFloat("MovimientoX", 0f);
            }
            else
            {
                _Animador.SetFloat("MovimientoY", 0f);
                _Animador.SetFloat("MovimientoX", -1f);
            }
            
            if (DistanciaDeD < 0.3f && ShouldMove == true)
            {
                EstoyEnA = false;              
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MovimientoAleatorio();
                }
                else
                {
                    MovimientoAleatorio();
                }

            }
        }
    }


    IEnumerator TiempoDeEspera()
    {
        _Animador.SetBool("Walk", false);
        ShouldMove = false;
        yield return new WaitForSeconds(TiempoEspera);
        ShouldMove = true;
        _Animador.SetBool("Walk", true);
    }


    private void MovimientoAleatorio()
    {
        int PosicionAleatoria = Random.Range(1, 5);

        if (PosicionAleatoria == PosicionAleatoriaAnterior)
        {
            PosicionAleatoria = PosicionAleatoria + 1;

            if (PosicionAleatoria > 4)
            {
                PosicionAleatoria = PosicionAleatoria - 2;
            }
        }

        PosicionAleatoriaAnterior = PosicionAleatoria;

        if (PosicionAleatoria == 1)
        {
            MoveToA = true;
            MoveToB = false;
            MoveToC = false;
            MoveToD = false;
        }
        else if(PosicionAleatoria == 2)
        {
            MoveToA = false;
            MoveToB = true;
            MoveToC = false;
            MoveToD = false;
        }
        else if(PosicionAleatoria == 3)
        {
            MoveToA = false;
            MoveToB = false;
            MoveToC = true;
            MoveToD = false;
        }
        else if(PosicionAleatoria == 4)
        {
            MoveToA = false;
            MoveToB = false;
            MoveToC = false;
            MoveToD = true;
        }
    }

    private void OnDisable()
    {
        ShouldMove = true;
    }

    //Se llama al momento de hablar con los NPC
    public void DetenerCurrutinas()
    {
        StopAllCoroutines();
    }
}
