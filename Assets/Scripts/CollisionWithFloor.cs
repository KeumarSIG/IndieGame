using UnityEngine;
using System.Collections;

public class CollisionWithFloor : MonoBehaviour 
{
	CharacterManagement m_referenceToCharacterMovement; // Ref to self
    CollisionWithDeathZones m_refToCollisionWithDeathZones;

    void Start()
	{
		m_referenceToCharacterMovement = GetComponent<CharacterManagement>();
        m_refToCollisionWithDeathZones = GetComponent<CollisionWithDeathZones>();
    }


	// If player collides with the floor
	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Floor")
		{
            this.GetComponent<CharacterManagement>().m_loseLife = true;

            if (m_referenceToCharacterMovement.m_canMove == false)
            {
                m_referenceToCharacterMovement.m_canMove = true; // used when the player respawns
                m_refToCollisionWithDeathZones.m_hasScoredPoints = false;
            }

            if (m_referenceToCharacterMovement.m_isJumping == true) m_referenceToCharacterMovement.m_isJumping = false; // If char was jumping, now he is not !!
        }
	}
}
