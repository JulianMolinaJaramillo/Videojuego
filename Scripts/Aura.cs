using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    //Tiempo de carga del aura
    public float WaitBeforePlay;
    public GameObject EfectoAbsorcion;
    public GameObject EfectoCarga;

    Animator _animador;
    Coroutine manager;
    bool carga;

    private void Awake()
    {
        _animador = GetComponent<Animator>();
    }

   
    public void AuraStar()
    {
        manager = StartCoroutine(Manager());
        _animador.Play("Aura_Idle");

       
    }

    public void AuraStop()
    {
        StopCoroutine(manager);
        _animador.Play("Aura_Idle");
        EfectoAbsorcion.gameObject.SetActive(false);
        EfectoCarga.gameObject.SetActive(false);
        AudioManager.instancia.AuraCargando.Stop();
        carga = false;
    }

    //comprobamos si ya hemnos cargado lo suficiente
    public IEnumerator Manager()
    {
 
        yield return new WaitForSeconds(WaitBeforePlay);
        _animador.Play("Aura_Play");
        EfectoAbsorcion.gameObject.SetActive(true);
        EfectoCarga.gameObject.SetActive(true);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Aura);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.AuraCargando);
        carga = true;
    }

    //Metodo para comprobar si ya cargamos
    public bool IsLoaded()
    {
        return carga;
    }
}
