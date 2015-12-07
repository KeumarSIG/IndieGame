using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
    public float m_cameraAngle;
    public float m_cameraHeight;
    public float m_fovMin;
    public float m_fovMax;
    public float m_zModifier;
    public float m_lerpSpeed;
    public int m_numberOfPlayers;

    public GameObject m_playerOne;
    public GameObject m_playerTwo;
    public GameObject m_playerThree;
    public GameObject m_playerFour;

    Camera m_thisCamera;
	[HideInInspector] public bool m_isFollowingPlayer;
	[HideInInspector] public string m_playerFollowed;

	void Start ()
    {
        m_thisCamera = GetComponent<Camera>();
        transform.eulerAngles = new Vector3(m_cameraAngle, 0, 0);
	}

    void Update()
    {
		if (m_isFollowingPlayer == false)
		{
	        switch (m_numberOfPlayers)
	        {
	            // 2 Players
	            case 2:
	                MoveCamera(TwoPlayersCamera().x, TwoPlayersCamera().z, TwoPlayersCamera());
	            break;

	            // 3 Players
	            case 3:
	                MoveCamera(ThreePlayersCamera().x, ThreePlayersCamera().z, ThreePlayersCamera());
	            break;

	            // 4 Players
	            case 4:
	                MoveCamera(FourPlayersCamera().x, FourPlayersCamera().z, FourPlayersCamera());
	            break;
	        }
		}

		else // If slowmotion is activated
		{
			m_thisCamera.fieldOfView = Mathf.Lerp(m_thisCamera.fieldOfView, 30, Time.deltaTime * 5); // Lerp FOV modification ; dist * 2 → defines FOV

			switch (m_playerFollowed)
			{
				case "1":
                    Quaternion targetRotation1 = Quaternion.LookRotation(m_playerOne.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation1, Time.deltaTime);
					break;

				case "2":
                    Quaternion targetRotation2 = Quaternion.LookRotation(m_playerTwo.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation2, Time.deltaTime * 5);
                    //transform.LookAt(m_playerTwo.transform.position);
                    break;

				case "3":
                    Quaternion targetRotation3 = Quaternion.LookRotation(m_playerThree.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation3, Time.deltaTime * 5);
                    //transform.LookAt(m_playerThree.transform.position);
                    break;

				case "4":
                    Quaternion targetRotation4 = Quaternion.LookRotation(m_playerFour.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation4, Time.deltaTime * 5);
                    //transform.LookAt(m_playerFour.transform.position);
                    break;
			}
		}
        
    }

    Vector3 TwoPlayersCamera()
    {
        Vector3 pos = (m_playerOne.transform.position + m_playerTwo.transform.position) * 0.5f;
        return pos;
    }

    Vector3 ThreePlayersCamera()
    {
        Vector3 pos = (m_playerOne.transform.position + m_playerTwo.transform.position) * 0.5f;
        Vector3 pos2 = (pos + m_playerThree.transform.position) * 0.5f;
        return pos2;
    }

    Vector3 FourPlayersCamera()
    {
        Vector3 pos = (m_playerOne.transform.position + m_playerTwo.transform.position) * 0.5f;
        Vector3 pos2 = (m_playerThree.transform.position + m_playerFour.transform.position) * 0.5f;
        Vector3 pos3 = (pos + pos2) * 0.5f;
        return pos3;
    }

    void MoveCamera(float newPosX, float newPosZ, Vector3 centroid)
    {
        float dist = Vector3.Distance(m_playerOne.transform.position, centroid); // Dist between the two players

        transform.position = Vector3.Lerp(transform.position, new Vector3(newPosX, m_cameraHeight, newPosZ - m_zModifier), Time.deltaTime * m_lerpSpeed * 5f); // Moves the camera to this position

        Quaternion targetRotation = Quaternion.LookRotation(centroid - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.01f);

        m_thisCamera.fieldOfView = Mathf.Lerp(m_thisCamera.fieldOfView, 60, Time.deltaTime);
    }
}