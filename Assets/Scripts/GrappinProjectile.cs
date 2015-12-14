using UnityEngine;
using System.Collections;

//Cette classe sert à gérer le comportement du grappin. A mettre sur le prefab de projectile.
public class GrappinProjectile : MonoBehaviour
{ 
    //L'objet qui nous a tiré
    [HideInInspector]
    public Grappin origin;

	[Tooltip ("La vitesse du grappin")]
	public float speed;
	[Tooltip ("Le temps après lequel le grappin s'autodétruit")]
	public float timerDeath;

	void Start ()
	{
		StartCoroutine(ObsolescenceProgrammée());
	}

	void Update()
	{
		//On avance
		transform.position = transform.position + transform.forward * speed / 60;

		//On place la corde
		origin.placerCorde (origin.transform.position, transform.position);
	}

	void OnCollisionEnter (Collision coll)
	{
		//Si on rentre dans quelque chose (qui n'est pas notre origine), on attache la corde entre les deux et on se détruit
		GameObject cgo = coll.gameObject;
		if ((cgo.tag == "Movable" || cgo.tag == "Player" || cgo.tag == "Unmovable") && cgo != origin.gameObject) 
		{
			origin.m_target = coll.collider.attachedRigidbody;
			origin.m_longueur = (coll.transform.position - origin.transform.position).magnitude;
			if (cgo.tag == "Movable" || cgo.tag == "Player")
				origin.pullTarget = true;
			if (cgo.tag == "Unmovable")
				origin.pullTarget = false;
			Destroy(this.gameObject);
		}
		if (cgo.GetComponent<GrappinProjectile>() != null) 
		{
			GrappinProjectile projecEnemy = cgo.GetComponent<GrappinProjectile>();
			origin.m_target = projecEnemy.origin.GetComponent<Rigidbody>();
			origin.m_longueur = (coll.transform.position - origin.transform.position).magnitude;
			origin.pullTarget = true;
			Destroy(projecEnemy.gameObject);
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
