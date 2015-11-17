using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class HazardLinearMovement : MonoBehaviour
{
    public Transform m_startPosition;
    public Transform m_endPosition;
    public float m_travelTime; 
	
	void FixedUpdate ()
    {
        Vector3 currentPosition = Vector3.Lerp(m_startPosition.position, m_endPosition.position, Mathf.Cos(Time.time / m_travelTime * Mathf.PI * 2) * -0.5f + 0.5f);
        GetComponent<Rigidbody>().MovePosition(currentPosition);
	}
}