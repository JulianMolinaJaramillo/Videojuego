using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarPosicion : MonoBehaviour
{
    public GameObject _hablarPosicion;
	public bool EsNPCMision;

	public bool MiroArriba;
	public bool MiroAbajo;
	public bool Miroizquierda;
	public bool MiroDerecha;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EsNPCMision == true)
        {
			if (MiroDerecha == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().MiroDerecha = true;
			}
			if (MiroArriba == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().MiroArriba = true;
			}

			if (Miroizquierda == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().Miroizquierda = true;
			}
			if (MiroAbajo == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().MiroAbajo = true;
			}
		}

        if (EsNPCMision == false)
        {
			if (MiroDerecha == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().MiroDerecha = true;
			}
			if (MiroArriba == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().MiroArriba = true;
			}

			if (Miroizquierda == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().Miroizquierda = true;
			}
			if (MiroAbajo == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().MiroAbajo = true;
			}
		}
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (EsNPCMision == true)
		{
			if (MiroDerecha == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().MiroDerecha = false;
			}
			if (MiroArriba == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().MiroArriba = false;
			}

			if (Miroizquierda == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().Miroizquierda = false;
			}
			if (MiroAbajo == true)
			{
				_hablarPosicion.GetComponent<HablarNPCMisiones>().MiroAbajo = false;
			}
		}

		if (EsNPCMision == false)
		{
			if (MiroDerecha == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().MiroDerecha = false;
			}
			if (MiroArriba == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().MiroArriba = false;
			}

			if (Miroizquierda == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().Miroizquierda = false;
			}
			if (MiroAbajo == true)
			{
				_hablarPosicion.GetComponent<HablarNPC>().MiroAbajo = false;
			}
		}
		
	}
}
