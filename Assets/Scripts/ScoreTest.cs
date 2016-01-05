using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTest : MonoBehaviour
{
    public Text[] m_scoresOfPlayers = new Text[4];
    public GameObject m_playerOne;

	// Use this for initialization
	void Start ()
    {
        ScoreUpdatingReturn(0, 0, 0, 0);
	}

    public void ScoreUpdatingReturn(int a, int b, int c, int d)
    {
        m_scoresOfPlayers[0].text = a.ToString();
        m_scoresOfPlayers[1].text = b.ToString();
        m_scoresOfPlayers[2].text = c.ToString();
        m_scoresOfPlayers[3].text = d.ToString();
    }
}
