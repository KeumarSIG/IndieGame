using UnityEngine;
using System.Collections;

public class CheckNumberPlayers : MonoBehaviour
{
    public GameObject[] m_players;
    int m_numberOfPlayers;
    int m_newNumberOfPlayers;

    // Check the inital number of players and activate them accordingly
    void Start()
    {
        m_newNumberOfPlayers = Input.GetJoystickNames().Length - 1;
        m_numberOfPlayers = m_newNumberOfPlayers;

        PlayerActivation();
        StartCoroutine(CheckForPlayersChangement());
    }

    // Activate players
    void PlayerActivation()
    {
        for (int i = 0; i < m_newNumberOfPlayers; i++)
        {
            m_players[i].SetActive(true);
        }

        m_numberOfPlayers = m_newNumberOfPlayers;
    }



    // Deactivate players
    void PlayerDeactivation()
    {
        // nothing
    }



    // Coroutine
    IEnumerator CheckForPlayersChangement()
    {
        yield return new WaitForSeconds(1.5f);
        m_newNumberOfPlayers = Input.GetJoystickNames().Length - 1;

        // Si le nombre de joueurs n'a pas changé
        if (m_newNumberOfPlayers == m_numberOfPlayers)
        {
            StartCoroutine(CheckForPlayersChangement());
            yield break;
        }

        // S'il y a des joueurs en plus
        else if (m_newNumberOfPlayers > m_numberOfPlayers)
        {
            print("yoyo");
            PlayerActivation();
        }

        else if (m_newNumberOfPlayers < m_numberOfPlayers)
        {
            PlayerDeactivation();
        }

        StartCoroutine(CheckForPlayersChangement());
    }
}