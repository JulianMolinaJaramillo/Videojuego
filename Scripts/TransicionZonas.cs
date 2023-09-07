using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicionZonas : MonoBehaviour
{

    Animator _Transicion;


    private void Awake()
    {
        _Transicion = GetComponent<Animator>();
    }


    public IEnumerator TransicionZona()
    {
        Debug.Log("transicion");
        _Transicion.SetTrigger("Salida");
        yield return new WaitForSeconds(1f);
  
    }
}
