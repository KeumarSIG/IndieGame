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
            refToCharacterManagement.BonusLauncher("BonusArmor", m_bonusArmorDuration);
            refToCharacterManagement.m_armor += m_bonusArmorAmount;
            refToCharacterManagement.gameObject.transform.localScale = new Vector3(2, 2, 2);
            Destroy(this.gameObject);
        }
    }
}
