using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincipalMenu : MonoBehaviour
{

    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        Scene Scena = SceneManager.GetActiveScene();

        if(Scena.name == "MainMenu")
        {
            //Detenemos la musica del fondo y activamos la musica del GameOver
            AudioManager.instancia.Bosque.Stop();
            AudioManager.instancia.PlayAudio(AudioManager.instancia.MainMenu);
        }

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StarGame()
    {
        //Para evitar errores al rehubicar los npc
        SceneManager.LoadScene(1);
        Debug.Log("Carga Scena");
    }

    public void QuitGame()
    {
        Application.Quit();
        
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);       
    }

    public void MostrarAjustes()
    {
        settings.SetActive(true);
    }
    public void OcultarAjustes()
    {
        settings.SetActive(false);
    }

}
