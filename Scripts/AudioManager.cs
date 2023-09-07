using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer Musica, Efectos;

    public AudioSource Bosque, Disparo, BolaFuego, Espada, Estrella, MainMenu, GameOver, Coins, Maderahurt, JarronRoto, CespedCortado, Pasos, Aura, AuraCargando, Salud, Desaparecer, SaveData, 
        MaderaCrujiente, PasosInteriores, PasoPagina, AbrirYCerrar, PuertaAbierta, PuertaBloqueada, SonidoError, ChorroBotella, ExplosionBotella, Tesoro, AparicionBotella, SubirNivel, AdquirirPoder,
        GuardadoExplosion, Risaboos, BoosDead, Disparo1Boos, Disparo2Boos, SonidoTierra, PeleaBoos, Ramas, GritoBruja, Mandril, RisaBruja, SonidoTerror, Pilar, ApagarPilar, FinalJuego;

    //Para tener los medidores de volumen en el scrip
    [Range(-80,10)]
    public float masterVol, effectsVol;

    //Para poder invocarlo desde todos los scripts
    public static AudioManager instancia;

    //Para manejar los Slider
    public Slider masterSlader, effectsSlader;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(Bosque);
        //masterSlader.value = masterVol;
        //effectsSlader.value = effectsVol;

        masterSlader.minValue = -80;
        masterSlader.maxValue = 10;

        effectsSlader.minValue = -80;
        effectsSlader.maxValue = 10;

        masterSlader.value = PlayerPrefs.GetFloat("Musica", 0f);
        effectsSlader.value = PlayerPrefs.GetFloat("MusicaEfectos", 0f);


    }

    // Update is called once per frame
    void Update()
    {
        //MasterVolumen();
        ////EffectsVolumen();
    }

    public void MasterVolumen()
    {
        Guardado.instancia.GuardadoMusica(masterSlader.value);
        Musica.SetFloat("MusicaVolumen", PlayerPrefs.GetFloat("Musica"));
    }

    public void EffectsVolumen()
    {
        Guardado.instancia.GuardadoMusicaEfectos(effectsSlader.value);
        Efectos.SetFloat("EfectosVolumen", PlayerPrefs.GetFloat("MusicaEfectos"));
    }

    public void PlayAudio(AudioSource Audio)
    {
        Audio.Play();
    }

    public void Volumen(int Identificador)
    {
        if (Identificador == 1)
        {
            StartCoroutine(BajarVolumen());
        }
    }

    private IEnumerator BajarVolumen()
    {
        while (PeleaBoos.GetComponent<AudioSource>().volume < 1f)
        {
            PeleaBoos.GetComponent<AudioSource>().volume -= 0.01f;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
