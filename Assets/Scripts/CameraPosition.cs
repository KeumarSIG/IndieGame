using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
	// =====
	// Camera position & angle
	// =====
	[Tooltip("The X angle of the camera")]
	public float m_cameraAngle;

	[Tooltip("The Y of the camera")]
	public float cameraHeight;

	[Tooltip("The Z of the camera (so that it is not completely top/down)")]
	public float m_zModifier;

	// =====
	// Camera FOV
	// =====
	[Tooltip("The minimal value the FOV can have")]
    public float m_fovMin;

	[Tooltip("The maximum value the FOV can have")]
    public float m_fovMax;

	[Tooltip("Low Value = Slow cam movement ; High Value = Fast camera movement")]
    public float lerpSpeed;
	
	// =====
	// Players
	// =====
	[Tooltip("Reference to player 1")]
    public GameObject m_playerOne;

	[Tooltip("Reference to player 2")]
    public GameObject m_playerTwo;

	// =====
	// Reference to self
	// =====
    Camera m_thisCamera;
    


    void Start()
    {
        m_thisCamera = GetComponent<Camera>(); // Reference to this gameobject
		transform.eulerAngles = new Vector3(m_cameraAngle, 0, 0);
		//StartCoroutine(SetAngleUp());
    }



	void Update ()
    {
        // Defines where camera should go
        Vector3 pos = ((m_playerTwo.transform.position - m_playerOne.transform.position) * 0.5f) + m_playerOne.transform.position; // Defines the point between the two players ; divide by 0.5 to get half
        transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, cameraHeight, pos.z - m_zModifier), Time.deltaTime * lerpSpeed * 5f); // Moves the camera to this position

        // Defines the FOV of the camera
        float dist = Vector3.Distance(m_playerOne.transform.position, m_playerTwo.transform.position); // Dist between the two players
        m_thisCamera.fieldOfView = Mathf.Lerp(m_thisCamera.fieldOfView, dist * 2f, lerpSpeed * Time.deltaTime); // Lerp FOV modification ; dist * 2 → defines FOV
        m_thisCamera.fieldOfView = Mathf.Clamp(m_thisCamera.fieldOfView, m_fovMin, m_fovMax); // Clamp FOV
    }
}