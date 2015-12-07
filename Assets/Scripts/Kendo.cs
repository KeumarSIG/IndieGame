using UnityEngine;
using System.Collections;

public class Kendo : MonoBehaviour 
{
	[Tooltip("The force of the kendo's bump")]
	public float m_kendoBump;
    [Tooltip("The kendo's number == the player's number")]
    public int m_kendoNumber;

	public Gamefeel m_refToGamefeel;

	// =====
	// If the kendo hits an enemy player
	// =====
		void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
            CharacterManagement refToOtherPlayer = coll.gameObject.GetComponent<CharacterManagement>(); // ref to other player management script
            refToOtherPlayer.m_lastPlayerWhoHit = m_kendoNumber; // last player hit variable 

            Vector3 thisCharDir = GetComponentInParent<CharacterManagement>().m_lastDir; // Uses the direction of the attacking player to bump the enemy in the same direction.
            coll.attachedRigidbody.velocity = thisCharDir * (m_kendoBump - (m_kendoBump * (refToOtherPlayer.m_armor * 0.01f))); // The actual bump ; formula →    direction * (bumpPower - (bumpPower * armor/100))

            refToOtherPlayer.m_kendoBumped = true; // Tell the bumped character he is bumped (used so that he can jump to reduce the bump intensity)
		
			int triggerSlowMotion = Random.Range(0, 100);
			if (triggerSlowMotion <= 10)
			{
				m_refToGamefeel.GetComponent<Gamefeel>().GamefeelTrigger("CameraSwitch", m_kendoNumber.ToString());
			}
		}
	}
}