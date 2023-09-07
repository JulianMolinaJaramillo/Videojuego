using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class StoreItems : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public GameObject itemAdd;
    public int cantidadItems;
    Inventario inventary;
    ManagerSingleton gameManager;
    //
    public int ID;
    PausaMenu Menupausa;


    //DONDE COLOCAREMOS TODOS LOS OBJETOS A VENDER
    public string NombreItem;
    public int PrecioVentaItem;
    public int PrecioCompraItem;
    TextMeshProUGUI PrecioVentaTexto;
    public StoreNPC storeNpc;

    private void Start()
    {
        gameManager = ManagerSingleton.instancia;
        inventary = gameManager.GetComponent<Inventario>();
        Menupausa = gameManager.GetComponent<PausaMenu>();

        NombreItem = itemAdd.name;
        PrecioVentaTexto = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        //buscaremos el primer transform que tenemos como de padre
        storeNpc = transform.root.GetComponent<StoreNPC>();

    }

    private void Update()
    {
        Actualizartexto();
    }
    public void ComprarItem()
    {
        //Sino estoy vendiendo items
        if (!storeNpc.SellItems)
        {
            //Si el precio del item es menor a lo que tenemos en el banco
            if (PrecioVentaItem <= Banco.instancia.BancoContador)
            {
                Banco.instancia.Money(-PrecioVentaItem);
                //Agregamos el item a nuestro inventario
                inventary.chekearSlotvacios(itemAdd, itemAdd.name, cantidadItems);
                PrecioVentaTexto.text = "$" + PrecioVentaItem.ToString();
                AudioManager.instancia.PlayAudio(AudioManager.instancia.Estrella);
            }
            else
            {
                //error sound.
                AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoError);
            }
        }
        else if(inventary.itemsInventario.ContainsKey(itemAdd.name))
        {
            PrecioVentaTexto.text = "$" + PrecioCompraItem.ToString();
            //Vendemos
            inventary.UsarItemsInventario(itemAdd.name);
            //Agregamos el valor de venta del item al banco
            Banco.instancia.Money(PrecioCompraItem);
            AudioManager.instancia.PlayAudio(AudioManager.instancia.Coins);
        }
        else
        {
            //error sound.
            AudioManager.instancia.PlayAudio(AudioManager.instancia.SonidoError);
        }

    }


    public void Actualizartexto()
    {
        if (storeNpc.SellItems)
        {
            PrecioVentaTexto.text = "$" + PrecioCompraItem.ToString();
        }
        else
        {
            PrecioVentaTexto.text = "$" + PrecioVentaItem.ToString();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (ID == 0)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Objeto potente de curación, acelera el cicatrizado de las heridas.");
        }

        if (ID == 1)
        {
            Menupausa.GetComponent<PausaMenu>().ActivarDescripcion("Piedra de sangre preciosa");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Menupausa.GetComponent<PausaMenu>().DesactivarDescripcion();
    }

}
