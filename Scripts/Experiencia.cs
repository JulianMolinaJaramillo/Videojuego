using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experiencia : MonoBehaviour
{
    public Image ExperienciaImagen;
    
    //variables para manipular el panel de experiencia y sus componentes
    public GameObject panel;
    public TextMeshProUGUI textoLevel;
    public Image imagenBoton;

    //Niveles y experiencia guardadar
    public float ExperienciaGanada;
    public float ExperienciaNextLV;
    public int NivelActual = 1;
    public TextMeshProUGUI ActualLV;

    public static Experiencia instancia;

    //Objetos y poderes guardados
    public GameObject ataqueActual;
    public float EstaminaActual;
    public int PoderTajo;

    //Guardado de camaras
    public GameObject CamaraInicial, CamaraDeGuardado, Camara1;
    public int IDcamara;
    

    private Transform _transform;
    public GameObject PuntoOrigen;
    public Vector2 PuntoGuardado;


    //Para asignar los objetos destruibles
    public int[] objetosDestruibles;
    int contador = 0;
    public bool asignarDestruccion = false;

    //Para asignar los npc destruibles
    public int[] npcDestruibles;
    int contadornpc = 0;
    public bool asignarDestruccionNPC = false;

    //Para asignar las misiones aceptadas npc
    public int[] npcMisiones;
    int contadornpcMisiones = 0;

    //Para asignar las misiones completadas npc
    public int[] npcMisionesCompleted;
    int contadornpcMisionesCompleted = 0;

    //Para asignar los coleccionables destruibles
    public int[] ColeccionablesDestruibles;
    int contadorColeccionable = 0;
    public bool asignarDestruccionColeccionable = false;

    //Para asignar las puertas destruibles
    public int[] PuertasDestruibles;
    int contadorPuertas = 0;
    public bool asignarDestruccionPuertas = false;

    //Para asignar los cofres destruibles
    public int[] CofresDestruibles;
    int contadorCofres = 0;
    public bool asignarDestruccionCofres = false;

    //Para asignar los sonidos destruibles
    public int[] SonidosDestruibles;
    int contadorSonidos = 0;
    public bool asignarDestruccionSonidos = false;

    //ManagerSingleton gameManager;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }


    void Start()
    {
        //gameManager = ManagerSingleton.instancia;
        panel.SetActive(false);

        //Volvemos a asignar

        ExperienciaGanada = PlayerPrefs.GetFloat("Experiencia", ExperienciaGanada);
        ExperienciaNextLV = PlayerPrefs.GetFloat("ExperienciaNLV", ExperienciaNextLV);
        NivelActual = PlayerPrefs.GetInt("NivelActual", NivelActual);

        //Asignamos posicion
        PuntoGuardado.x = PlayerPrefs.GetFloat("posicionX", PuntoGuardado.x);
        PuntoGuardado.y = PlayerPrefs.GetFloat("posicionY", PuntoGuardado.y);
        _transform.position = PuntoGuardado;

        //Asignamos salud
        HealthPlayer.instancia.saludTotal = PlayerPrefs.GetInt("SaludGuardada", HealthPlayer.instancia.saludTotal);
        HealthPlayer.instancia.Salud = PlayerPrefs.GetInt("CorazonesGuardados", HealthPlayer.instancia.Salud);
        //HealthPlayer.instancia.corazonHUD = PlayerPrefs.GetFloat("CorazonesGuardados", HealthPlayer.instancia.corazonHUD);
        HealthPlayer.instancia.healthHUD.sizeDelta = new Vector2(HealthPlayer.instancia.corazonHUD * HealthPlayer.instancia.Salud, HealthPlayer.instancia.corazonHUD);

        //Asignamos ataque
        ataqueActual.GetComponent<Atacarhit>().ataque = PlayerPrefs.GetInt("AumentoAtaque", ataqueActual.GetComponent<Atacarhit>().ataque);
      
        //Asignamos dinero
        Banco.instancia.BancoContador = PlayerPrefs.GetFloat("MoneyActual", Banco.instancia.BancoContador);
        Banco.instancia.TextoBanco.text = Banco.instancia.BancoContador.ToString();

        //Asiganmos objetos destruidos y contador
        contador = PlayerPrefs.GetInt("Contadores",contador);

        for (int i = 0; i < objetosDestruibles.Length; i++)
        {
            objetosDestruibles[0] = PlayerPrefs.GetInt("Objetos");
            objetosDestruibles[1] = PlayerPrefs.GetInt("Objetos1");
            objetosDestruibles[2] = PlayerPrefs.GetInt("Objetos2");
            objetosDestruibles[3] = PlayerPrefs.GetInt("Objetos3");
            objetosDestruibles[4] = PlayerPrefs.GetInt("Objetos4");
            objetosDestruibles[5] = PlayerPrefs.GetInt("Objetos5");
            objetosDestruibles[6] = PlayerPrefs.GetInt("Objetos6");
            objetosDestruibles[7] = PlayerPrefs.GetInt("Objetos7");
            objetosDestruibles[8] = PlayerPrefs.GetInt("Objetos8");
            objetosDestruibles[9] = PlayerPrefs.GetInt("Objetos9");
            objetosDestruibles[10] = PlayerPrefs.GetInt("Objetos10");
            objetosDestruibles[11] = PlayerPrefs.GetInt("Objetos11");
            objetosDestruibles[12] = PlayerPrefs.GetInt("Objetos12");
            objetosDestruibles[13] = PlayerPrefs.GetInt("Objetos13");
            objetosDestruibles[14] = PlayerPrefs.GetInt("Objetos14");
            objetosDestruibles[15] = PlayerPrefs.GetInt("Objetos15");
            objetosDestruibles[16] = PlayerPrefs.GetInt("Objetos16");
            objetosDestruibles[17] = PlayerPrefs.GetInt("Objetos17");
            objetosDestruibles[18] = PlayerPrefs.GetInt("Objetos18");
            objetosDestruibles[19] = PlayerPrefs.GetInt("Objetos19");
            objetosDestruibles[20] = PlayerPrefs.GetInt("Objetos20");
            objetosDestruibles[21] = PlayerPrefs.GetInt("Objetos21");
            objetosDestruibles[22] = PlayerPrefs.GetInt("Objetos22");
            objetosDestruibles[23] = PlayerPrefs.GetInt("Objetos23");
            objetosDestruibles[24] = PlayerPrefs.GetInt("Objetos24");
            objetosDestruibles[25] = PlayerPrefs.GetInt("Objetos25");
            objetosDestruibles[26] = PlayerPrefs.GetInt("Objetos26");
            objetosDestruibles[27] = PlayerPrefs.GetInt("Objetos27");
            objetosDestruibles[28] = PlayerPrefs.GetInt("Objetos28");
            objetosDestruibles[29] = PlayerPrefs.GetInt("Objetos29");
            objetosDestruibles[30] = PlayerPrefs.GetInt("Objetos30");
            objetosDestruibles[31] = PlayerPrefs.GetInt("Objetos31");
            objetosDestruibles[32] = PlayerPrefs.GetInt("Objetos32");
            objetosDestruibles[33] = PlayerPrefs.GetInt("Objetos33");
            objetosDestruibles[34] = PlayerPrefs.GetInt("Objetos34");
            objetosDestruibles[35] = PlayerPrefs.GetInt("Objetos35");
            objetosDestruibles[36] = PlayerPrefs.GetInt("Objetos36");
            objetosDestruibles[37] = PlayerPrefs.GetInt("Objetos37");
            objetosDestruibles[38] = PlayerPrefs.GetInt("Objetos38");
            objetosDestruibles[39] = PlayerPrefs.GetInt("Objetos39");
            objetosDestruibles[40] = PlayerPrefs.GetInt("Objetos40");
            objetosDestruibles[41] = PlayerPrefs.GetInt("Objetos41");
            objetosDestruibles[42] = PlayerPrefs.GetInt("Objetos42");
            objetosDestruibles[43] = PlayerPrefs.GetInt("Objetos43");
            objetosDestruibles[44] = PlayerPrefs.GetInt("Objetos44");
            objetosDestruibles[45] = PlayerPrefs.GetInt("Objetos45");
            objetosDestruibles[46] = PlayerPrefs.GetInt("Objetos46");
            objetosDestruibles[47] = PlayerPrefs.GetInt("Objetos47");
            objetosDestruibles[48] = PlayerPrefs.GetInt("Objetos48");
            objetosDestruibles[49] = PlayerPrefs.GetInt("Objetos49");
            objetosDestruibles[50] = PlayerPrefs.GetInt("Objetos50");
            objetosDestruibles[51] = PlayerPrefs.GetInt("Objetos51");
            objetosDestruibles[52] = PlayerPrefs.GetInt("Objetos52");
            objetosDestruibles[53] = PlayerPrefs.GetInt("Objetos53");
            objetosDestruibles[54] = PlayerPrefs.GetInt("Objetos54");
            objetosDestruibles[55] = PlayerPrefs.GetInt("Objetos55");
            objetosDestruibles[56] = PlayerPrefs.GetInt("Objetos56");
        }
        asignarDestruccion = true;

        //Asiganmos NPC destruidos y contador
        contadornpc = PlayerPrefs.GetInt("ContadoresNPC", contadornpc);

        for (int i = 0; i < npcDestruibles.Length; i++)
        {
            npcDestruibles[0] = PlayerPrefs.GetInt("Npc");
            npcDestruibles[1] = PlayerPrefs.GetInt("Npc1");
            npcDestruibles[2] = PlayerPrefs.GetInt("Npc2");
            npcDestruibles[3] = PlayerPrefs.GetInt("Npc3");
            npcDestruibles[4] = PlayerPrefs.GetInt("Npc4");
        }
        asignarDestruccionNPC = true;

        //Asiganmos NPCMisiones aceptadas y contador
        contadornpcMisiones = PlayerPrefs.GetInt("MisionesNPCContador", contadornpcMisiones);

        for (int i = 0; i < npcMisiones.Length; i++)
        {
            npcMisiones[0] = PlayerPrefs.GetInt("NpcMision");
            npcMisiones[1] = PlayerPrefs.GetInt("NpcMision1");
            npcMisiones[2] = PlayerPrefs.GetInt("NpcMision2");
        }

        //Asiganmos NPCMisiones completadas y contador
        contadornpcMisionesCompleted = PlayerPrefs.GetInt("MisionesCompletadasNPCContador", contadornpcMisionesCompleted);

        for (int i = 0; i < npcMisionesCompleted.Length; i++)
        {
            npcMisionesCompleted[0] = PlayerPrefs.GetInt("NpcMisionCompletada");
            npcMisionesCompleted[1] = PlayerPrefs.GetInt("NpcMisionCompletada1");
            npcMisionesCompleted[2] = PlayerPrefs.GetInt("NpcMisionCompletada2");
        }

        //Asiganmos Coleccionables destruidos y contador
        contadorColeccionable = PlayerPrefs.GetInt("ColeccionablesContador", contadorColeccionable);

        for (int i = 0; i < ColeccionablesDestruibles.Length; i++)
        {
            ColeccionablesDestruibles[0] = PlayerPrefs.GetInt("Coleccionables");
            ColeccionablesDestruibles[1] = PlayerPrefs.GetInt("Coleccionables1");
            ColeccionablesDestruibles[2] = PlayerPrefs.GetInt("Coleccionables2");
            ColeccionablesDestruibles[3] = PlayerPrefs.GetInt("Coleccionables3");
            ColeccionablesDestruibles[4] = PlayerPrefs.GetInt("Coleccionables4");
            ColeccionablesDestruibles[5] = PlayerPrefs.GetInt("Coleccionables5");
            ColeccionablesDestruibles[6] = PlayerPrefs.GetInt("Coleccionables6");
            ColeccionablesDestruibles[7] = PlayerPrefs.GetInt("Coleccionables7");
            ColeccionablesDestruibles[8] = PlayerPrefs.GetInt("Coleccionables8");
            ColeccionablesDestruibles[9] = PlayerPrefs.GetInt("Coleccionables9");
            ColeccionablesDestruibles[10] = PlayerPrefs.GetInt("Coleccionables10");
            ColeccionablesDestruibles[11] = PlayerPrefs.GetInt("Coleccionables11");
            ColeccionablesDestruibles[12] = PlayerPrefs.GetInt("Coleccionables12");
        }
        asignarDestruccionColeccionable = true;


        //Asignamos puertas a destruir
        contadorPuertas = PlayerPrefs.GetInt("PuertasContador", contadorPuertas);

        for (int i = 0; i < PuertasDestruibles.Length; i++)
        {
            PuertasDestruibles[0] = PlayerPrefs.GetInt("Puertas");
            PuertasDestruibles[1] = PlayerPrefs.GetInt("Puertas1");
            PuertasDestruibles[2] = PlayerPrefs.GetInt("Puertas2");
        }
        asignarDestruccionPuertas = true;

        //Asignamos cofres a abrir
        contadorCofres = PlayerPrefs.GetInt("CofresContador", contadorCofres);

        for (int i = 0; i < CofresDestruibles.Length; i++)
        {
            CofresDestruibles[0] = PlayerPrefs.GetInt("Cofre0");
            CofresDestruibles[1] = PlayerPrefs.GetInt("Cofre1");
        }
        asignarDestruccionCofres = true;

        //Asignamos sonidos a destruir
        contadorSonidos = PlayerPrefs.GetInt("SonidosContador", contadorSonidos);

        for (int i = 0; i < SonidosDestruibles.Length; i++)
        {
            SonidosDestruibles[0] = PlayerPrefs.GetInt("Sonido0");
            SonidosDestruibles[1] = PlayerPrefs.GetInt("Sonido1");
            SonidosDestruibles[2] = PlayerPrefs.GetInt("Sonido2");
        }
        asignarDestruccionSonidos = true;


        //Asignamos el poder de tajo
        PoderTajo = PlayerPrefs.GetInt("Tajo", PoderTajo);

        //Asignamos la estamina
        EstaminaActual = PlayerPrefs.GetFloat("Estamina", EstaminaActual);

        //Asignamos la camara inicial
        IDcamara = PlayerPrefs.GetInt("Camara", IDcamara);
        if (IDcamara == 1)
        {
            Camara1.gameObject.SetActive(false);
            CamaraDeGuardado.gameObject.SetActive(true);
            Destroy(CamaraInicial.gameObject);
        }

        //Asignamos level actual
        ActualLV.text = "LV" + "  " + NivelActual.ToString();  
        if(instancia == null)
        {
            instancia = this;
        }

        ExperienciaImagen.fillAmount = ExperienciaGanada / ExperienciaNextLV;
    }

    // Update is called once per frame
    void Update()

    {

    }

    public void ModificadorExperiencia(float Experiencia)
    {
        //Guardamos la experiencia obtenida y experiencia de nivel
        ExperienciaGanada += Experiencia;  
             
        ExperienciaImagen.fillAmount = ExperienciaGanada / ExperienciaNextLV;


        if (ExperienciaGanada >= ExperienciaNextLV)
        {
            //Para cada vez que subamos experiencia, necvesitemos el doble para el proximo nivel
            AudioManager.instancia.Pasos.Stop();
            AudioManager.instancia.PasosInteriores.Stop();
            AudioManager.instancia.PlayAudio(AudioManager.instancia.SubirNivel);

            ExperienciaNextLV = ExperienciaNextLV * 2;
            ExperienciaGanada = 0;
            ExperienciaImagen.fillAmount = 0;

            //Modificamos la salud segun la experiencia ganada
            HealthPlayer.instancia.saludTotal += 1;
            HealthPlayer.instancia.healthHUD.sizeDelta = new Vector2(HealthPlayer.instancia.corazonHUD * HealthPlayer.instancia.saludTotal, HealthPlayer.instancia.corazonHUD);
            HealthPlayer.instancia.Salud = HealthPlayer.instancia.saludTotal;

            //Modificamos el ataque segun la experiencia ganada
            Atacarhit.instancia.ataque += 5;

            //Modificamos la estamina segun la experiencia ganada
            EstaminaActual += 0.05f;

            //Modificamos el nivel segun la experiencia ganada
            NivelActual++;
            ActualLV.text = "LV" + "  " + NivelActual.ToString();

            MovimientoPlayer.instancia.GetComponent<MovimientoPlayer>().enabled = false;
            MovimientoPlayer.instancia.GetComponent<Animator>().SetBool("Walk", false);
            panel.SetActive(true);
            textoLevel.text = $"Level Up \n Ataque + 5 \n Aumento De Vida + 1 \n Level Actual {NivelActual}";
        }
        

    }
    //Metodo llamado desde boton OK en experiencia
    public void BorrarPanel()
    {
        textoLevel.text = "";
        imagenBoton.gameObject.SetActive(false);
    }

    //Metodo llamado desde clase DesbloquearPoderes
    public void PoderDesbloqueado()
    {
        textoLevel.text = $"Poder Desbloqueado, mantén presionado la tecla \n Para realizar ataque cargado";
        imagenBoton.gameObject.SetActive(true);
    }
    public void DatosGuardaso()
    {
        //Actualizamos experiencia en guardado y guardado en disco
        Guardado.instancia.GuardadoExperiencia(ExperienciaGanada);
        Guardado.instancia.GuardadoExperienciaNLV(ExperienciaNextLV);
        Guardado.instancia.NivelActual(NivelActual);


        //Volvemos a asignar
        ExperienciaGanada = PlayerPrefs.GetFloat("Experiencia");
        ExperienciaNextLV = PlayerPrefs.GetFloat("ExperienciaNLV");
        NivelActual = PlayerPrefs.GetInt("NivelActual");

        //Guardamos la health actual para despues de morir y los corazones
        Guardado.instancia.SaludActual(HealthPlayer.instancia.saludTotal);
        HealthPlayer.instancia.saludTotal = PlayerPrefs.GetInt("SaludGuardada");

        Guardado.instancia.CorazonesActuales(HealthPlayer.instancia.Salud);
        HealthPlayer.instancia.Salud = PlayerPrefs.GetInt("CorazonesGuardados");

        //Guardamos ataque
        Guardado.instancia.AumentosAtaques(ataqueActual.GetComponent<Atacarhit>().ataque);
        ataqueActual.GetComponent<Atacarhit>().ataque = PlayerPrefs.GetInt("AumentoAtaque");

        //Guardamos estamina
        Guardado.instancia.GuardarEstamina(EstaminaActual);
        EstaminaActual = PlayerPrefs.GetFloat("Estamina");

        //Guardamos el dinero
        Guardado.instancia.DineroActual(Banco.instancia.BancoContador);
        Banco.instancia.BancoContador = PlayerPrefs.GetFloat("MoneyActual");

        //GUARDADO AVABNZADO DE INVENTARIO
        GameData.instancia.LiampiarLista();
        ManagerSingleton.instancia.GetComponent<Inventario>().InventaryData();
        GameData.instancia.Save();

        //Guardamos posicion
        Guardado.instancia.PosicionActual(_transform.position);
        PuntoGuardado.x = PlayerPrefs.GetFloat("posicionX");
        PuntoGuardado.y = PlayerPrefs.GetFloat("posicionY");

        //Guardamos los objetos destruidos y el contador
        Guardado.instancia.ObjetosDestroyers(objetosDestruibles);
        Guardado.instancia.ObjetosDestroyersContador(contador);

        //Guardamos los objetos NPC y el contador
        Guardado.instancia.ObjetosDestroyersContadorNPC(contadornpc);
        Guardado.instancia.NPCDestroyers(npcDestruibles);

        //Guardamos los objetos NPCMisionesAceptdas y el contador
        Guardado.instancia.ContadorNPCMisiones(contadornpcMisiones);
        Guardado.instancia.NPCMisiones(npcMisiones);

        //Guardamos los objetos NPCMisionesCompletadas y el contador
        Guardado.instancia.ContadorNPCMisionesCompletadas(contadornpcMisionesCompleted);
        Guardado.instancia.NPCMisionesCompletadas(npcMisionesCompleted);

        //Guardamos los objetos Coleccionables y el contador
        Guardado.instancia.ColeecionablesDestruidosContador(contadorColeccionable);
        Guardado.instancia.ColeecionablesDestruidos(ColeccionablesDestruibles);

        //Guardamos la camara inicial
        Guardado.instancia.GuardarCamaraInicial(1);

        //guardamos las puertas
        Guardado.instancia.PuertasDestruidasContador(contadorPuertas);
        Guardado.instancia.PuertasADestruir(PuertasDestruibles);

        //guardamos los cofres
        Guardado.instancia.CofresContador(contadorCofres);
        Guardado.instancia.AbrirCofresTesoro(CofresDestruibles);

        //Guardamos sonidos y el contador
        Guardado.instancia.SonidosContador(contadorSonidos);
        Guardado.instancia.QuitarSonidos(SonidosDestruibles);

        //Guardamos el tajo
        Guardado.instancia.GuardarPoderTajo(PoderTajo);
    }

    public void BorrarDatos()
    {
        //Actualizamos experiencia en guardado y guardado en disco
        Guardado.instancia.GuardadoExperiencia(0);
        Guardado.instancia.GuardadoExperienciaNLV(100);
        Guardado.instancia.NivelActual(1);
        Guardado.instancia.SaludActual(2);
        Guardado.instancia.CorazonesActuales(2);
        Guardado.instancia.AumentosAtaques(10);
        Guardado.instancia.DineroActual(0);
        Guardado.instancia.PosicionActual(PuntoOrigen.transform.position);
        Guardado.instancia.ObjetosDestroyers(new int[57]);
        Guardado.instancia.ObjetosDestroyersContador(0);
        Guardado.instancia.NPCDestroyers(new int[5]);
        Guardado.instancia.ObjetosDestroyersContadorNPC(0);
        Guardado.instancia.NPCMisiones(new int[3]);
        Guardado.instancia.ContadorNPCMisiones(0);
        Guardado.instancia.NPCMisionesCompletadas(new int[3]);
        Guardado.instancia.ContadorNPCMisionesCompletadas(0);
        Guardado.instancia.ColeecionablesDestruidosContador(0);
        Guardado.instancia.ColeecionablesDestruidos(new int[13]);
        Guardado.instancia.PuertasDestruidasContador(0);
        Guardado.instancia.PuertasADestruir(new int[3]);
        Guardado.instancia.GuardarCamaraInicial(0);
        Guardado.instancia.CofresContador(0);
        Guardado.instancia.AbrirCofresTesoro(new int[2]);
        Guardado.instancia.SonidosContador(0);
        Guardado.instancia.QuitarSonidos(new int[3]);
        Guardado.instancia.GuardarPoderTajo(0);
        Guardado.instancia.GuardarEstamina(0);

        Debug.Log("BORADO");

    }

    //Guardamos objetos a destruir
    public void ObjetosDestruidos(int IDObjeto)
    {
        for (int i = 0; i < objetosDestruibles.Length; i++)
        {
                objetosDestruibles[contador] = IDObjeto;   
        }
        contador += 1;
    }

    //Guardamos NPC a destruir
    public void npcDestruidos(int IDNPC)
    {
        for (int i = 0; i < npcDestruibles.Length; i++)
        {
            npcDestruibles[contadornpc] = IDNPC;
        }
        contadornpc += 1;
    }

    //Guardamos Misiones aceptadas a destruir
    public void npcMisionesAceptadas(int IDNPC)
    {
        for (int i = 0; i < npcMisiones.Length; i++)
        {
            npcMisiones[contadornpcMisiones] = IDNPC;
        }
        contadornpcMisiones += 1;
    }

    //Guardamos Misiones completadas a destruir
    public void npcMisionesCompletdas(int IDNPC)
    {
        for (int i = 0; i < npcMisionesCompleted.Length; i++)
        {
            npcMisionesCompleted[contadornpcMisionesCompleted] = IDNPC;
        }
        contadornpcMisionesCompleted += 1;
    }

    //Guardamos Coleccionables a destruir
    public void ColeccionablesDestruidos(int IDColecct)
    {
        for (int i = 0; i < ColeccionablesDestruibles.Length; i++)
        {
            ColeccionablesDestruibles[contadorColeccionable] = IDColecct;
        }
        contadorColeccionable += 1;
    }

    //Guardamos Puertas a destruir
    public void PuertaDestruir(int IDPuert)
    {
        for (int i = 0; i < PuertasDestruibles.Length; i++)
        {
            PuertasDestruibles[contadorPuertas] = IDPuert;
        }
        contadorPuertas += 1;
    }

    //Guardamos Cofres a destruir
    public void CofreDestruir(int IDCofre)
    {
        for (int i = 0; i < CofresDestruibles.Length; i++)
        {
            CofresDestruibles[contadorCofres] = IDCofre;
        }
        contadorCofres += 1;
    }

    //Guardamos Sonidos a destruir

    public void SonidoDestruir(int IDsonido)
    {
        for (int i = 0; i < SonidosDestruibles.Length; i++)
        {
            SonidosDestruibles[contadorSonidos] = IDsonido;
        }
        contadorSonidos += 1;
    }

}
