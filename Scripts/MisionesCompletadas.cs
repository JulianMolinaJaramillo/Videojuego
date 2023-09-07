using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionesCompletadas : MonoBehaviour
{
    public GameObject NPCs1, NPCs2;
    public GameObject Recompensa1;

    private bool FueAceptada;

    public bool Mision1;
    public bool Mision2;
    public bool Mision3;

    public static MisionesCompletadas isntancia;

    //Para evitarn inconvenientes al rehubicar los npc
    public int Contador;

    private void Start()
    {
        if (isntancia == null)
        {
            isntancia = this;
        }
        Contador = 0;

    }

    private void LateUpdate()
    {
        if (Contador == 0)
        {
            for (int i = 0; i < Experiencia.instancia.npcMisionesCompleted.Length; i++)
            {
                if (NPCs1.GetComponent<HablarNPCMisiones>().idNPC == Experiencia.instancia.npcMisionesCompleted[i])
                {
                    Mision2 = true;
                }
            }
            for (int i = 0; i < Experiencia.instancia.npcMisionesCompleted.Length; i++)
            {
                if (Experiencia.instancia.npcMisionesCompleted[i] != 0)
                {
                    RehubicarNPC(Experiencia.instancia.npcMisionesCompleted[i]);
                }
            }

            Contador = 1;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Mision1 == true)
            {
                if (Mision2 == true)
                {
                    CompletarMisione1(NPCs1.GetComponent<HablarNPCMisiones>().idNPC);
                    Destroy(this.gameObject);
                }
                
                if (Mision2 == false)
                {
                    CompletarMisione1(NPCs1.GetComponent<HablarNPCMisiones>().idNPC);
                    NPCs1.GetComponent<HablarNPCMisiones>().MisionCompleted = true;
                    RehubicarNPC(NPCs1.GetComponent<HablarNPCMisiones>().idNPC);
                    Experiencia.instancia.npcMisionesCompletdas(NPCs1.GetComponent<HablarNPCMisiones>().idNPC);
                    Destroy(this.gameObject);
                }
            }           
        }
    }

    public void CompletarMisione1(int ID)
    {
        for (int i = 0; i < Experiencia.instancia.npcMisiones.Length; i++)
        {
            if (ID == Experiencia.instancia.npcMisiones[i])
            {
                FueAceptada = true;            
            }
        }

        if (FueAceptada == false)
        {
            Destroy(Recompensa1);
        }
        else
        {
            Recompensa1.gameObject.SetActive(true);
        }
    }

    public void RehubicarNPC(int IDNPCAubicar)
    {
        // Rehubicamos el NPC
        if (IDNPCAubicar == 1)
        {
            NPCs1.transform.position = new Vector3(370.46f, 82.97f, 0);
            NPCs1.GetComponent<MovimientoAleatorioObjetos>().PuntoA.transform.position = new Vector3(367.37f, 85.26f, 0);
            NPCs1.GetComponent<MovimientoAleatorioObjetos>().PuntoB.transform.position = new Vector3(373.64f, 84.06f, 0);
            NPCs1.GetComponent<MovimientoAleatorioObjetos>().PuntoC.transform.position = new Vector3(374.35f, 80.23f, 0);
            NPCs1.GetComponent<MovimientoAleatorioObjetos>().PuntoD.transform.position = new Vector3(367.94f, 81.31f, 0);
            Contador = 1;
        }

        if (IDNPCAubicar == 2)
        {
            Contador = 1;
        }

    }
}
