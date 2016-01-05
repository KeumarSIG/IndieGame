using UnityEngine;
using System.Collections;

public class VictoryCheck : MonoBehaviour
{
    public GameObject m_playerOne;
    bool m_playerOneActive = true;
    public GameObject m_playerTwo;
    bool m_playerTwoActive = true;
    public GameObject m_playerThree;
    bool m_playerThreeActive = true;
    public GameObject m_playerFour;
    bool m_playerFourActive = true;

    public GameObject m_victoryScreen;

    int stopGame = 3;

    public void CheckVictory()
    {
        if (m_playerOne.activeInHierarchy == false && m_playerOneActive == true)
        {
            stopGame--;
            m_playerOneActive = false;
        }

        if (m_playerTwo.activeInHierarchy == false && m_playerTwoActive == true)
        {
            stopGame--;
            m_playerTwoActive = false;
        }

        if (m_playerThree.activeInHierarchy == false && m_playerThreeActive == true)
        {
            stopGame--;
            m_playerThreeActive = false;
        }

        if (m_playerFour.activeInHierarchy == false && m_playerFourActive == true)
        {
            stopGame--;
            m_playerFourActive = false;
        }

        if (stopGame <= 0)
        {
            if (m_victoryScreen.activeInHierarchy == false) m_victoryScreen.SetActive(true);
        }
    }
}
