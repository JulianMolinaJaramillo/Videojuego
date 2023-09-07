using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAudio : MonoBehaviour
{

    private AudioSource _audioInterno;
    float contadorVolumen = 0;

    private void Awake()
    {
        _audioInterno = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _audioInterno.GetComponent<AudioSource>().enabled = true;
            StopAllCoroutines();
            StartCoroutine(SubirVolumen());
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();

            if (_audioInterno.GetComponent<AudioSource>().volume == 0.3)
            {
                StartCoroutine(BajarVolumen());
            }
            else if (_audioInterno.GetComponent<AudioSource>().volume != 0.3)
            {
                StartCoroutine(BajarVolumen());
                contadorVolumen = _audioInterno.GetComponent<AudioSource>().volume;
            }
        }
    }


    private IEnumerator SubirVolumen()
    {
        while (contadorVolumen < 0.3)
        {
            contadorVolumen += 0.10f;
            _audioInterno.GetComponent<AudioSource>().volume += 0.10f;
            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator BajarVolumen()
    {
        while (contadorVolumen > 0)
        {
            contadorVolumen -= 0.10f;
            _audioInterno.GetComponent<AudioSource>().volume -= 0.10f;
            yield return new WaitForSeconds(0.4f);
        }
        contadorVolumen = 0;
        _audioInterno.GetComponent<AudioSource>().enabled = false;
    }

    public void VolumenCero()
    {
        _audioInterno.GetComponent<AudioSource>().volume = 0.0f;
    }

}
