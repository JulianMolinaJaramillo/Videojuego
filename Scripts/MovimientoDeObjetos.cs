using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoDeObjetos : MonoBehaviour
{
    public Transform PuntoA, PuntoB, PuntoC, PuntoD;
    public float Velocidad;
    public float TiempoEspera;

    public bool ShouldWait;
    public bool ShouldMove;

    private bool MoveToA;
    private bool MoveToB;
    private bool MoveToC;
    private bool MoveToD;

    private Animator _Animador;

    private void Awake()
    {
        _Animador = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MoveToA = true;
        MoveToB = false;
        MoveToC = false;
        MoveToD = false;  
        _Animador.SetBool("Walk", true);
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
        if(DistanciaDeA > 0.1f && MoveToA == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoA.position, Velocidad * Time.deltaTime);
            _Animador.SetFloat("MovimientoY",1f);
            _Animador.SetFloat("MovimientoX",0f);

            if (DistanciaDeA < 0.3f && ShouldMove == true)
            {
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MoveToA = false;
                    MoveToB = true;
                }
                else
                {
                    MoveToA = false;
                    MoveToB = true;
                }
                
            }
        }

        if (DistanciaDeB > 0.1f && MoveToB == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoB.position, Velocidad * Time.deltaTime);
            _Animador.SetFloat("MovimientoY", 0f);
            _Animador.SetFloat("MovimientoX", 1f);

            if (DistanciaDeB < 0.3f && ShouldMove == true)
            {
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MoveToB = false;
                    MoveToC = true;
                }
                else
                {
                    MoveToB = false;
                    MoveToC = true;
                }
                        
            }
        }

        if (DistanciaDeC > 0.1f && MoveToC == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoC.position, Velocidad * Time.deltaTime);
            _Animador.SetFloat("MovimientoY", -1f);
            _Animador.SetFloat("MovimientoX", 0f);

            if (DistanciaDeC < 0.3f && ShouldMove == true)
            {
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MoveToC = false;
                    MoveToD = true;
                }
                else
                {
                    MoveToC = false;
                    MoveToD = true;
                }
                
            }
        }

        if (DistanciaDeD > 0.1f && MoveToD == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, PuntoD.position, Velocidad * Time.deltaTime);
            _Animador.SetFloat("MovimientoY", 0f);
            _Animador.SetFloat("MovimientoX", -1f);

            if (DistanciaDeD < 0.3f && ShouldMove == true)
            {
                if (ShouldWait)
                {
                    StartCoroutine(TiempoDeEspera());
                    MoveToD = false;
                    MoveToA = true;
                    _Animador.SetFloat("MovimientoY", 1f);
                    _Animador.SetFloat("MovimientoX", 0f);
                }
                else
                {
                    MoveToD = false;
                    MoveToA = true;
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

    private void OnDisable()
    {
        ShouldMove = true;
    }
}
