﻿using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour 
{
	public GameObject m_playerOne;
	public GameObject m_playerTwo;
    public GameObject m_playerThree;
    public GameObject m_playerFour;

    public GameObject m_refToOtherScore;

    [HideInInspector]
    public int m_playerOneScore;

    [HideInInspector]
    public int m_playerTwoScore;

    [HideInInspector]
    public int m_playerThreeScore;

    [HideInInspector]
    public int m_playerFourScore;

    
    void Start () 
	{
        m_playerOneScore = 0;
        m_playerTwoScore = 0;
        m_playerThreeScore = 0;
        m_playerFourScore = 0;
        /*
        GetComponent<GUIText>().text =  "Player 1:     " + m_playerOneScore + "   -   " +
                                        "Player 2:     " + m_playerTwoScore + "   -   " +
                                        "Player 3:     " + m_playerThreeScore + "   -   " +
                                        "Player 4:     " + m_playerFourScore;
                                        */
    }

	public void ScoreUpdating()
	{
        /*
        GetComponent<GUIText>().text =  "Player 1:     " + m_playerOneScore + "   -   " +
                                        "Player 2:     " + m_playerTwoScore + "   -   " +
                                        "Player 3:     " + m_playerThreeScore + "   -   " +
                                        "Player 4:     " + m_playerFourScore;
        */

        m_refToOtherScore.GetComponent<ScoreTest>().ScoreUpdatingReturn
            (m_playerOne.GetComponent<CharacterManagement>().m_currentHealth,
            m_playerTwo.GetComponent<CharacterManagement>().m_currentHealth,
            m_playerThree.GetComponent<CharacterManagement>().m_currentHealth,
            m_playerFour.GetComponent<CharacterManagement>().m_currentHealth);
    }
}