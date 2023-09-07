using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    public float MonedasADar;

    [SerializeField] GameObject ParticulasLuz;
    [SerializeField] GameObject BurshParticulas;

    private SpriteRenderer _rederer;
    private Collider2D _colider;

    private void Awake()
    {
        _rederer = GetComponent<SpriteRenderer>();
        _colider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Banco.instancia.Money(MonedasADar);
            //Llamamos el sonido
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Coins);

            // deshabilitar collider
            _colider.enabled = false;

            _rederer.enabled = false;
            ParticulasLuz.SetActive(false);
            BurshParticulas.SetActive(true);

            Destroy(this.gameObject, 1f);
            //StartCoroutine("ActivarObjeto");
        }
    }

    private IEnumerator ActivarObjeto()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(true);
        _rederer.enabled = true;
        _colider.enabled = true;
        ParticulasLuz.SetActive(true);
        BurshParticulas.SetActive(false);
    }
}
