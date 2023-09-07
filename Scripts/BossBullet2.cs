using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet2 : MonoBehaviour
{
    private Animator _animador;
    public GameObject Dust;
    public int Daño;

    void Awake()
    {
        _animador = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EmpezarAnimacion());
    }


    private IEnumerator EmpezarAnimacion()
    {
        Dust.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        Dust.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Dust.gameObject.SetActive(true);
        _animador.SetBool("Hide", true);
        yield return new WaitForSeconds(1f);
        Dust.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AdherirDaño", Daño);
        }
    }
}
