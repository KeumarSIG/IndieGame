using UnityEngine;
using System.Collections;

//Cette classe sert à gérer le comportement du grappin. A mettre sur le prefab de projectile.
public class GrappinProjectile : MonoBehaviour
{ 
    //L'objet qui nous a tiré
    [HideInInspector]
    public Grappin origin;

	//La vitesse du grappin
	public float speed;
	//Le temps après lequel le grappin s'autodétruit
	public float timerDeath;

	void Start ()
	{
		StartCoroutine(ObsolescenceProgrammée());
	}

	void Update()
	{
		//On avance
		transform.position = transform.position + transform.forward * speed / 60;
	}

	void OnCollisionEnter (Collision coll)
	{
		//Si on rentre dans quelque chose (qui n'est pas notre origine), on attache la corde entre les deux et on se détruit
		if (coll.gameObject.GetComponent<CharacterManagement>() != null && coll.gameObject != origin.gameObject) 
		{
			origin.m_target = coll.collider.attachedRigidbody;
			origin.m_longueur = (coll.transform.position - origin.transform.position).magnitude;
			Destroy(this.gameObject);
		}
	}

	//AUTODESTRUCTION
	IEnumerator ObsolescenceProgrammée ()
	{
		yield return new WaitForSeconds (timerDeath);
		origin.GetComponent<Grappin>().m_noGrap = true;
		Destroy(gameObject);
	}
}
