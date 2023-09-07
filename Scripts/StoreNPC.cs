using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StoreNPC : MonoBehaviour
{
    //Agregaremos los items que el vendedor tendra a la venta
    public GameObject[] StoreItems;
    public bool SellItems;
    private Animator PanelStore;
    Inventario inventary;
    bool IsInstanciado;


    private void Start()
    {

        inventary = gameObject.GetComponent<Inventario>();
        PanelStore = gameObject.GetComponent<Animator>();


        SetUpStore();
    }

     private void SetUpStore()
    {
        //Buscaremos en los slots del inventario y alli instanciaremos en la tienda del vendedor
        for(int i = 0; i < StoreItems.Length; i++)
        {
            //Instanciamos el item a vender 

            GameObject itemtosell = Instantiate(StoreItems[i], inventary.slots[i].transform.position, Quaternion.identity);
            //Le colocamos como padre el inventario, false, para que la posicoon del gameobject sea el padre
            itemtosell.transform.SetParent(inventary.slots[i].transform,false);
            itemtosell.transform.localPosition = new Vector3(0, 0, 0);
            itemtosell.name = itemtosell.name.Replace("(Clone)", "");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
       //ManagerSingleton.instancia.gameObject.GetComponent<PausaMenu>().Pausa();    
        PanelStore.SetBool("MostratStore", true);
        MovimientoPlayer.instancia.gameObject.GetComponent<MovimientoPlayer>().DesactivarRygy();
        MovimientoPlayer.instancia.gameObject.GetComponent<Animator>().enabled = false;
        ManagerSingleton.instancia.gameObject.GetComponent<PausaMenu>().PausarTienda();
    }

    public void ExitStore()
    {
        PanelStore.SetBool("MostratStore", false);
        StartCoroutine(DesactivarStore());
        MovimientoPlayer.instancia.gameObject.GetComponent<MovimientoPlayer>().ActivarRygy();
        MovimientoPlayer.instancia.gameObject.GetComponent<Animator>().enabled = true;
        ManagerSingleton.instancia.gameObject.GetComponent<PausaMenu>().DespausarTienda();        
    }

    public void itemVendido()
    {
        SellItems = !SellItems;
     
    }


    //Desactivamos por un momento el collider del NPC para no reactivar la tienda
    IEnumerator DesactivarStore()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.8f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
