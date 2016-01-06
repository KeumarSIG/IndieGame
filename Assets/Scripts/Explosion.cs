using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public float m_radius;
	public float m_power;
	public float m_explosiveLift;
	
	Animator m_thisAnimator;
	bool m_explosion;

	void Start()
	{
		m_thisAnimator = GetComponent<Animator>();
		//print (m_thisAnimator + " :whatevevr");
	}

	void Update () 
	{		
		if (m_explosion == true)
		{
			Vector3 exploPosition = this.transform.position; 
			Collider[] hitColliders = Physics.OverlapSphere(exploPosition, m_radius);
			
			foreach (Collider hit in hitColliders)
			{
                if (hit.gameObject.tag == "Player" && hit.gameObject.tag != "Kendo")
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(m_power, exploPosition, m_radius, m_explosiveLift, ForceMode.Impulse);
                        
                    }
                }
			}
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			m_explosion = true;
			m_thisAnimator.SetBool("isBumping", true);
            if (other.GetComponent<CharacterManagement>().m_kendoStick.activeInHierarchy == true) other.GetComponent<CharacterManagement>().m_kendoStick.SetActive(false);
        }

        if (other.gameObject.tag == "Kendo")
        {
            //if (other.enabled == true) other.enabled = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), other);
        }
	}

	void OnTriggerExit()
	{
		m_explosion = false;
		m_thisAnimator.SetBool("isBumping", false);
	}


	void OnDrawGizmos()
	{
		if (m_explosion == true)
		{
			Gizmos.color = new Color(1, 0, 0, 0.125f);
			Gizmos.DrawSphere (transform.position, m_radius);
		}
	}
}