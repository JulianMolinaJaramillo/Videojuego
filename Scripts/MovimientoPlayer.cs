using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MovimientoPlayer : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    //para prevenir que el player se mueva y poder haceer el ataque cargado
    public bool PrevencionMovimiento;
    public bool EstoyEnInterior;

    private Rigidbody2D rigy;
    public Animator _animator;

    public Image EstaminaImagen;
    public float DisminucionEstamina;
    private bool NoPuedeAtacar;

    public Vector2 Movimiento;
    public Vector2 Movimiento2;
    

    CircleCollider2D AtaqueCollider;
    Aura aura;

    public GameObject TajoPrefab;
    public GameObject ExplosionAttack;
    public GameObject HitBox;
    public bool TajoDesbloqueado;
    //private bool AtaqueDesbloqueado;
    private bool cargaataque;

    public static MovimientoPlayer instancia;

    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;
        }
        _animator = GetComponent<Animator>();
        rigy = GetComponent<Rigidbody2D>();
 


        Assert.IsNotNull(TajoPrefab);
    }

    private void Start()
    {
        //Recuperamos el collider de ataque del primer hijo
        AtaqueCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();

        //Desactivamos el circle de ataque en el principio
        //AtaqueCollider.enabled = false;

        //Tomamos el componente aura del hijo numero 1 del player
        aura = transform.GetChild(1).GetComponent<Aura>();

        
    }
    // Update is called once per frame
    void Update()
    {
        //Para validar si el tajo esta desbloqueado
        if (Experiencia.instancia.PoderTajo == 1)
        {
            TajoDesbloqueado = true;
        }
        else
        {
            TajoDesbloqueado = false;
        }


        Movimientos();

        Animaciones();

        
        if (NoPuedeAtacar == false)
        {
            if (cargaataque == false)
            {
                AtaqueEspada();
            }         
        }

        if (TajoDesbloqueado == true && NoPuedeAtacar == false)
        {
            TajoAtaque();
        }


        PrevenirMovimiento();
    }


    //Tomamos el movimiento en el fixedUpdate para evitar errrores
     void FixedUpdate()
    {
        //Posicion acutal de nuestro rigibody, el movimiento o la direecion, por la velocidad6
        rigy.MovePosition(rigy.position + Movimiento * velocidadMovimiento * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (Movimiento != Vector2.zero)
        {
            
        }
        else
        {
            if (EstoyEnInterior == true)
            {
                AudioManager.instancia.Pasos.Stop();
                AudioManager.instancia.PlayAudio(AudioManager.instancia.PasosInteriores);
            }

            if (EstoyEnInterior == false)
            {
                AudioManager.instancia.PlayAudio(AudioManager.instancia.Pasos);
            }
            
        }

        
    }

    void Movimientos()
    {
        Movimiento = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));  
    }

    void Animaciones()
    {
        //Comprovamos si el vector es diferente al vector vacio, para que la animacion siempre este en la direccion adecuada
        if (Movimiento != Vector2.zero)
        {
            
            _animator.SetFloat("MovimientoX", Movimiento.x);
            _animator.SetFloat("MovimientoY", Movimiento.y);
            _animator.SetBool("Walk", true);
            //_animator.speed = 1;
            
        }
        else
        {
            //_animator.speed = 0;
            _animator.SetBool("Walk", false);
            
        }
    }

    void AtaqueEspada()
    {
        //Buscamos el estado actual mirando informacion en el animator
        AnimatorStateInfo satateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        bool Atacando = satateInfo.IsName("Player Attack");

        //Atacamos
        if (Input.GetKeyDown(KeyCode.C) && !Atacando)
        {
            //Bajamos a la estamina
            DisminucionEstamina += 0.40f - Experiencia.instancia.EstaminaActual;
            EstaminaImagen.fillAmount = 1 - DisminucionEstamina;
            
            if (EstaminaImagen.fillAmount <= 0.40 - Experiencia.instancia.EstaminaActual)
            {
                NoPuedeAtacar = true;
                StopAllCoroutines();
                StartCoroutine(reactivarEstamina());
            }
            else if (EstaminaImagen.fillAmount != 1 && NoPuedeAtacar == false)
            {
                StopAllCoroutines();
                StartCoroutine(reactivarEstamina());
            }

            
            _animator.SetTrigger("Atacando");

            PrevencionMovimiento = true;
            PrevenirMovimiento();

            AudioManager.instancia.PlayAudio(AudioManager.instancia.Espada);
            StartCoroutine(activarCollider(0.1f));
            StartCoroutine(ActivarMovimientoDespues(0.7f));
        }
      

        //Actualizamos la posicion del circle collider de ataque
        if (Movimiento != Vector2.zero)
        {
            AtaqueCollider.offset = new Vector2(Movimiento.x / 2, Movimiento.y / 2);
            
        }
    }

    void TajoAtaque()
    {
        //Buscamos el estado actual mirando informacion en el animator comprobaNDO QUE NO ESTE HACIENDO LA ANIMACION
        AnimatorStateInfo satateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        bool Cargando = satateInfo.IsName("Player Tajo Inicio");

        //Atacamos
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Para evitar atacar mientras cargamos
            cargaataque = true;

            _animator.SetTrigger("CargarAtaque");
            aura.AuraStar();

        }else if (Input.GetKeyUp(KeyCode.X))
        {
            if (!aura.IsLoaded() )
            {
                //_animator.SetTrigger("NoCargandoAtaque");
                _animator.SetBool("NoCargandoAtaque", true);

                //Esperamos un momento despues del ataque para poder movernos
                StartCoroutine(ActivarMovimientoDespues(0.05f));

                //ESTE ESPACIO ES POR SI DESEAMOS QUE EL PLAYER HAGA UN ATAQUE SI NO ESTA CARGADA EL AURA
                ////Bajamos a la estamina sino esta cargado el ataque
                //DisminucionEstamina += 0.40f - Experiencia.instancia.EstaminaActual;
                //EstaminaImagen.fillAmount = 1 - DisminucionEstamina;

                //if (EstaminaImagen.fillAmount <= 0.40 - Experiencia.instancia.EstaminaActual)
                //{
                //    NoPuedeAtacar = true;
                //    StopAllCoroutines();
                //    StartCoroutine(reactivarEstamina());
                //}
                //else if (EstaminaImagen.fillAmount != 1 && NoPuedeAtacar == false)
                //{
                //    StopAllCoroutines();
                //    StartCoroutine(reactivarEstamina());
                //}

                //AudioManager.instancia.PlayAudio(AudioManager.instancia.Espada);
                //StartCoroutine(activarCollider(0.1f));


            }

            //Solo instanciamos el objeto si el aura esta cargada
            if (aura.IsLoaded())
            {
                //Bajamos a la estamina si esta cargado el ataque
                DisminucionEstamina += 0.80f - Experiencia.instancia.EstaminaActual;
                EstaminaImagen.fillAmount = 1 - DisminucionEstamina;
                _animator.SetBool("CargarAtaqueEnd", true);

                if (EstaminaImagen.fillAmount <= 0.40 - Experiencia.instancia.EstaminaActual)
                {
                    NoPuedeAtacar = true;
                    StopAllCoroutines();
                    StartCoroutine(reactivarEstamina());
                }
                else if (EstaminaImagen.fillAmount != 1 && NoPuedeAtacar == false)
                {
                    StopAllCoroutines();
                    StartCoroutine(reactivarEstamina());
                }

                
                //conseguimos la rotacion a partir de un vector
                float angle = Mathf.Atan2(
                    _animator.GetFloat("MovimientoY"),
                    _animator.GetFloat("MovimientoX")) * Mathf.Rad2Deg;

                //Creamos la instancia del tajo
                GameObject objetoTajo = Instantiate(TajoPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                AudioManager.instancia.PlayAudio(AudioManager.instancia.BolaFuego);
                StartCoroutine(DestruirObjeto());

                //Le otorgamos el movimiento inicial
                Tajo Slash = objetoTajo.GetComponent<Tajo>();
                Slash.Mov.y = _animator.GetFloat("MovimientoY");
                Slash.Mov.x = _animator.GetFloat("MovimientoX");

                //Esperamos un momento despues del ataque para poder movernos
                StartCoroutine(ActivarMovimientoDespues(0.5f));
            }

            //Reiniciamos el proceso del aura
            aura.AuraStop();
            cargaataque = false;       
        }

        if (Cargando == true)
        {
            PrevencionMovimiento = true;
        }
        
    }

    void PrevenirMovimiento()
    {
        //si el movimiento es verdadero. lo pasamos a cero para cancelarlo
        if (PrevencionMovimiento)
        {
            Movimiento = Vector2.zero;
        }
    }

    IEnumerator ActivarMovimientoDespues(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        PrevencionMovimiento = false;
        _animator.SetBool("NoCargandoAtaque", false);
        _animator.SetBool("CargarAtaqueEnd", false);
    }

    IEnumerator activarCollider(float seg)
    {
        yield return new WaitForSeconds(0.3f);
        HitBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(seg);
        HitBox.gameObject.SetActive(false);
    }

    IEnumerator reactivarEstamina()
    {
        yield return new WaitForSeconds(2.5f);

        while (EstaminaImagen.fillAmount < 1)
        {
            EstaminaImagen.fillAmount += 0.10f;
            yield return new WaitForSeconds(0.05f);

        }

        NoPuedeAtacar = false;
        DisminucionEstamina = 0;
    }

    public void DesactivarRygy()
    {
        //rigy.gameObject.SetActive(false);
        //rigy.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.gameObject.GetComponent<MovimientoPlayer>().enabled = false;
        AudioManager.instancia.Pasos.Stop();
        AudioManager.instancia.PasosInteriores.Stop();
    }

    public void ActivarRygy()
    {
        //rigy.gameObject.SetActive(true);
        //rigy.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.gameObject.GetComponent<MovimientoPlayer>().enabled = true;
    }

    IEnumerator DestruirObjeto()
    {
        GameObject objeto = Instantiate(ExplosionAttack, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(objeto);
    }

}
