using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Atacarhit : MonoBehaviour
{
    public int ataque;
    public int ataqueDelMomento;

    public static Atacarhit instancia;

    private void LateUpdate()
    {
        ataqueDelMomento = ataque;
    }

    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Restamos 1 de vida al enemigo
        if (collision.tag == "Enemy")
        {
            ataqueDelMomento = Random.Range(ataque, ataque + 10);

            collision.SendMessage("Atacado", ataqueDelMomento);
        }
        
    }

    
}
