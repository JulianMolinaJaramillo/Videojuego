using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar nuestro rect transform del UI
using UnityEngine.UI;

public class SaveRoom : MonoBehaviour
{
    public GameObject PanelSaveGamen,Particulas, ParticulasExplocion;

    public Collider2D _collider;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

    }

    private void Start()
    {

        StartCoroutine("AlIniciar");
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Experiencia.instancia.DatosGuardaso();

            PanelSaveGamen.gameObject.SetActive(true);

            _animator.SetBool("SaveGame", true);
            Particulas.gameObject.SetActive(true);

            AudioManager.instancia.PlayAudio(AudioManager.instancia.SaveData);
            AudioManager.instancia.Pasos.Stop();

            collision.GetComponent<MovimientoPlayer>().enabled = false;
            collision.GetComponent<Animator>().enabled = false;

            Debug.Log("Guardado");

            yield return new WaitForSeconds(3f);

            while (PanelSaveGamen.GetComponent<CanvasGroup>().alpha != 0f)
            {
                PanelSaveGamen.GetComponent<CanvasGroup>().alpha -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }

            PanelSaveGamen.gameObject.SetActive(false);

            AudioManager.instancia.GuardadoExplosion.pitch = 1;
            AudioManager.instancia.PlayAudio(AudioManager.instancia.GuardadoExplosion);

            _animator.SetBool("SaveGame", false);
            ParticulasExplocion.gameObject.SetActive(true);

            Particulas.gameObject.SetActive(false);

            collision.GetComponent<MovimientoPlayer>().enabled = true;
            collision.GetComponent<Animator>().enabled = true;

            _collider.enabled = false;

            yield return new WaitForSeconds(4f);
            PanelSaveGamen.GetComponent<CanvasGroup>().alpha = 1f;
            _collider.enabled = true;
            ParticulasExplocion.gameObject.SetActive(false);
        }
    }

    private IEnumerator AlIniciar()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(4f);
        _collider.enabled = true;
    }

}
