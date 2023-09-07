using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tajo : MonoBehaviour
{
    //Aparece en le inspector si colocamos el raton encima para pruebas
    [Tooltip("Esperar X segundos antes de destruir el objeto")]
    public float EsperaAntesDeDestruir;

    //Para ocultar en el inspector
    [HideInInspector]
    public Vector2 Mov;

    public float Speed;
    public int ataque;
    private int ataqueDelMomento;

    public static Tajo instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //A nuestra posicion actual la actualizamos sumandole un nuevo vector con los datos que le pasemos  
        transform.position += new Vector3(Mov.x, Mov.y, 0) * Speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        ataqueDelMomento = ataque;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Proteccion")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Objeto")
        {
            yield return new WaitForSeconds(EsperaAntesDeDestruir);
            //Destruimos el tajo cuando chocamos con u objeto
            Destroy(gameObject);
        }else if(collision.tag != "Player" && collision.tag != "Attack")
        {
            ataqueDelMomento = Random.Range(ataque, ataque + 10);

            if (collision.tag == "Enemy") {
                collision.SendMessage("Atacado", ataqueDelMomento);
                
            }
            
            Destroy(gameObject);
        }
    }
}
