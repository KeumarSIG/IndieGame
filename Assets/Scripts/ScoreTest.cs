using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTest : MonoBehaviour
{
    public Text[] m_scoresOfPlayers = new Text[4];
	// Use this for initialization
	void Start ()
    {
        for (int i = 0 ; i < 4; i++)
        {
            m_scoresOfPlayers[i].text = m_scoresOfPlayers[i].text + " score";
        }
	}
}
