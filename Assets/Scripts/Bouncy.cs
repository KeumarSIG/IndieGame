using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour {

	public float puissance;

	//En cas de collision, l'objet est renvoyé dans la direction inverse
	void OnCollisionEnter (Collision coll)
	{
		if (coll.rigidbody != null) 
		{
			ContactPoint cp = coll.contacts [0];
			Debug.Log(coll.gameObject);
			coll.rigidbody.AddForce (- cp.normal * puissance);
			Debug.Log(- cp.normal * puissance);
		}
	}
}
