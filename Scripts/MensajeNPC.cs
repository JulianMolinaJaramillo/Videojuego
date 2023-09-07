using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar nuestro rect transform del UI
using UnityEngine.UI;
using TMPro;

public class MensajeNPC : MonoBehaviour
{
    int index = 0;
    public int idNPC;

    public TextMeshProUGUI textoBoton1;
    public TextMeshProUGUI textoBoton2;
    private bool DestruccionNPC;


    public string[] mensaje; /*= { "hola perra",*/  //index 0
                                                    //"Dame tu culo," +                          //index 1
                                                    //        "Suerte Marika"};                  //index 2

    public static MensajeNPC instancia;


    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

    private void Update()
    {
        if (DestruccionNPC == true)
        {
            Destroy(gameObject);
        }

        DestruccionNPCAlCargar();
    }

    public void LateUpdate()
    {
        if (Experiencia.instancia.asignarDestruccionNPC == true)
        {

            Experiencia.instancia.asignarDestruccionNPC = false;
        }
    }

    public string ConversacionNPC()
    {
        //Para aumentar los mensajes
        return mensaje[index];
    }

    public void BotonAfirmativo()
    {
        if (idNPC == 1)
        {
            index = 1;
            textoBoton1.text = "No se quien eres, pero gracias";
            textoBoton2.text = "Al parecer no eres de mucha ayuda";
        }

        if (idNPC == 2)
        {
            index = 1;
            textoBoton1.text = "No es mucho, gracias";
            textoBoton2.text = "Eso no sirve de mierda";
        }

        if (idNPC == 3)
        {
            index = 1;
            textoBoton1.text = "Creo que sé a que te refieres";
            textoBoton2.text = "¿Realmente sabes de lo que hablas?";
        }

        if (idNPC == 4)
        {
            index = 1;
            textoBoton1.text = "Acabaré con esto";
            textoBoton2.text = "SI algo le pasa, acabaré contigo";
        }

        if (idNPC == 5)
        {
            index = 1;
            textoBoton1.text = "Muchas gracias, creo que te entiendo";
            textoBoton2.text = "La lectura no sirve de mucho";
        }

    }

    public void BotonNegativo()
    {
        if (idNPC == 1)
        {
            index = 2;
            textoBoton1.text = "Bueno, dime lo que sabes.";
            textoBoton2.text = "Me sé cuidar solo.";
        }

        if (idNPC == 2)
        {
            index = 2;
            textoBoton1.text = "De verdad puedes ayudarme?";
            textoBoton2.text = "No me vuelvas a molestar";
        }

        if (idNPC == 3)
        {
            index = 2;
            textoBoton1.text = "Haber, dime tu adivinanza.";
            textoBoton2.text = "No te estoy pidiendo que me ayudes.";
        }

        if (idNPC == 4)
        {
            index = 2;
            textoBoton1.text = "¿Como?, ¿quien eres realmente?";
            textoBoton2.text = "Ya fuera, no me desconcentres";
        }

        if (idNPC == 5)
        {
            index = 2;
            textoBoton1.text = "Bueno con orar me basta";
            textoBoton2.text = "Tampoco necesito que ores por mi";
        }

    }

    public void ReiniciarTexto()
    {
        textoBoton1.text = "";
        textoBoton2.text = "";
        index = 0;
    }


    public void DestruccionNPCAlCargar()
    {

            for (int i = 0; i < Experiencia.instancia.npcDestruibles.Length; i++)
            {
                if (idNPC == Experiencia.instancia.npcDestruibles[i])
                {
                    DestruccionNPC = true;                   
                }
            }

    }

}
