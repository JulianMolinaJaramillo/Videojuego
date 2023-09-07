using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetoDespesDe : MonoBehaviour
{
    public float TiempoDestruccion;
    public bool mejorDesactivar;

    private void Awake()
    {
        StartCoroutine("DestruirObjetoDespuesDe");
    }
    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine("DestruirObjetoDespuesDe");
    }

 
    private IEnumerator DestruirObjetoDespuesDe()
    {
        yield return new WaitForSeconds(TiempoDestruccion);

        if (mejorDesactivar == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
