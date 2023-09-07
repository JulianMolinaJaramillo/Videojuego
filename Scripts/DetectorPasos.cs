using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPasos : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MovimientoPlayer>().EstoyEnInterior = true;
    
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MovimientoPlayer>().EstoyEnInterior = false;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Pasos);
        }
    }

}
