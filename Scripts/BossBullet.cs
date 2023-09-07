using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    private Animator _animador;
    private Collider2D _colliderTrigger;
    private SpriteRenderer _sprite;
    public GameObject Dust;
    public float offsetX;
    public int Daño;


    void Awake()
    {
        _animador = GetComponent<Animator>();
        _colliderTrigger = GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EmpezarAnimacion());
    }


    private IEnumerator EmpezarAnimacion()
    {
        float aumento = -0.8976035f;
        Dust.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        _animador.GetComponent<Animator>().enabled = true;      
        _sprite.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.6f);
        _colliderTrigger.GetComponent<Collider2D>().enabled = true;

        for (int i = 0; i < 7; i++)
        {           
            _colliderTrigger.GetComponent<Collider2D>().offset = new Vector2(offsetX, aumento);
            yield return new WaitForSeconds(0.1f);
            aumento += 0.3f;
        }
        yield return new WaitForSeconds(0.3f);
        Dust.gameObject.SetActive(false);

        yield return new WaitForSeconds(6f);
        _animador.SetBool("Hide", true);
        Dust.gameObject.SetActive(true);

        for (int i = 0; i < 7; i++)
        {
            _colliderTrigger.GetComponent<Collider2D>().offset = new Vector2(offsetX, aumento);
            yield return new WaitForSeconds(0.1f);
            aumento -= 0.3f;
        }

        yield return new WaitForSeconds(0.3f);
        _colliderTrigger.GetComponent<Collider2D>().enabled = false;
        Dust.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AdherirDaño", Daño);
        }
    }
}
