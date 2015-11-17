using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour 
{
	public GameObject m_playerOne;
	public GameObject m_playerTwo;

	void Start () 
	{
		GetComponent<GUIText>().text = "Player 1:     " + 
										m_playerOne.GetComponent<CharacterManagement>().m_playerScore.ToString() + 
										"   -   " + 
										m_playerTwo.GetComponent<CharacterManagement>().m_playerScore.ToString() +
										"     : Player 2";
	}

	public void ScoreUpdating()
	{
		GetComponent<GUIText>().text = "Player 1:     " + 
										m_playerOne.GetComponent<CharacterManagement>().m_playerScore.ToString() + 
										"   -   " + 
										m_playerTwo.GetComponent<CharacterManagement>().m_playerScore.ToString() +
										"     : Player 2";
	}
}
