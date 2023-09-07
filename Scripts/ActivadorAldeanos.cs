using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorAldeanos : MonoBehaviour
{

    public GameObject Aldeanos;
    public bool IngresaAlPueblo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (IngresaAlPueblo == true)
            {
                Aldeanos.gameObject.SetActive(true);
            }

            if (IngresaAlPueblo == false)
            {
                Aldeanos.gameObject.SetActive(false);
            }
        }
    }
}
