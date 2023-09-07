using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaPisada : MonoBehaviour
{
    private Animator _Animator;
    private Collider2D _Collider;

    private void Awake()
    {
        _Animator = GetComponentInParent<Animator>();
        _Collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            _Animator.SetBool("Plantapisando", true);
        }

        if (collision.tag == "Enemy")
        {
            _Animator.SetBool("Plantapisando", true);
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _Animator.SetBool("Plantapisando", true);

        }

        if (collision.tag == "Enemy")
        {
            _Animator.SetBool("Plantapisando", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _Animator.SetBool("Plantapisando", false);
        }

        if (collision.tag == "Enemy")
        {
            _Animator.SetBool("Plantapisando", false);
        }
    }

}
