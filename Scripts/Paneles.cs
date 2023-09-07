using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paneles : MonoBehaviour
{
    public GameObject PanelExperiencia;


    public void activarPanelExperiencia()
    {
        PanelExperiencia.gameObject.SetActive(true);
    }
}
