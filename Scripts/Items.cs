using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public GameObject itemAdd;
    public int cantidadItems;
    Inventario inventary;
    ManagerSingleton gameManager;

    public int IDColeccionable;
    private bool DestruccionColeccionable;
    public bool EsInstanciado;


    void Start()
    {
        gameManager = ManagerSingleton.instancia;
        inventary = gameManager.GetComponent<Inventario>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
         
            //Llamamos el sonido
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Estrella);

            //verificamos los slot del inventario y agregamos
            inventary.chekearSlotvacios(itemAdd, itemAdd.name, cantidadItems);

            if (EsInstanciado == true)
            {
                Experiencia.instancia.ColeccionablesDestruidos(IDColeccionable);
            }
            
            Destroy(this.gameObject);

        }
    }

    private void Update()
    {
        if (DestruccionColeccionable == true)
        {
            Destroy(this.gameObject);
        }

        DestruccionColeccionableAlCargar();
    }

    public void DestruccionColeccionableAlCargar()
    {
        if (EsInstanciado == true)
        {
            for (int i = 0; i < Experiencia.instancia.ColeccionablesDestruibles.Length; i++)
            {
                if (IDColeccionable == Experiencia.instancia.ColeccionablesDestruibles[i])
                {
                    DestruccionColeccionable = true;

                }
            }
        }
        
    }


    public void LateUpdate()
    {
        if (Experiencia.instancia.asignarDestruccionColeccionable == true)
        {

            Experiencia.instancia.asignarDestruccionColeccionable = false;
        }
    }

}
