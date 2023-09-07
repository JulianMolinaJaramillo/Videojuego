using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banco : MonoBehaviour
{
    public float BancoContador;
    public Text TextoBanco;


    //<para acceder a esta clase en cualquier momento y desde cualquier clase
    public static Banco instancia;

    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;
        }
    }

    private void Start()
    {
        TextoBanco.text = "X" + BancoContador.ToString();
    }

    public void Money(float efectivo)
    {
        BancoContador += efectivo;
        TextoBanco.text = "X" + BancoContador.ToString();
    }
}
