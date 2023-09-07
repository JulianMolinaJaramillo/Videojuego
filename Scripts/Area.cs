using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Area : MonoBehaviour
{
    Animator animador;
    // Start is called before the first frame update
    private void Awake()
    {
        animador = GetComponent<Animator>();
    }

    public IEnumerator AreaShow(string name, int tiempoArea)
    {
        animador.Play("AreaShow");
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;

        yield return new WaitForSeconds(tiempoArea);
        animador.Play("AreaFadeOut");
    }
}
