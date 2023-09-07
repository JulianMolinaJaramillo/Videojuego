using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSonidoAmbiente : MonoBehaviour
{
    private ActivarAudio _sonido;
    public int[] Objetos;

    private void Awake()
    {
        _sonido = GetComponentInChildren<ActivarAudio>();
    }

    public void DesactivarSonidos()
    {
        for (int i = 0; i < Objetos.Length; i++)
        {
            transform.GetChild(i).GetComponent<ActivarAudio>().VolumenCero();
        }
    }
}
