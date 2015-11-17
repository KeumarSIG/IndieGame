using UnityEngine;
using System.Collections;

public class CollisionWithDeathZones : MonoBehaviour 
{
	[Tooltip("Particles played on player's death")]
	public GameObject m_playerDeath; // particles played on player's death

	[Tooltip("The other player")]
	public GameObject m_otherPlayer;

	// =====
	// Respawn position variables
	// =====
	[Tooltip("Minimum x respawn position")]
	public float m_minX;

	[Tooltip("Maximum x respawn position")]
	public float m_maxX;

	[Tooltip("Y respawn position")]
	public float m_Y;

	[Tooltip("Minimum Z respawn position")]
	public float m_minZ;

	[Tooltip("Maximum Z respawn position")]
	public float m_maxZ;

	// Make sure the player doesn't earn more than 1 point
	bool m_hasScoredPoints;

	// The vector 3 using all previous position variables
	Vector3 m_randomSpawnPosition;

	public GameObject m_score;



	void Start()
	{
		//m_score = GetComponent<UpdateScore>();
		m_hasScoredPoints = false;
	}
	        
	// =====
	// On collision with the DeathZone
	// =====
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "DeathZone" && this.gameObject.tag == "Player" && m_hasScoredPoints == false)
		{
			m_hasScoredPoints = true;

			// Particles that show the player is "dead"
			GameObject clone = Instantiate(m_playerDeath, this.transform.position, Quaternion.identity) as GameObject;
			Destroy(clone, clone.GetComponent<ParticleSystem>().duration);

			// Move player's position
			m_randomSpawnPosition = new Vector3 (Random.Range(m_minX, m_maxX),
			                                     m_Y,
			                                     Random.Range(m_minZ, m_maxZ));

			GetComponent<CharacterManagement>().m_canMove = false;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.transform.position = m_randomSpawnPosition;

			m_otherPlayer.GetComponent<CharacterManagement>().m_playerScore += 1;
			m_score.GetComponent<UpdateScore>().ScoreUpdating(); // Updates the score of the game
			if (m_hasScoredPoints == true) StartCoroutine (AddPoints());
		}
	}

	IEnumerator AddPoints()
	{
		yield return new WaitForSeconds(0.5f);
		m_hasScoredPoints = false;
	}
}