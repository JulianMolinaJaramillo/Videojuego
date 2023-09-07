using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbolBoss : MonoBehaviour
{
    private Animator _animador;
    private Collider2D _arbolCollider;
    private BoosHealth _arbolhealth;

    //Variables para el ataque
    public bool ActivarArbol;
    private int AtaqueRandom;
    private bool Ataque1;
    private bool Ataque2;
    private bool Ataque3;
    public GameObject Enemigo;
    public Transform RespawnEnemigo, RespawnEnemigo2;
    public GameObject Bullet, Bullet1, HandFloor, HandFloor1;
    public GameObject BulletHand;
    GameObject ObjetoAinstanciar;
    public GameObject Bolaenergia, BolaenergiaCarga;
    public Transform[] AtanqueTres;
    public Transform[] AtaquesSuelo;
    public Transform[] AtaquesAire;

    //Para detectar la posicion del jugador
    [HideInInspector]
    public bool PlayerIzquierdaSuperior;
    [HideInInspector]
    public bool PlayerIzquierdaInferior;
    [HideInInspector]
    public bool PlayerDerechaSuperior;
    [HideInInspector]
    public bool PlayerDerechaInferior;
    [HideInInspector]
    public bool PlayerCentro;

    //Variables para los efectos
    public GameObject Proteccion, Proteccion2;
    public Transform PuntoSuelo1, PuntoSuelo2 , PuntoMano1, PuntoMano2;
    public GameObject Polvo , PolvoMano;
    public float TiempoDeVidaPolvo;
    public float TiempoDeVidaPolvoMano;
    public GameObject EfectoAbsorcion, EfectoExpulsion, EfectoExpulsionGravedad;
    public GameObject Follaje;
    

    private void Awake()
    {
        _animador = GetComponent<Animator>();
        _arbolCollider = GetComponent<Collider2D>();
        _arbolhealth = GetComponent<BoosHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivarArbol == true)
        {
            StartCoroutine(ActivateArbol());
            ActivarArbol = false;
        }

    }

    private void AtaquesRandom()
    {
        int QueAtaque = Random.Range(1, 4);
        //Nos aseguramos de que no se repitan los ataques, que sean aleatorios
        if (AtaqueRandom == QueAtaque)
        {
            if (QueAtaque == 1)
            {
                QueAtaque = Random.Range(2, 4);
            }else if (QueAtaque == 2)
            {
                QueAtaque = QueAtaque - 1;
            }else if (QueAtaque == 3)
            {
                QueAtaque = Random.Range(1, 3);
            }
        }
        AtaqueRandom = QueAtaque;
        //Verificamos los ataques
        if (QueAtaque == 1)
        {
            Ataque1 = true;
            Proteccion.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 19f;
            Proteccion2.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 19f;
        }

        if (QueAtaque == 2)
        {
            Ataque2 = true;
            Proteccion.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 8f;
            Proteccion2.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 8f;
        }

        if (QueAtaque == 3)
        {
            Ataque3 = true;
            Proteccion.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 12.5f;
            Proteccion2.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 12.5f;
        }

        //Activamos la proteccion
        Proteccion.gameObject.SetActive(true);
        Proteccion2.gameObject.SetActive(true);

        //Realizamos el ataque a ejecutar
        if (Ataque1 == true)
        {
            StartCoroutine(Attack1());
            Ataque1 = false;
        }

        if (Ataque2 == true)
        {
            StartCoroutine(Attack2());
            Ataque2 = false;
        }

        if (Ataque3 == true)
        {
            StartCoroutine(Attack3());
            Ataque3 = false;
        }

        
    }

    private void AtaquesRandomFase2()
    {
        int QueAtaque = Random.Range(1, 4);
        //Nos aseguramos de que no se repitan los ataques, que sean aleatorios
        if (AtaqueRandom == QueAtaque)
        {
            if (QueAtaque == 1)
            {
                QueAtaque = Random.Range(2, 4);
            }
            else if (QueAtaque == 2)
            {
                QueAtaque = QueAtaque - 1;
            }
            else if (QueAtaque == 3)
            {
                QueAtaque = Random.Range(1, 3);
            }
        }
        AtaqueRandom = QueAtaque;
        //Verificamos los ataques 
        if (QueAtaque == 1)
        {
            Ataque1 = true;
            Proteccion.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 14.5f;
            Proteccion2.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 14.5f;
        }

        if (QueAtaque == 2)
        {
            Ataque2 = true;
            Proteccion.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 6f;
            Proteccion2.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 6f;
        }

        if (QueAtaque == 3)
        {
            Ataque3 = true;
            Proteccion.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 12.5f;
            Proteccion2.gameObject.GetComponent<BossProteccion>().TiempoEscudo = 12.5f;
        }

        //Activamos la proteccion
        Proteccion.gameObject.SetActive(true);
        Proteccion2.gameObject.SetActive(true);

        //Realizamos el ataque a ejecutar
        if (Ataque1 == true)
        {
            StartCoroutine(Attack1Fase2());
            Ataque1 = false;
        }

        if (Ataque2 == true)
        {
            StartCoroutine(Attack2Fase2());
            Ataque2 = false;
        }

        if (Ataque3 == true)
        {
            StartCoroutine(Attack3Fase2());
            Ataque3 = false;
        }

        
    }
    private IEnumerator ActivateArbol()
    {
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Mandril);
        yield return new WaitForSeconds(0.5f);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Risaboos);
        _animador.GetComponent<Animator>().enabled = true;
        _arbolCollider.GetComponent<Collider2D>().enabled = true;
        _arbolhealth.GetComponent<BoosHealth>().ActivarPanel();

        //Eliminamos el follaje y aparecemos la animación
        Follaje.transform.GetChild(0).gameObject.SetActive(false);
        Follaje.transform.GetChild(1).gameObject.SetActive(false);
        Follaje.transform.GetChild(2).gameObject.SetActive(false);
        Follaje.transform.GetChild(3).gameObject.SetActive(false);
        Follaje.transform.GetChild(4).gameObject.SetActive(false);
        Follaje.transform.GetChild(5).gameObject.SetActive(true);
        Follaje.transform.GetChild(6).gameObject.SetActive(true);
        Follaje.transform.GetChild(7).gameObject.SetActive(true);
        Follaje.transform.GetChild(8).gameObject.SetActive(true);
        Follaje.transform.GetChild(9).gameObject.SetActive(true);

        yield return new WaitForSeconds(7f);
        AtaquesRandom();
    }

    private IEnumerator ActivarFase2()
    {
        yield return new WaitForSeconds(0.5f);
        _animador.SetBool("Fase2", true);
        yield return new WaitForSeconds(3f);
        AtaquesRandomFase2();
    }

    private IEnumerator Attack1()
    {
        _animador.SetTrigger("Attack1");
        yield return new WaitForSeconds(2f);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoTierra);
        StartCoroutine(DisparosSuelo());
        yield return new WaitForSeconds(13f);
        _animador.SetBool("Attack1EnProceso", true);
        AudioManager.instancia.SonidoTierra.Stop();
        yield return new WaitForSeconds(5f);
        _animador.SetBool("Attack1EnProceso", false); 
        yield return new WaitForSeconds(5f);
        AtaquesRandom();
    }

    private IEnumerator Attack2()
    {
        _animador.SetTrigger("Attack2");
        yield return new WaitForSeconds(1f);
        //instanciamos enemigos
        GameObject Enemy = Instantiate(Enemigo, RespawnEnemigo.transform.position, Quaternion.identity);
        StartCoroutine(DisparosAire(4,10));
        yield return new WaitForSeconds(4f);
        _animador.SetBool("Attack2EnProceso", true);
        yield return new WaitForSeconds(5f);
        _animador.SetBool("Attack2EnProceso", false);
        yield return new WaitForSeconds(5f);
        AtaquesRandom();
    }

    private IEnumerator Attack3()
    {
        _animador.SetTrigger("Attack3");
        yield return new WaitForSeconds(2f);
        //instanciamos enemigos
        GameObject Enemy = Instantiate(Enemigo, RespawnEnemigo.transform.position, Quaternion.identity);
        StartCoroutine(AtaqueBolaCarga(3,16));
        AudioManager.instancia.PlayAudio(AudioManager.instancia.AuraCargando);    
        yield return new WaitForSeconds(9f);
        _animador.SetBool("Attack3EnProceso", true);
        AudioManager.instancia.AuraCargando.Stop();
        yield return new WaitForSeconds(5f);
        _animador.SetBool("Attack3EnProceso", false);   
        yield return new WaitForSeconds(5f);
        AtaquesRandom();
    }


    private IEnumerator Attack1Fase2()
    {
        _animador.SetTrigger("Attack1Fase2");
        yield return new WaitForSeconds(2f);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoTierra);
        StartCoroutine(DisparosSueloFase2());
        yield return new WaitForSeconds(9.9f);
        _animador.SetBool("Attack1EnProcesoFase2", true);
        AudioManager.instancia.SonidoTierra.Stop();
        yield return new WaitForSeconds(5f);
        _animador.SetBool("Attack1EnProcesoFase2", false);
        yield return new WaitForSeconds(3f);
        AtaquesRandomFase2();
    }

    private IEnumerator Attack2Fase2()
    {
        _animador.SetTrigger("Attack2Fase2");
        yield return new WaitForSeconds(1f);
        //instanciamos enemigos
        GameObject Enemy = Instantiate(Enemigo, RespawnEnemigo.transform.position, Quaternion.identity);
        GameObject Enemy2 = Instantiate(Enemigo, RespawnEnemigo2.transform.position, Quaternion.identity);
        StartCoroutine(DisparosAire(3,15));
        yield return new WaitForSeconds(4f);
        _animador.SetBool("Attack2EnProcesoFase2", true);
        yield return new WaitForSeconds(5f);
        _animador.SetBool("Attack2EnProcesoFase2", false);
        yield return new WaitForSeconds(3f);
        AtaquesRandomFase2();
    }

    private IEnumerator Attack3Fase2()
    {
        _animador.SetTrigger("Attack3Fase3");
        yield return new WaitForSeconds(2f);
        //instanciamos enemigos
        GameObject Enemy = Instantiate(Enemigo, RespawnEnemigo.transform.position, Quaternion.identity);
        GameObject Enemy2 = Instantiate(Enemigo, RespawnEnemigo2.transform.position, Quaternion.identity);
        StartCoroutine(AtaqueBolaCarga(2, 21));
        AudioManager.instancia.PlayAudio(AudioManager.instancia.AuraCargando);
        yield return new WaitForSeconds(9f);
        _animador.SetBool("Attack3EnProcesoFase2", true);
        AudioManager.instancia.AuraCargando.Stop();
        yield return new WaitForSeconds(5f);
        _animador.SetBool("Attack3EnProcesoFase2", false);     
        yield return new WaitForSeconds(1f);
        AtaquesRandomFase2();
    }

    //Metodo que se llama desde los eventos del animator para utilizar particulas
    private void InstanciarPolvo()
    {
        GameObject polvoInstanciado = Instantiate(Polvo, PuntoSuelo1.transform.position, Quaternion.identity);
        GameObject polvoInstanciado2 = Instantiate(Polvo, PuntoSuelo2.transform.position, Quaternion.identity);
        polvoInstanciado2.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        if (TiempoDeVidaPolvo > 0f)
        {
            Destroy(polvoInstanciado, TiempoDeVidaPolvo);
            Destroy(polvoInstanciado2, TiempoDeVidaPolvo);
        }
    }

    //Metodo que es llamado desde Animation
    private void InstanciarPolvoMano()
    {
        GameObject polvoHand = Instantiate(PolvoMano, PuntoMano1.transform.position, Quaternion.identity);
        GameObject RockHand = Instantiate(BulletHand, PuntoMano1.transform.position, Quaternion.identity);
        GameObject polvoHand2 = Instantiate(PolvoMano, PuntoMano2.transform.position, Quaternion.identity);
        GameObject RockHand2 = Instantiate(BulletHand, PuntoMano2.transform.position, Quaternion.identity);
        polvoHand2.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        RockHand.gameObject.GetComponent<Proyectil>().direccion.y = 1;
        RockHand2.gameObject.GetComponent<Proyectil>().direccion.y = 1;

        if (TiempoDeVidaPolvoMano > 0f)
        {
            Destroy(polvoHand, TiempoDeVidaPolvoMano);
            Destroy(polvoHand2, TiempoDeVidaPolvoMano);
        }
    }

    private IEnumerator DisparosSuelo()
    {
        GameObject[] Bullets = new GameObject[AtaquesSuelo.Length];
        //instanciamos enemigos
        GameObject Enemy = Instantiate(Enemigo, RespawnEnemigo.transform.position, Quaternion.identity);
        int AtaqueAleatorio = Random.Range(1,4);
        for (int i = 0; i < AtaquesSuelo.Length;i++)
        {
            //Para instanciar un proyectil o el otro
            int ProyectilAleatorio = Random.Range(1, 3);
            if (ProyectilAleatorio == 1)
            {
                ObjetoAinstanciar = Bullet;
            }
            else if (ProyectilAleatorio == 2)
            {
                ObjetoAinstanciar = Bullet1;
            }

            yield return new WaitForSeconds(0.1f);
            Bullets[i] = Instantiate(ObjetoAinstanciar, AtaquesSuelo[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            if (i > 2)
            {
                if (AtaqueAleatorio == 1)
                {
                    i += 1;
                    yield return new WaitForSeconds(0.2f);
                }

                else if (AtaqueAleatorio == 2)
                {
                    i += 2;
                    yield return new WaitForSeconds(0.3f);
                }

                else if (AtaqueAleatorio == 3)
                {
                    
                }
            }         
            
        }

        yield return new WaitForSeconds(8f);
        for (int i = 0; i < AtaquesSuelo.Length; i++)
        {
            Destroy(Bullets[i], 1f);
        }
    }

    private IEnumerator DisparosSueloFase2()
    {
        GameObject[] Bullets = new GameObject[AtaquesSuelo.Length];
        //instanciamos enemigos
        GameObject Enemy = Instantiate(Enemigo, RespawnEnemigo.transform.position, Quaternion.identity);
        GameObject Enemy2 = Instantiate(Enemigo, RespawnEnemigo2.transform.position, Quaternion.identity);
        int AtaqueAleatorio = Random.Range(1, 3);

        for (int i = 0; i < AtaquesSuelo.Length; i++)
        {
            //Para instanciar un proyectil o el otro
            int ProyectilAleatorio = Random.Range(1, 3);
            if (ProyectilAleatorio == 1)
            {
                ObjetoAinstanciar = HandFloor;
            }
            else if (ProyectilAleatorio == 2)
            {
                ObjetoAinstanciar = HandFloor1;
            }

            yield return new WaitForSeconds(0.1f);
            Bullets[i] = Instantiate(ObjetoAinstanciar, AtaquesSuelo[i].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            if (i > 2)
            {
                if (AtaqueAleatorio == 1)
                {
                    
                }
                else if (AtaqueAleatorio == 2)
                {
                    i += 1;
                    yield return new WaitForSeconds(0.5f);
                }
            }

        }

        yield return new WaitForSeconds(1.8f);
        for (int i = 0; i < AtaquesSuelo.Length; i++)
        {
            Destroy(Bullets[i], 1f);
        }
    }

    private IEnumerator DisparosAire(int Aleatorio, float velocidadRock)
    {
        GameObject[] Bullets = new GameObject[AtaquesAire.Length];
        int AtaqueAleatorio = Random.Range(1, Aleatorio);

        for (int i = 0; i < AtaquesAire.Length; i++)
        {
            //Para cambiar el tiempo de vuelo entre un proyectil y otro
            int ProyectilAleatorio = Random.Range(1, 4);
            if (ProyectilAleatorio == 1)
            {
                BulletHand.gameObject.GetComponent<Proyectil>().tiempoVuelo = 1.8f;
                if (velocidadRock == 15)
                {
                    BulletHand.gameObject.GetComponent<Proyectil>().tiempoVuelo = 1.2f;
                }
            }
            else if (ProyectilAleatorio == 2)
            {
                BulletHand.gameObject.GetComponent<Proyectil>().tiempoVuelo = 2f;
                if (velocidadRock == 15)
                {
                    BulletHand.gameObject.GetComponent<Proyectil>().tiempoVuelo = 1.4f;
                }
            }
            else if (ProyectilAleatorio == 3)
            {
                BulletHand.gameObject.GetComponent<Proyectil>().tiempoVuelo = 2.05f;
                if (velocidadRock == 15)
                {
                    BulletHand.gameObject.GetComponent<Proyectil>().tiempoVuelo = 1.6f;
                }
            }  
            yield return new WaitForSeconds(0.1f);
            Bullets[i] = Instantiate(BulletHand, AtaquesAire[i].transform.position, Quaternion.identity);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Disparo2Boos);
            Bullets[i].gameObject.GetComponent<Proyectil>().velocidad = velocidadRock;
            yield return new WaitForSeconds(0.1f);

            if (AtaqueAleatorio == 1)
            {
                i += 1;
                yield return new WaitForSeconds(0.5f);
            }

            else if (AtaqueAleatorio == 2)
            {

            }

            else if (AtaqueAleatorio == 3)
            {

                i += 2;
                yield return new WaitForSeconds(0.5f);
            }

        }
    }

    private IEnumerator AtaqueBolaCarga(int aleatorio, float velocidad)
    {
        float TiempoBolaCargada = 0.5f;
        int CadenciaDeAtaque = Random.Range(1, aleatorio);
        GameObject EfectoAbsorver = Instantiate(EfectoAbsorcion, AtanqueTres[1].position, Quaternion.identity);
        GameObject BolaCargaInstanciada = Instantiate(BolaenergiaCarga, AtanqueTres[0].position, Quaternion.identity);
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 7; i++)
        {
            if (CadenciaDeAtaque == 1)
            {
                yield return new WaitForSeconds(1f);
                TiempoBolaCargada = 0.6f;
            }
            else if (CadenciaDeAtaque == 2)
            {
                yield return new WaitForSeconds(1.3f);
                i++;
                TiempoBolaCargada = 2.3f;
            }

            GameObject BolaEnergias = Instantiate(Bolaenergia, AtanqueTres[0].position, Quaternion.identity);
            BolaEnergias.gameObject.GetComponent<Rock>().velocidad = velocidad;
            GameObject EfectoExpulsar = Instantiate(EfectoExpulsion, AtanqueTres[0].position, Quaternion.identity);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Disparo1Boos);

            //para cambiar la rotacion de la bola de energia
            var angulos = BolaEnergias.transform.rotation.eulerAngles;

            if (PlayerIzquierdaSuperior == true)
            {
                angulos.z = -45f;
                BolaEnergias.transform.rotation = Quaternion.Euler(angulos);
            }
            else if (PlayerIzquierdaInferior == true)
            {
                angulos.z = -25f;
                BolaEnergias.transform.rotation = Quaternion.Euler(angulos);
            }
            else if (PlayerDerechaSuperior == true)
            {
                angulos.z = 45f;
                BolaEnergias.transform.rotation = Quaternion.Euler(angulos);
            }
            else if (PlayerDerechaInferior)
            {
                angulos.z = 25f;
                BolaEnergias.transform.rotation = Quaternion.Euler(angulos);
            }
            else if (PlayerCentro)
            {

            }

        }
        yield return new WaitForSeconds(TiempoBolaCargada);

        Destroy(EfectoAbsorver);
        Instantiate(EfectoExpulsionGravedad, AtanqueTres[0].position, Quaternion.identity);
        Destroy(BolaCargaInstanciada);
    }

    //se llama desde BoosHealth
    public void DetenerJefe()
    {
        StopAllCoroutines();
    }

    //se llama desde BoosHealth
    public void ActivateFase2()
    {
        TiempoDeVidaPolvo = 7.5f;
        StopAllCoroutines();
        StartCoroutine(ActivarFase2());
    }

}
