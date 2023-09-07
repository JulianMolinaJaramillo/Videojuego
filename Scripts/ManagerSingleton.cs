using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSingleton : MonoBehaviour
{
    public static ManagerSingleton instancia;

    private void Awake()
    {
        instancia = this;
    }
}
