using UnityEngine;
using System.Collections;

public class HazardMovement : MonoBehaviour 
{
	public Transform m_hazard;
	public Transform m_startPosition;
	public Transform m_endPosition; 
	public float m_hazardSpeed;
	public float m_hazardTimer;
	public float m_hazardTimerVariation;
	public float m_maxTimer;
	public float m_minTimer;

	bool m_hazardCanMove;
	float m_originalHazardTimer;
	Vector3 m_direction;
	Transform m_destination;
	Rigidbody m_hazardRigidbody;

	void Start()
	{
		m_originalHazardTimer = m_hazardTimer;
		m_hazardRigidbody = m_hazard.GetComponent<Rigidbody>();
		m_hazardCanMove = true;
		SetDestination(m_startPosition);
	}

	void FixedUpdate()
	{
		if (m_hazardCanMove == true)
		{
			m_hazardRigidbody.velocity = (transform.position + m_direction * m_hazardSpeed * Time.fixedDeltaTime);

			if (Vector3.Distance(m_hazard.position, m_destination.position) < (m_hazardSpeed * Time.fixedDeltaTime * 0.1))
			{
				SetDestination(m_destination == m_startPosition ? m_endPosition : m_startPosition);
			}
		}
	}



	void SetDestination(Transform dest)
	{
		m_hazardRigidbody.velocity = Vector3.zero;
		m_hazardCanMove = false;
		m_destination = dest;
		m_direction = (m_destination.position - m_hazard.position).normalized;
		StartCoroutine(MakeHazardMoveAgain());
	}


	IEnumerator MakeHazardMoveAgain()
	{
		yield return new WaitForSeconds(m_hazardTimer);

		m_hazardTimer = m_originalHazardTimer + Random.Range(-m_hazardTimerVariation, m_hazardTimerVariation);
		if (m_hazardTimer < m_minTimer) m_hazardTimer = m_minTimer;
		else if (m_hazardTimer > m_maxTimer) m_hazardTimer = m_maxTimer;

		m_hazardCanMove = true;
	}


	void OnDrawGizmos()
	{
		// Start position
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(m_startPosition.position, m_hazard.localScale);

		// End position
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(m_endPosition.position, m_hazard.localScale);
	}
}