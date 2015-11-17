using UnityEngine;
using System.Collections;

public class CollisionWithFloor : MonoBehaviour 
{
	CharacterManagement m_referenceToCharacterMovement; // Ref to self

    void Start()
	{
		m_referenceToCharacterMovement = GetComponent<CharacterManagement>();
	}



	// If player collides with the floor
	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Floor")
		{
            if (m_referenceToCharacterMovement.m_canMove == false)
            {
                m_referenceToCharacterMovement.m_canMove = true; // used when the player respawns
            }

            /*
            if (transform.eulerAngles != Vector3.zero)
            {
                StartCoroutine(ResetCharacterAngle()); // used to make sure the angle of the player is correct ; MIGHT BE DEPRECATED
            }
            */

            if (m_referenceToCharacterMovement.m_isJumping == true) m_referenceToCharacterMovement.m_isJumping = false; // If char was jumping, now he is not !!
        }
	}

	// Reset char angle
    /*
	IEnumerator ResetCharacterAngle()
	{
		yield return new WaitForSeconds(1.5f);
		transform.eulerAngles = Vector3.zero;
	}
    */
}
