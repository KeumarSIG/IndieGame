using UnityEngine;
using System.Collections;

public class BonusArmor : MonoBehaviour
{
    public float m_bonusArmorAmount;
    public float m_bonusArmorDuration;

	void OnCollisionEnter(Collision collPlayer)
    {
        if (collPlayer.gameObject.tag == "Player")
        {
            CharacterManagement refToCharacterManagement = collPlayer.gameObject.GetComponent<CharacterManagement>();

            refToCharacterManagement.BonusLauncher("BonusArmor", m_bonusArmorDuration); // name of the bonus → go check CharacterManagement
            refToCharacterManagement.m_armor += m_bonusArmorAmount; // The effect on the player

            GetComponentInParent<BonusSpawner>().LaunchCoroutine();

            Destroy(this.gameObject);
        }
    }
}
