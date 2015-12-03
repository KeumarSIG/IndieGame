using UnityEngine;
using System.Collections;

public class BonusSpawner : MonoBehaviour
{
    [Tooltip("Delay of spawn after the bonus has been picked up")]
    public float m_spawnDelay;

    [Tooltip("Type of bonus")]
    public GameObject m_bonus;

    bool canSpawn;

    void Awake ()
    {
        Bonus();
	}

    // Because for some reason I cannot access the coroutine directly from another script → needs to be corrected later
    public void LaunchCoroutine()
    {
        StartCoroutine(SpawnBonus());
    }

    // Delay to spawn the bonus
    IEnumerator SpawnBonus()
    {
        yield return new WaitForSeconds(m_spawnDelay);
        Bonus();
    }

    // The bonus spawn
    void Bonus()
    {
        Quaternion bonusRotation = Quaternion.identity;
        bonusRotation.eulerAngles = new Vector3(0, 270, 330);

        GameObject bonus = Instantiate(m_bonus, new Vector3(transform.position.x, 1.25f, transform.position.z), bonusRotation) as GameObject;
        bonus.transform.parent = transform;
    }
}
