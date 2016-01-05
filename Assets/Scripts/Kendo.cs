using UnityEngine;
using System.Collections;

public class Kendo : MonoBehaviour 
{
	[Tooltip("The force of the kendo's bump")]
	public float m_kendoBump;
    public int m_chanceToTriggerSlowMotion;
    public Gamefeel m_refToGamefeel; // ref to gamefeel manager

    public CharacterManagement thisCharacterManagement;

    [HideInInspector] public int m_kendoNumber;

    void Awake()
    {
        m_kendoNumber = GetComponentInParent<CharacterManagement>().m_playerNumber;
        CharacterManagement thisCharacterManagement = GetComponentInParent<CharacterManagement>();
    }

    // =====
    // If the kendo hits an enemy player
    // =====
    void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
            CharacterManagement refToOtherPlayer = coll.gameObject.GetComponent<CharacterManagement>(); // ref to other player management script
            refToOtherPlayer.m_lastPlayerWhoHit = m_kendoNumber; // last player hit variable 

            thisCharacterManagement.m_rb.velocity = Vector3.zero;
            thisCharacterManagement.m_speed = 0;
            //thisCharacterManagement.m_canMove = false;
            StartCoroutine(lol());

            Vector3 thisCharDir = GetComponentInParent<CharacterManagement>().m_lastDir; // Uses the direction of the attacking player to bump the enemy in the same direction.
            coll.attachedRigidbody.velocity = thisCharDir * (1.5f * m_kendoBump - (m_kendoBump * (refToOtherPlayer.m_armor * 0.01f))); // The actual bump ; formula →    direction * (bumpPower - (bumpPower * armor/100))

            refToOtherPlayer.m_kendoBumped = true; // Tell the bumped character he is bumped (used so that he can jump to reduce the bump intensity)
		
            // Slow motion trigger
			int triggerSlowMotion = Random.Range(0, 100);
			if (triggerSlowMotion <= m_chanceToTriggerSlowMotion)
			{             
                m_refToGamefeel.GetComponent<Gamefeel>().GamefeelTrigger("CameraSwitch", coll.GetComponent<CharacterManagement>().m_playerNumber.ToString());                                   
            }
		}
	}

    
    IEnumerator lol()
    {
        yield return new WaitForSeconds(0.4f);
        thisCharacterManagement.m_canMove = true;
    }
    
}