using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventario : MonoBehaviour
{
    public GameObject[] slots;
    //public GameObject[] backpaks;
    private bool isintantiate;
    TextMeshProUGUI textoPro;
    //Para guardar una referenica de cada item del inventario en el GameData
    public ItemList itemlist;


    //Para detectar items en concreto
    public bool EsUsable;
    public bool EsUnSolouso;

    //Para las puertas
    public int IDPuertas;
    public int Llave1;

    //BotonInventario boton;
    public Dictionary<string, int> itemsInventario = new Dictionary<string, int>();

    private void Start()
    {
        //boton = GetComponent<BotonInventario>();
        //Si existen items en el juego
        if (itemlist != null)
        {
            DataToInventary();
        }

    }

    public void chekearSlotvacios(GameObject ItemAdherir,string ItemNombre,int ItemCantidad)
    {

        
        //Aun no hemos creado items
        isintantiate = false;

        //verificamos slots
        for(int i = 0; i < slots.Length; i++)
        {
            //Si la cantidad de items dentro de ese slot es igual a cero
            if (slots[i].transform.childCount > 0)
            {

                //Futuras referencias
                //slots[i].GetComponent<SlotSript>().EstaUsado = true;
            }
            else if(!isintantiate && slots[i].GetComponent<SlotSript>())
            {
                //Creamos el item en el slot vacio
                //el nombre del item, Le pasamos la cantidad de items que vamos a colocar y el item que se va a instanciar
                if (!itemsInventario.ContainsKey(ItemNombre))
                {
                    GameObject item = Instantiate(ItemAdherir, slots[i].transform.position, Quaternion.identity);
                    //le colocamos al item que estamos revisando el slot como padre
                    item.transform.SetParent(slots[i].transform, false);
                    //le hacemos un reinicio a la imagen
                    item.transform.localPosition = new Vector3(0, 0, 0);

                    item.name = item.name.Replace("(Clone)", "");
                    slots[i].GetComponent<SlotSript>().EstaUsado = true;

                    //Agregamos el item al diccionario
                    itemsInventario.Add(ItemNombre, ItemCantidad);

                    //modificamos la cantidad de items
                    textoPro = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                    textoPro.text = ItemCantidad.ToString();
                    break;
                }
                else
                {
                    //Verificamos donde hay mas items de ese tipo y le sumamos la cantidad
                    for (int j = 0; j < slots.Length; j++)
                    {
                        //Si el hijo del transform Slot tiene el mismo nombre del item que acabamos de tomar, le sumamos cantidad
                        if(slots[j].transform.GetChild(0).gameObject.name == ItemNombre)
                        {
                            itemsInventario[ItemNombre] += ItemCantidad;
                            textoPro = slots[j].GetComponentInChildren<TextMeshProUGUI>();
                            textoPro.text = itemsInventario[ItemNombre].ToString();
                            break;
                        }
                    }
                    break;
                }

            }
        }
    }

    public void UsarItemsInventario(string Nombreitem)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<SlotSript>().EstaUsado)
            {
                continue;
            }

            if(slots[i].transform.GetChild(0).gameObject.name == Nombreitem && slots[i].transform.GetChild(0).gameObject.name != "llave1(use)" && slots[i].transform.GetChild(0).gameObject.name != "llave2(use)" && slots[i].transform.GetChild(0).gameObject.name != "Collar(use)")
            {
                
                
                textoPro = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                //Restamos un item del inventario
                itemsInventario[Nombreitem]--;
                textoPro.text = itemsInventario[Nombreitem].ToString();
                
                if (itemsInventario[Nombreitem] <= 0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<SlotSript>().EstaUsado = false;
                    itemsInventario.Remove(Nombreitem);
                    ReorganizarInventario();
                }
                EsUnSolouso = false;
                break;
            }         

            if (slots[i].transform.GetChild(0).gameObject.name == "llave1(use)" && EsUsable == true && EsUnSolouso == true && IDPuertas == 1 && Llave1 == 1)
            {
                //Restamos un item del inventario
                itemsInventario[Nombreitem]--;

                if (itemsInventario[Nombreitem] <= 0)
                {
                    EsUnSolouso = false;
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<SlotSript>().EstaUsado = false;
                    itemsInventario.Remove(Nombreitem);
                    ReorganizarInventario();
                }
                break;
            }

            if (slots[i].transform.GetChild(0).gameObject.name == "llave2(use)" && EsUsable == true && EsUnSolouso == true && IDPuertas == 2 && Llave1 == 2)
            {
                //Restamos un item del inventario
                itemsInventario[Nombreitem]--;

                if (itemsInventario[Nombreitem] <= 0)
                {
                    EsUnSolouso = false;
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<SlotSript>().EstaUsado = false;
                    itemsInventario.Remove(Nombreitem);
                    ReorganizarInventario();
                }
                break;
            }

            if (slots[i].transform.GetChild(0).gameObject.name == "Collar(use)" && EsUsable == true && EsUnSolouso == true && IDPuertas == 3 && Llave1 == 3)
            {
                //Restamos un item del inventario
                itemsInventario[Nombreitem]--;

                if (itemsInventario[Nombreitem] <= 0)
                {
                    EsUnSolouso = false;
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<SlotSript>().EstaUsado = false;
                    itemsInventario.Remove(Nombreitem);
                    ReorganizarInventario();
                }
                break;
            }

        }

    }

    public void CambiarEstado()
    {
        EsUnSolouso = true;
    }

    public void VerificarPuerta(int ID)
    {
        if (ID == 3)
        {
            Llave1 = 1;
        }

        if (ID == 4)
        {
            Llave1 = 2;
        }

        if (ID == 5)
        {
            Llave1 = 3;
        }
    }


    private void ReorganizarInventario()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //Sino esta usado el slot, movemos el objeto siguiente hacia atras
            if (!slots[i].GetComponent<SlotSript>().EstaUsado)
            {
                for(int j = i + 1; j < slots.Length; j++)
                {
                    if (slots[j].GetComponent<SlotSript>().EstaUsado)
                    {
                        Transform itemMove = slots[j].transform.GetChild(0).transform;
                        itemMove.transform.SetParent(slots[i].transform, false);
                        //le hacemos un reinicio a la imagen
                        itemMove.transform.localPosition = new Vector3(0, 0, 0);
                        slots[i].GetComponent<SlotSript>().EstaUsado = true;
                        slots[j].GetComponent<SlotSript>().EstaUsado = false;
                        break;
                    }
                }
            }

        }
    }





    //Para grabar la informacion
    public void InventaryData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<SlotSript>().EstaUsado)
            {
                if (!GameData.instancia.saveData.gotoAdderir.Contains(slots[i].GetComponentInChildren<BotonInventario>().ID))
                {
                    GameData.instancia.saveData.gotoAdderir.Add(slots[i].GetComponentInChildren<BotonInventario>().ID);
                    GameData.instancia.saveData.inventarioitemsnombre.Add(slots[i].GetComponentInChildren<BotonInventario>().name);
                    GameData.instancia.saveData.inventarioitemsamount.Add(itemsInventario[slots[i].GetComponentInChildren<BotonInventario>().gameObject.name]);
                }
            }
        }
    }


    

    //Para cargar la informacion
    public void DataToInventary()
    {
        for (int i = 0; i < GameData.instancia.saveData.gotoAdderir.Count; i++)
        {
            for (int j = 0; j < itemlist.items.Count; j++)
            {
                if (itemlist.items[j].ID == GameData.instancia.saveData.gotoAdderir[i])
                {

                    chekearSlotvacios(itemlist.items[j].gameObject, GameData.instancia.saveData.inventarioitemsnombre[i], GameData.instancia.saveData.inventarioitemsamount[i]);

                }
            }
        }
    }


}
