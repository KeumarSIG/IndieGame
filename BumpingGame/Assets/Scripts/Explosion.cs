using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public float m_radius;
	public float m_power;
	public float m_explosiveLift;
	public float m_exploDuration;

	bool m_explosion;
	//bool m_coroutineOff;
	bool m_activated;

	void Start()
	{
		m_explosion = false;
		//m_coroutineOff = true;
		m_activated = false;
		StartCoroutine(ActivateInstance());
		//StartCoroutine(DestroyExplosion());
	}

	void Update () 
	{		
		if (m_explosion == true)
		{
			Vector3 exploPosition = this.transform.position; 
			Collider[] hitColliders = Physics.OverlapSphere(exploPosition, m_radius);
			
			foreach (Collider hit in hitColliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.AddExplosionForce(m_power, exploPosition, m_radius, m_explosiveLift);
				}
			}

			//if (m_coroutineOff == true) StartCoroutine(DestroyExplosion());
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "DeathZone" && m_activated == true)
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			m_explosion = true;
		}
	}

	IEnumerator ActivateInstance()
	{
		yield return new WaitForSeconds(0.1f);
		m_activated = true;
	}

	/*
	IEnumerator DestroyExplosion()
	{
		m_coroutineOff = false;
		yield return new WaitForSeconds(m_exploDuration);
		Destroy (this.gameObject);
	}
	*/

	void OnDrawGizmos()
	{
		if (m_explosion == true)
		{
			Gizmos.color = new Color(1, 0, 0, 0.125f);
			Gizmos.DrawSphere (transform.position, m_radius);
		}
	}
}