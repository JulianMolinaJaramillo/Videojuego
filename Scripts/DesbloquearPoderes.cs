using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesbloquearPoderes : MonoBehaviour
{
    public int IDPoder;
    public GameObject ParticulasChorro;
    public GameObject ParticulasExplosion;

    ManagerSingleton _GameManager;
    Paneles _Panel;
    GameObject _texo;

    private SpriteRenderer _Sprite;

    private void Awake()
    {
        _Sprite = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {
        _GameManager = ManagerSingleton.instancia;
        _Panel = _GameManager.GetComponent<Paneles>();
        _texo = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instancia.Pasos.Stop();
            MovimientoPlayer.instancia.GetComponent<MovimientoPlayer>().enabled = false;
            MovimientoPlayer.instancia._animator.GetComponent<Animator>().SetBool("Walk", false);
            if (IDPoder == 1)
            {
                
                ParticulasChorro.gameObject.SetActive(true);
                MovimientoPlayer.instancia.TajoDesbloqueado = true;
                Experiencia.instancia.PoderTajo = 1;        
                transform.GetChild(3).GetComponent<Animator>().SetBool("AbrirBotella", true);
                AudioManager.instancia.PlayAudio(AudioManager.instancia.ChorroBotella);
            }

            //if (IDPoder == 2)
            //{
            //    MovimientoPlayer.instancia.AtaqueDesbloqueado = true;
            //}

            yield return new WaitForSeconds(3f);
            _Sprite.GetComponent<SpriteRenderer>().enabled = false;
            ParticulasExplosion.gameObject.SetActive(true);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.ExplosionBotella);

            yield return new WaitForSeconds(3.13f);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.AdquirirPoder);

            yield return new WaitForSeconds(0.4f);
            _Panel.GetComponent<Paneles>().activarPanelExperiencia();
            _texo.GetComponent<Experiencia>().PoderDesbloqueado();

            yield return new WaitForSeconds(0.2f);
            Destroy(this.gameObject);
            
        }
        
    }
}
