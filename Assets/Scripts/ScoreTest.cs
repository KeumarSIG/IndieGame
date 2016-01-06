using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTest : MonoBehaviour
{
    [Header("Current level")]
    public bool m_currentLevelColorWhite;
    public Material m_newMaterial;

    [Header("Score")]
    public Text[] m_scoresOfPlayers = new Text[4];

    [Header("Player")]
    public GameObject m_playerOne;
    public GameObject m_playerTwo;
    public GameObject m_playerThree;
    public GameObject m_playerFour;

    // Use this for initialization
    void Start ()
    {
        // Level flipper
        if (m_currentLevelColorWhite == true)
        {
            for (int i = 0; i < 4; i++)
            {
                m_scoresOfPlayers[i].material = m_newMaterial;
            }
        }

        int basicHp = m_playerOne.GetComponent<CharacterManagement>().m_currentHealth;
        ScoreUpdatingReturn(basicHp, basicHp, basicHp, basicHp);
	}

    public void ScoreUpdatingReturn(int a, int b, int c, int d)
    {
        m_scoresOfPlayers[0].text = a.ToString() + "♥";
        m_scoresOfPlayers[1].text = b.ToString() + "♥";
        m_scoresOfPlayers[2].text = c.ToString() + "♥";
        m_scoresOfPlayers[3].text = d.ToString() + "♥";
    }
}
