using UnityEngine;
using System.Collections;

public class Gamefeel : MonoBehaviour 
{
	// Screenshake 
	public Camera m_mainCamera;
	public float m_shake;
	public float m_shakeAmount;
	public float m_shakeDecrease;
	float m_originalShake;
	Vector3 m_originalCameraPosition;

    // Slow-motion and camera
    public float m_slowMotionAmount;
    [Tooltip("In seconds")]
    public float m_slowMotionDuration;

	void Awake()
	{
		m_originalShake = m_shake;
        //m_slowMotionAmount = m_slowMotionAmount * 0.01f;
    }

	public void GamefeelTrigger(string gamefeel, string playerNumber)
	{
		switch (gamefeel)
		{
			// Shake the screen
			case "ScreenShake":
				StartCoroutine(ScreenShake());
				break;

			// Switch from main camera to other camera
			case "CameraSwitch":
				StartCoroutine(CameraSwitch(playerNumber));
				break;
		}
	}

	IEnumerator ScreenShake()
	{
		while (m_shake > 0) 
		{
			// Screen shake
			m_mainCamera.transform.position = new Vector3 (m_mainCamera.transform.position.x + Random.Range(-m_shakeAmount, m_shakeAmount),
			                                               m_mainCamera.transform.position.y,
			                                               m_mainCamera.transform.position.z + Random.Range(-m_shakeAmount, m_shakeAmount));
	
			m_shake -= Time.deltaTime * m_shakeDecrease;
			yield return new WaitForEndOfFrame();
		}

		m_shake = m_originalShake;
	}

	IEnumerator CameraSwitch(string playerNumber)
	{
        // Slowmotion
		Time.timeScale *= m_slowMotionAmount;
		Time.fixedDeltaTime *= m_slowMotionAmount;
        m_mainCamera.GetComponent<AudioSource>().pitch *= 0.8f      ;

        if (Time.timeScale < 0.1) Time.timeScale = 0.1f;
        if (Time.fixedDeltaTime < 0.002) Time.fixedDeltaTime = 0.002f;

        // Make camera follow a certain player
		m_mainCamera.GetComponent<CameraPosition>().m_isFollowingPlayer = true;
		m_mainCamera.GetComponent<CameraPosition>().m_playerFollowed = playerNumber;                            

		yield return new WaitForSeconds(m_slowMotionDuration);

        while (Time.timeScale < 0.9)
        {
            Time.timeScale *= 1.05f; // basic time scale
            Time.fixedDeltaTime *= 1.05f; // basic fixedDeltaTime
            m_mainCamera.GetComponent<AudioSource>().pitch *= 1.05f;
            yield return new WaitForEndOfFrame();
        }

        Time.timeScale = 1f; // basic time scale
        Time.fixedDeltaTime = 0.02f; // basic fixedDeltaTime
        m_mainCamera.GetComponent<AudioSource>().pitch = 1;
        m_mainCamera.GetComponent<CameraPosition>().m_isFollowingPlayer = false;
		m_mainCamera.GetComponent<CameraPosition>().m_playerFollowed = "0";
	}
}
