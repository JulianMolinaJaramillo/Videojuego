using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guardado : MonoBehaviour
{
    public static Guardado instancia;
 

    private void Awake()
    {
 
        if(instancia == null)
        {
            instancia = this;
        }
        //Si hay varias instancias, eliminamos esta
        else if(instancia != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //GuardadoExperiencia(0);
        //GuardadoExperienciaNLV(10);
        //NivelActual(1);
        //CorazonesActuales(2);
        DontDestroyOnLoad(gameObject);
    }

    public void GuardadoMusica(float valor)
    {
        PlayerPrefs.SetFloat("Musica", valor);
    }

    public void GuardadoMusicaEfectos(float valor)
    {
        PlayerPrefs.SetFloat("MusicaEfectos", valor);
    }

    public void GuardadoExperiencia(float valor)
    {
        //MAnera de guardar cualquier tipo de informacion PlayerPrefas, en este caso la experiencia
        PlayerPrefs.SetFloat("Experiencia", valor);
    }

    public void GuardadoExperienciaNLV(float valor)
    {
        //MAnera de guardar cualquier tipo de informacion PlayerPrefas, en este caso la experiencia
        PlayerPrefs.SetFloat("ExperienciaNLV", valor);
    }

    public void NivelActual(int valor)
    {
        //MAnera de guardar cualquier tipo de informacion PlayerPrefas, en este caso la experiencia
        PlayerPrefs.SetInt("NivelActual", valor);
    }

    public void SaludActual(int valor)
    {
        PlayerPrefs.SetInt("SaludGuardada", valor);
    }

    public void CorazonesActuales(int valor)
    {
        PlayerPrefs.SetInt("CorazonesGuardados", valor);
    }

    public void AumentosAtaques(int valor)
    {
        PlayerPrefs.SetInt("AumentoAtaque", valor);
    }

    public void DineroActual(float valor)
    {
        PlayerPrefs.SetFloat("MoneyActual", valor);
    }

    public void PosicionActual(Vector2 Posicion)
    {
        PlayerPrefs.SetFloat("posicionX", Posicion.x);
        PlayerPrefs.SetFloat("posicionY", Posicion.y);
    }


    public void ObjetosDestroyersContador(int contador)
    {

        PlayerPrefs.SetInt("Contadores", contador);
       
    }

    public void ObjetosDestroyers(int[] objetos)
    {
   
        PlayerPrefs.SetInt("Objetos", objetos[0]);
        PlayerPrefs.SetInt("Objetos1", objetos[1]);
        PlayerPrefs.SetInt("Objetos2", objetos[2]);
        PlayerPrefs.SetInt("Objetos3", objetos[3]);
        PlayerPrefs.SetInt("Objetos4", objetos[4]);
        PlayerPrefs.SetInt("Objetos5", objetos[5]);
        PlayerPrefs.SetInt("Objetos6", objetos[6]);
        PlayerPrefs.SetInt("Objetos7", objetos[7]);
        PlayerPrefs.SetInt("Objetos8", objetos[8]);
        PlayerPrefs.SetInt("Objetos9", objetos[9]);
        PlayerPrefs.SetInt("Objetos10", objetos[10]);
        PlayerPrefs.SetInt("Objetos11", objetos[11]);
        PlayerPrefs.SetInt("Objetos12", objetos[12]);
        PlayerPrefs.SetInt("Objetos13", objetos[13]);
        PlayerPrefs.SetInt("Objetos14", objetos[14]);
        PlayerPrefs.SetInt("Objetos15", objetos[15]);
        PlayerPrefs.SetInt("Objetos16", objetos[16]);
        PlayerPrefs.SetInt("Objetos17", objetos[17]);
        PlayerPrefs.SetInt("Objetos18", objetos[18]);
        PlayerPrefs.SetInt("Objetos19", objetos[19]);
        PlayerPrefs.SetInt("Objetos20", objetos[20]);
        PlayerPrefs.SetInt("Objetos21", objetos[21]);
        PlayerPrefs.SetInt("Objetos22", objetos[22]);
        PlayerPrefs.SetInt("Objetos23", objetos[23]);
        PlayerPrefs.SetInt("Objetos24", objetos[24]);
        PlayerPrefs.SetInt("Objetos25", objetos[25]);
        PlayerPrefs.SetInt("Objetos26", objetos[26]);
        PlayerPrefs.SetInt("Objetos27", objetos[27]);
        PlayerPrefs.SetInt("Objetos28", objetos[28]);
        PlayerPrefs.SetInt("Objetos29", objetos[29]);
        PlayerPrefs.SetInt("Objetos30", objetos[30]);
        PlayerPrefs.SetInt("Objetos31", objetos[31]);
        PlayerPrefs.SetInt("Objetos32", objetos[32]);
        PlayerPrefs.SetInt("Objetos33", objetos[33]);
        PlayerPrefs.SetInt("Objetos34", objetos[34]);
        PlayerPrefs.SetInt("Objetos35", objetos[35]);
        PlayerPrefs.SetInt("Objetos36", objetos[36]);
        PlayerPrefs.SetInt("Objetos37", objetos[37]);
        PlayerPrefs.SetInt("Objetos38", objetos[38]);
        PlayerPrefs.SetInt("Objetos39", objetos[39]);
        PlayerPrefs.SetInt("Objetos40", objetos[40]);
        PlayerPrefs.SetInt("Objetos41", objetos[41]);
        PlayerPrefs.SetInt("Objetos42", objetos[42]);
        PlayerPrefs.SetInt("Objetos43", objetos[43]);
        PlayerPrefs.SetInt("Objetos44", objetos[44]);
        PlayerPrefs.SetInt("Objetos45", objetos[45]);
        PlayerPrefs.SetInt("Objetos46", objetos[46]);
        PlayerPrefs.SetInt("Objetos47", objetos[47]);
        PlayerPrefs.SetInt("Objetos48", objetos[48]);
        PlayerPrefs.SetInt("Objetos49", objetos[49]);
        PlayerPrefs.SetInt("Objetos50", objetos[50]);
        PlayerPrefs.SetInt("Objetos51", objetos[51]);
        PlayerPrefs.SetInt("Objetos52", objetos[52]);
        PlayerPrefs.SetInt("Objetos53", objetos[53]);
        PlayerPrefs.SetInt("Objetos54", objetos[54]);
        PlayerPrefs.SetInt("Objetos55", objetos[55]);
        PlayerPrefs.SetInt("Objetos56", objetos[56]);
    }

    public void ObjetosDestroyersContadorNPC(int contador)
    {

        PlayerPrefs.SetInt("ContadoresNPC", contador);

    }

    public void NPCDestroyers(int[] npcs)
    {

        PlayerPrefs.SetInt("Npc", npcs[0]);
        PlayerPrefs.SetInt("Npc1", npcs[1]);
        PlayerPrefs.SetInt("Npc2", npcs[2]);
        PlayerPrefs.SetInt("Npc3", npcs[3]);
        PlayerPrefs.SetInt("Npc4", npcs[4]);
    }

    public void ColeecionablesDestruidosContador(int contador)
    {

        PlayerPrefs.SetInt("ColeccionablesContador", contador);

    }

    public void ColeecionablesDestruidos(int[] Coleccionables)
    {

        PlayerPrefs.SetInt("Coleccionables", Coleccionables[0]);
        PlayerPrefs.SetInt("Coleccionables1", Coleccionables[1]);
        PlayerPrefs.SetInt("Coleccionables2", Coleccionables[2]);
        PlayerPrefs.SetInt("Coleccionables3", Coleccionables[3]);
        PlayerPrefs.SetInt("Coleccionables4", Coleccionables[4]);
        PlayerPrefs.SetInt("Coleccionables5", Coleccionables[5]);
        PlayerPrefs.SetInt("Coleccionables6", Coleccionables[6]);
        PlayerPrefs.SetInt("Coleccionables7", Coleccionables[7]);
        PlayerPrefs.SetInt("Coleccionables8", Coleccionables[8]);
        PlayerPrefs.SetInt("Coleccionables9", Coleccionables[9]);
        PlayerPrefs.SetInt("Coleccionables10", Coleccionables[10]);
        PlayerPrefs.SetInt("Coleccionables11", Coleccionables[11]);
        PlayerPrefs.SetInt("Coleccionables12", Coleccionables[12]);
    }

    public void PuertasDestruidasContador(int contador)
    {

        PlayerPrefs.SetInt("PuertasContador", contador);

    }
    public void PuertasADestruir(int[] Puertas)
    {

        PlayerPrefs.SetInt("Puertas", Puertas[0]);
        PlayerPrefs.SetInt("Puertas1", Puertas[1]);
        PlayerPrefs.SetInt("Puertas2", Puertas[2]);
    }

    public void CofresContador(int contador)
    {

        PlayerPrefs.SetInt("CofresContador", contador);

    }

    public void AbrirCofresTesoro(int[] Cofres)
    {
        PlayerPrefs.SetInt("Cofre0", Cofres[0]);
        PlayerPrefs.SetInt("Cofre1", Cofres[1]);
    }

    public void SonidosContador(int contador)
    {
        PlayerPrefs.SetInt("SonidosContador", contador);
    }

    public void QuitarSonidos(int[] Sonidos)
    {
        PlayerPrefs.SetInt("Sonido0", Sonidos[0]);
        PlayerPrefs.SetInt("Sonido1", Sonidos[1]);
        PlayerPrefs.SetInt("Sonido2", Sonidos[2]);
    }

    public void GuardarCamaraInicial(int Camara)
    {
        PlayerPrefs.SetInt("Camara", Camara);
    }

    public void GuardarPoderTajo(int IDTajo)
    {
        PlayerPrefs.SetInt("Tajo", IDTajo);
    }

    public void GuardarEstamina(float Estamina)
    {
        PlayerPrefs.SetFloat("Estamina", Estamina);
    }


    public void ContadorNPCMisiones(int contador)
    {

        PlayerPrefs.SetInt("MisionesNPCContador", contador);

    }

    public void NPCMisiones(int[] npcs)
    {

        PlayerPrefs.SetInt("NpcMision", npcs[0]);
        PlayerPrefs.SetInt("NpcMision1", npcs[1]);
        PlayerPrefs.SetInt("NpcMision2", npcs[2]);
    }

    public void ContadorNPCMisionesCompletadas(int contador)
    {

        PlayerPrefs.SetInt("MisionesCompletadasNPCContador", contador);

    }

    public void NPCMisionesCompletadas(int[] npcs)
    {

        PlayerPrefs.SetInt("NpcMisionCompletada", npcs[0]);
        PlayerPrefs.SetInt("NpcMisionCompletada1", npcs[1]);
        PlayerPrefs.SetInt("NpcMisionCompletada2", npcs[2]);
    }
}
