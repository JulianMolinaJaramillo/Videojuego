using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float TiempoDeRespawn;
    public GameObject[] enemyRespawn;
    public int[] IDEnemigo;



    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i <= enemyRespawn.Length; i++)
        {
                enemyRespawn[i] = transform.GetChild(i).gameObject;
                break;
        }
    }


    public IEnumerator RespawnEnemigo(int idEnemigo)
    {
        for(int i = 0; i <= enemyRespawn.Length; i++)
        {
            if(IDEnemigo[i] == idEnemigo)
            {
                enemyRespawn[i].SetActive(false);
                yield return new WaitForSeconds(TiempoDeRespawn);
                enemyRespawn[i].SetActive(true);
                enemyRespawn[i].GetComponent<Enemy>().HP = enemyRespawn[i].GetComponent<Enemy>().maxHP + 20;
                enemyRespawn[i].GetComponent<Enemy>().maxHP = enemyRespawn[i].GetComponent<Enemy>().HP;
                enemyRespawn[i].GetComponent<Enemy>().atacando = false;
                break;
            }
        }
        
    }
}
