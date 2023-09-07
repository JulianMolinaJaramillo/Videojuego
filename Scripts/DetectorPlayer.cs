using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPlayer : MonoBehaviour
{
    public GameObject _ScripArbol;
    public bool PlayerIzquierdaSuperior;
    public bool PlayerIzquierdainferior;
    public bool PlayerCentro;
    public bool PlayerDerechaInferior;
    public bool PlayerDerechaSuperior;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerIzquierdaSuperior)
            {
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaSuperior = true;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerCentro = false;
            }
            else if (PlayerIzquierdainferior)
            {
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaInferior = true;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerCentro = false;
            }
            else if (PlayerCentro)
            {
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerCentro = true;
            }
            else if (PlayerDerechaSuperior)
            {
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaSuperior = true;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerCentro = false;
            }
            else if (PlayerDerechaInferior)
            {
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerIzquierdaInferior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaSuperior = false;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerDerechaInferior = true;
                _ScripArbol.GetComponent<ArbolBoss>().PlayerCentro = false;
            }
        }
    }
}
