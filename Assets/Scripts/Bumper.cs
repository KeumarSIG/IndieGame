using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour 
{
	public float m_bumperForce;
	
	void OnCollisionEnter(Collision coll) 
	{
		print ("Bump_1");
		if (coll.gameObject.tag == "Player") 
		{
			print ("Bump_2");
			foreach (ContactPoint contact in coll.contacts) 
			{
				print ("Contacts: " + coll.contacts);
				contact.otherCollider.attachedRigidbody.AddForce(-1 * contact.normal * m_bumperForce, ForceMode.VelocityChange);
			}
			/*
			print ("bump");

			CharacterManagement refToCharacterManagement = coll.gameObject.GetComponent<CharacterManagement>();

			Vector3 bumperPos = this.transform.position; // init
			Vector3 playerPos = coll.transform.position; // target
			Vector3 bumpDirection = playerPos - bumperPos;

			Debug.DrawLine(bumperPos, playerPos);

			coll.gameObject.GetComponent<Rigidbody>().AddForce(bumpDirection * 20);
			*/
		}
		/*
		coll.gameObject.GetComponent<Rigidbody>().velocity = bumpDirection * m_bumperForce; // The actual bump ; formula →    direction * (bumpPower - (bumpPower * armor/100))
		refToCharacterManagement.m_kendoBumped = true; // Tell the bumped character he is bumped (used so that he can jump to reduce the bump intensity)

		if (coll.gameObject.tag == "Player") 
		{
			//print ("bump");
			//print("First normal of the point that collide: " + coll.contacts[0].normal);
			//coll.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, coll.transform.position, 100f, 2f);


			//coll.gameObject.GetComponent<Rigidbody>().velocity = -coll.gameObject.GetComponent<Rigidbody>().velocity * 50;
			coll.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 20);
		}
		*/
	}
}