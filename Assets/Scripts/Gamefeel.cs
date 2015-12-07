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

	void Awake()
	{
		m_originalShake = m_shake;
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
		Time.timeScale *= 0.5f;
		Time.fixedDeltaTime *= 0.5f;
		m_mainCamera.GetComponent<CameraPosition>().m_isFollowingPlayer = true;
		m_mainCamera.GetComponent<CameraPosition>().m_playerFollowed = playerNumber;

		yield return new WaitForSeconds(0.25f);

		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
		m_mainCamera.GetComponent<CameraPosition>().m_isFollowingPlayer = false;
		m_mainCamera.GetComponent<CameraPosition>().m_playerFollowed = "0";
	}
}
