using UnityEngine;
using System.Collections;

public class NewCameraPosition : MonoBehaviour
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

	void Start ()
    {
        m_thisCamera = GetComponent<Camera>();
        transform.eulerAngles = new Vector3(m_cameraAngle, 0, 0);
	}

    void Update()
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

        // Dans la logique, ça marche, maintenant, il faut que cela s'adapte au nombre de joueurs.
        /*
        Vector3 pos1 = (m_playerOne.transform.position + m_playerTwo.transform.position) * 0.5f;
        Vector3 pos2 = (m_playerThree.transform.position + m_playerFour.transform.position) * 0.5f;
        Vector3 pos = (pos2 - pos1) * 0.5f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, m_cameraHeight, pos.z - m_zModifier), Time.deltaTime * m_lerpSpeed * 5f); // Moves the camera to this position
        */
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
        //float dist = Vector3.Distance(m_playerOne.transform.position, centroid); // Dist between the two players
        transform.position = Vector3.Lerp(transform.position, new Vector3(newPosX, m_cameraHeight, newPosZ - m_zModifier), Time.deltaTime * m_lerpSpeed * 5f); // Moves the camera to this position
        m_thisCamera.fieldOfView = Mathf.Lerp(m_thisCamera.fieldOfView, centroid.magnitude * 2, m_lerpSpeed * Time.deltaTime); // Lerp FOV modification ; dist * 2 → defines FOV
        m_thisCamera.fieldOfView = Mathf.Clamp(m_thisCamera.fieldOfView, m_fovMin, m_fovMax); // Clamp FOV
    }
}