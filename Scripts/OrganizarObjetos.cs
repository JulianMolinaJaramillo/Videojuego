using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizarObjetos : MonoBehaviour
{
    //variable para actualizar la posicion del fotograma
    public bool fixEveryFrame;
    SpriteRenderer _spray;

    void Awake()
    {
        _spray = GetComponent<SpriteRenderer>();        
        
    }


    // Start is called before the first frame update
    void Start()
    {
        _spray.sortingLayerName = "Player";
        //la posicion nuestra negada multiplicada por 100 y lo redondeamos a entero RoundToInt
        _spray.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (fixEveryFrame)
        {
            _spray.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}
