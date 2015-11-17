using UnityEngine;
using System.Collections;

public class Kendo : MonoBehaviour 
{
	[Tooltip("The force of the kendo's bump")]
	public float m_kendoBump;

	// =====
	// If the kendo hits an enemy player
	// =====
	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Vector3 thisCharDir = GetComponentInParent<CharacterManagement>().m_lastDir; // Uses the direction of the attacking player to bump the enemy in the same direction.
			coll.attachedRigidbody.velocity = (thisCharDir * m_kendoBump); // The actual bump
			coll.GetComponent<CharacterManagement>().m_kendoBumped = true; // Tell the bumped character he is bumped (used so that he can jump to reduce the bump intensity)
		}
	}
}