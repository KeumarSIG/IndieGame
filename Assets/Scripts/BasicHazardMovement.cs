using UnityEngine;
using System.Collections;

public class BasicHazardMovement : MonoBehaviour 
{
	public float m_hazardSpeed;
	public float m_cooldown;
	public float m_cooldownVariation;

	bool isMoving;
	bool m_axis; // false: X ; true: Z
	Vector3 m_positionA; // Starting position
	Vector3 m_positionB; // Other position

	void Start () 
	{
		if (transform.position.x > 30 || transform.position.x < -30) m_axis = false;
		else if (transform.position.z > 30 || transform.position.z < -30) m_axis = true;

		print (m_axis);

		if (m_axis == false) // X
		{
			m_positionA = transform.position;
			m_positionB = new Vector3 (-transform.position.x, transform.position.y, transform.position.z);
		}

		else // Z
		{
			m_positionA.z = transform.position.z;
			m_positionB.z = transform.position.z;
		}

		//StartCoroutine (HazardMovementX());
	}

	void Update()
	{
		Vector3.Lerp(transform.position, m_positionB, m_hazardSpeed);
	}

	/*
	void Function()
	{
		Vector3.Lerp(transform.position, m_positionB, m_hazardSpeed);
		//if (Mathf.Abs(transform.position.x) >= Mathf.Abs(m_positionB.x) * 0.9f) 
		//{
			transform.position = m_positionB;
			//isMoving = false;
		//}
	}

	IEnumerator HazardMovementX()
	{
		yield return new WaitForSeconds(m_cooldown);

		if (transform.position == m_positionA) // X here
		{
			isMoving = true;

			while (isMoving == true)
			{
				Function();
				yield return new WaitForEndOfFrame();
			}

			m_cooldown = m_cooldown + Random.Range(-m_cooldownVariation, m_cooldownVariation);
		}

		else
		{
			while (transform.position != m_positionA)
			{
				Vector3.Lerp(transform.position, m_positionA, m_hazardSpeed * Time.deltaTime);
				if (Mathf.Abs(transform.position.x) >= Mathf.Abs(m_positionB.x) * 0.9f) transform.position = m_positionA;
			}
			
			m_cooldown = m_cooldown + Random.Range(-m_cooldownVariation, m_cooldownVariation);
		}

		Mathf.Clamp(m_cooldown, 2, 6);
		StartCoroutine (HazardMovementX());
	}*/
}