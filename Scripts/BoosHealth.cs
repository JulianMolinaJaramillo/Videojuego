using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoosHealth : MonoBehaviour
{
    public GameObject PanelSalud;
    public Image ImagenHeatlh;
    public GameObject TextDamage;
    public GameObject PuertaBoss;

    public float SaludTotal = 10;
    private float SaludActual;

    private Animator _animador;
    private ArbolBoss _arbol;
    private Collider2D _collider;

    


    private void Awake()
    {
        _animador = GetComponent<Animator>();
        _arbol = GetComponent<ArbolBoss>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SaludActual = SaludTotal;
    }


    //Metodo que es llamado de clase AtacarHit
    public void Atacado(int ataque)
    {
        SaludActual -= ataque;
        ImagenHeatlh.fillAmount = SaludActual / SaludTotal;

        //Para instanciar el texto de daño
        if (ataque >= Atacarhit.instancia.ataque + 7)
        {
            TextDamage.GetComponent<TextMeshPro>().SetText(ataque.ToString() + " CRITICO!");

        }
        else
        {
            TextDamage.GetComponent<TextMeshPro>().SetText(ataque.ToString());
        }
        AudioManager.instancia.PlayAudio(AudioManager.instancia.Maderahurt);
        GameObject TextoUp = Instantiate(TextDamage, new Vector3(transform.position.x,transform.position.y -4,0), Quaternion.identity);

        StartCoroutine(Movertexto(TextoUp));

        if (ImagenHeatlh.fillAmount <= 0.5)
        {
            //_animador.SetBool("Dead", true);
            _arbol.GetComponent<ArbolBoss>().StopAllCoroutines();
            _arbol.GetComponent<ArbolBoss>().ActivateFase2();
        }

        if (SaludActual <= 0)
        {
            //_animador.SetBool("Dead", true);
            _arbol.GetComponent<ArbolBoss>().DetenerJefe();
            StartCoroutine(SonidoDeadBoos());
            _arbol.GetComponent<ArbolBoss>().enabled = false;
            _collider.GetComponent<Collider2D>().enabled = false;
            PuertaBoss.GetComponent<PuertaBoss>().DetenerSecuencia();
            StartCoroutine(DesactivarPanel());
        }
    }

    private IEnumerator SonidoDeadBoos()
    {

        AudioManager.instancia.Risaboos.Stop();
        AudioManager.instancia.PlayAudio(AudioManager.instancia.BoosDead);
        yield return new WaitForSeconds(2.3f);
        AudioManager.instancia.PlayAudio(AudioManager.instancia.MaderaCrujiente);
    }

    //Se llama desde ArbolBoss
    public void ActivarPanel()
    {
        PanelSalud.gameObject.SetActive(true);
    }

    private IEnumerator DesactivarPanel()
    {
        yield return new WaitForSeconds(1.5f);
        PanelSalud.gameObject.SetActive(false);
    }


    IEnumerator Movertexto(GameObject texto)
    {
        Vector2 inicial = new Vector2(texto.transform.position.x, texto.transform.position.y);
        Vector2 Final = new Vector2(texto.transform.position.x + 50, texto.transform.position.y + 50);
        int upTimes = 0;

        while (upTimes < 6)
        {
            upTimes++;
            //Para mover el objeto hacia una posicion
            //Para que cada vez que pasa un frame se mueva al loop del While
            yield return new WaitForEndOfFrame();
            texto.transform.position = Vector2.MoveTowards(inicial, Final, 35f * Time.deltaTime);
        }

        
    }
}
