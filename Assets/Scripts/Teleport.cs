using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
	public Transform m_exitPoint;

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			CharacterManagement refToCharacterManagement = coll.GetComponent<CharacterManagement>();

			if (refToCharacterManagement.m_hasTeleport == false)
			{
				coll.transform.position = m_exitPoint.position;
				refToCharacterManagement.m_hasTeleport = true;
				refToCharacterManagement.StartCoroutine("ResetTeleport");
			}
		}
	}

	/*
	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			CharacterManagement refToCharacterManagement = coll.GetComponent<CharacterManagement>();
			
			if (refToCharacterManagement.m_hasTeleport == true)
			{
				refToCharacterManagement.m_hasTeleport = false;
			}
		}
	}
	*/
}