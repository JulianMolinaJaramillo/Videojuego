using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    public GameObject _camara_enable;
    public GameObject _camara_disable;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            _camara_enable.gameObject.SetActive(true);
            _camara_disable.gameObject.SetActive(false);
            Debug.Log("ojala");
      
        }
    }


}
