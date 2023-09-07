using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProteccion : MonoBehaviour
{
    private Animator _animador;
    private Collider2D _colliderTrigger;
    public GameObject SpriteWait;

    public float TiempoEscudo;

    void Awake()
    {
        _animador = GetComponent<Animator>();
        _colliderTrigger = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EmpezarAnimacion(TiempoEscudo));
    }


    public IEnumerator EmpezarAnimacion(float TiempoEscudo)
    {
        yield return new WaitForSeconds(TiempoEscudo);
        _animador.SetBool("Hide", true);
        yield return new WaitForSeconds(1f);
        _colliderTrigger.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        SpriteWait.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        SpriteWait.gameObject.SetActive(false);
        StartCoroutine(EmpezarAnimacion(TiempoEscudo));        
    }

    private void OnDisable()
    {
        _animador.SetBool("Hide", false);
        _colliderTrigger.GetComponent<Collider2D>().enabled = true;
    }
}
