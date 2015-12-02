using UnityEngine;
using System.Collections;

public class AnisoDrag : MonoBehaviour
{
    public Vector3 m_drag;
    Rigidbody m_rb;

	// Use this for initialization
	void Start ()
    {
        m_rb = GetComponent<Rigidbody>();	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 localVelocity = m_rb.velocity;
        localVelocity = transform.InverseTransformDirection(m_rb.velocity);
        localVelocity.Scale(m_drag);

        m_rb.AddForce(m_rb.rotation * -localVelocity);
        // m_rb.AddForce(m_rb.velocity * drag);
	}
}
