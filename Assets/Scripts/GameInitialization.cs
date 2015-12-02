using UnityEngine;
using System.Collections;

public class GameInitialization : MonoBehaviour 
{
	public GameObject m_floor;
	public Camera m_camera;
	public GameObject m_playerOne;
	public GameObject m_playerTwo;


	Vector3 m_originalCameraPosition;

	// EVENT 1: Screenshake after floor reached final destination and affiliated variables
    bool m_event1 = false; 
	float shake = 10;
	float shakeAmount = 0.2f;
	float shakeDecrease = 15f;

	public GameObject m_floorParticles;

    // EVENT 2: xyz
    bool m_event2 = false;


	// EVENT Rumble:
	bool m_eventRumble = false;
	int i;
	public GameObject[] m_letsGetReadyToRumble;

	// EVENT LAST:
	bool m_eventLast;
	
	void Start()
	{
        i = 0;
		m_originalCameraPosition = m_camera.transform.position;
	}

	void Update () 
	{
		// ==================
		//	EVENT 0: Floor goes down
		// ==================
		if (m_floor.transform.position.y < 1)
		{
			m_floor.GetComponent<Animator>().enabled = false; // Stops the floor animation
			m_floor.transform.position = Vector3.zero; // Makes sure it has a 0,0,0 position
			m_event1 = true;
		}



		// ==================
		//	EVENT 1: Camera shakes
		// ==================
		if (m_event1 == true && m_event2 == false)
		{
			if (shake > 0) 
			{
				// Screen shake
				m_camera.transform.position = new Vector3 (m_camera.transform.position.x + Random.Range(-shakeAmount, shakeAmount),
				                                           m_camera.transform.position.y,
				                                           m_camera.transform.position.z + Random.Range(-shakeAmount, shakeAmount));

				shake -= Time.deltaTime * shakeDecrease;
			}

			else 
			{
				// Reset shake to 0 and the camera to its original position
				shake = 0.0f;
				m_camera.transform.position = m_originalCameraPosition;

				// Spawn a particle system that destroys itself when the particles has finished to play
				GameObject temp_particles = Instantiate(m_floorParticles, Vector3.zero, Quaternion.identity) as GameObject;
				temp_particles.transform.eulerAngles = new Vector3(90, 0, 0);
				Destroy(temp_particles, temp_particles.GetComponent<ParticleSystem>().duration);

				// Stop event 1 & go to event 2
				m_event1 = false;
				m_event2 = true;
				m_eventRumble = true;
				}
		}

		// ==================
		//	EVENT Rumble: Let's get ready to rumble
		// ==================
		if (m_eventRumble == true)
		{
			StartCoroutine(ReadyToRumble());
		}

		// ==================
		//	EVENT LAST: Activated camera
		// ==================
		if (m_eventLast == true)
		{
			StartCoroutine(SetEventLastUp());
		}
	}

    IEnumerator SetEventLastUp()
    {
        yield return new WaitForSeconds(1.5f);
        //m_camera.GetComponent<CameraPosition>().enabled = true;
		m_playerOne.GetComponent<CharacterManagement>().enabled = true;
		m_playerOne.GetComponent<CharacterManagement>().m_canMove = true;
		m_playerTwo.GetComponent<CharacterManagement>().enabled = true;
		m_playerTwo.GetComponent<CharacterManagement>().m_canMove = true;
        Destroy(this.gameObject, 0.1f);
    }

	IEnumerator ReadyToRumble()
	{
		yield return new WaitForSeconds(0.5f);

		m_letsGetReadyToRumble[0].SetActive(true); // Let's
		yield return new WaitForSeconds(0.5f);

		m_letsGetReadyToRumble[1].SetActive(true); // get
		yield return new WaitForSeconds(0.5f);

		m_letsGetReadyToRumble[2].SetActive(true); // ready
		yield return new WaitForSeconds(0.5f);

		m_letsGetReadyToRumble[3].SetActive(true); // to
		yield return new WaitForSeconds(0.5f);

		m_letsGetReadyToRumble[4].SetActive(true); // Rumble
		yield return new WaitForSeconds(0.5f);

		m_eventRumble = false;
		m_eventLast = true;
	}

    void OnDestroy()
    {
        while (i < m_letsGetReadyToRumble.Length)
        {
            //m_letsGetReadyToRumble[i].SetActive(false);
            Destroy(m_letsGetReadyToRumble[i]);
            i++;
        }
    }
}