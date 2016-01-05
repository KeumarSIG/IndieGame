using UnityEngine;
using System.Collections;

public class CollisionWithDeathZones : MonoBehaviour 
{
    [Tooltip("Reference to the object managing the score")]
    public GameObject m_scoreManager;

    [Tooltip("Particles played on player's death")]
	public GameObject m_playerDeath; // particles played on player's death

	[Tooltip("Ref to gamefeel manager")]
	public GameObject m_refToGameFeel;

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

    // Make sure the player doesn't earn more than 1 point → the variable is reset in Collision with floor → it might be optimized
    [HideInInspector]
    public bool m_hasScoredPoints;

	// The vector 3 using all previous position variables
	Vector3 m_randomSpawnPosition;

    int m_lastPlayerNumber;
	
    void Start()
    {
        m_hasScoredPoints = false;
    }

    // =====
    // On collision with the DeathZone
    // =====
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "DeathZone" && this.gameObject.tag == "Player" /*&& m_hasScoredPoints == false*/)
        {
            UpdateScore refToUpdateScore = m_scoreManager.GetComponent<UpdateScore>();
            CharacterManagement refToCharacterManagement = GetComponent<CharacterManagement>();
            m_hasScoredPoints = true;            


            // Particles that show the player is "dead"
            GameObject clone = Instantiate(m_playerDeath, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, clone.GetComponent<ParticleSystem>().duration);



            // Move player's position
            m_randomSpawnPosition = new Vector3(Random.Range(m_minX, m_maxX),
                                                m_Y,
                                                Random.Range(m_minZ, m_maxZ));

            refToCharacterManagement.m_canMove = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = m_randomSpawnPosition;

 
            // If player suicides
            if (refToCharacterManagement.m_lastPlayerWhoHit == 0)
            {
				switch (refToCharacterManagement.m_playerNumber)
				{
					case 1:
                        if (refToUpdateScore.m_playerOneScore > 0)
                        {
                            refToUpdateScore.m_playerOneScore--;
                        }
						break;
						
					case 2:
						if (refToUpdateScore.m_playerTwoScore > 0) refToUpdateScore.m_playerTwoScore--;
						break;
						
					case 3:
						if (refToUpdateScore.m_playerThreeScore > 0) refToUpdateScore.m_playerThreeScore--;
						break;
						
					case 4:
						if (refToUpdateScore.m_playerFourScore > 0) refToUpdateScore.m_playerFourScore--;
						break;
				}
            }

            // else it means he scored some points
            else
            {
                

                switch (refToCharacterManagement.m_lastPlayerWhoHit)
                {
                    case 1:
                        refToUpdateScore.m_playerOneScore++;
                        break;

                    case 2:
						refToUpdateScore.m_playerTwoScore++;
                        break;

                    case 3:
						refToUpdateScore.m_playerThreeScore++;
                        break;

                    case 4:
						refToUpdateScore.m_playerFourScore++;
                        break;
                }
            }

            m_scoreManager.GetComponent<UpdateScore>().ScoreUpdating(); // Updates the score of the game
			refToCharacterManagement.m_lastPlayerWhoHit = 0;
			m_refToGameFeel.GetComponent<Gamefeel>().GamefeelTrigger("ScreenShake", "Default");



            // Reduce HP by 1
            if (this.GetComponent<CharacterManagement>().m_loseLife == true)
            {
                this.GetComponent<CharacterManagement>().m_loseLife = false;

                if (GetComponent<CharacterManagement>().m_currentHealth <= 0)
                {
                    gameObject.SetActive(false);
                }

                else
                {
                    GetComponent<CharacterManagement>().m_currentHealth--;
                    /*
                    if (GetComponent<CharacterManagement>().m_currentHealth <= 0)
                    {
                        gameObject.SetActive(false);
                    }
                    */
                }
            }  
        }
    }   
}