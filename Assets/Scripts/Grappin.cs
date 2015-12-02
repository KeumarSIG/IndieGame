using UnityEngine;
using System.Collections;

//Cette classe sert à tirer des grappins et à les gérer. A mettre sur les voitures.
public class Grappin : MonoBehaviour {

	//Le prefab de grappin qu'on va utiliser
	public GameObject m_grappin;
	//Le prefab de la corde
	public GameObject m_corde;

	//Deux bool pour dire si je me tire et si je tire ma cible. Je vais les retirer pour faire marcher ca autrement
	public bool pullSelf;
	public bool pullTarget;
	//True si ce joueur n'a aucun grappin présent sur la scène (nécessaire pour tirer)
	public bool m_noGrap;

	//La cible et le tireur
    [HideInInspector]
	public Rigidbody m_target;

    [HideInInspector]
    Rigidbody m_self;

    //La longueur de la corde (décidée quand on tire)
    [HideInInspector]
    public float m_longueur;
	//La vitesse à laquelle on ramène la corde
	public float m_vitesseTractage;


	void Start () 
	{
		m_self = GetComponent<Rigidbody> ();
	}


	void Update () 
	{
		//Si on est accroché on ne peut pas s'éloigner
		if (/*DistTarget () > m_longueur &&*/ m_target != null) 
		{
			m_longueur -= m_vitesseTractage / 60;
			Vector3 dt = DirectTarget();
			Vector3 sp = m_self.position;
			Vector3 tp = m_target.position;
			if (pullSelf)
			{
				m_self.MovePosition(tp + dt * m_longueur);
			}
			if (pullTarget)
			{
				m_target.MovePosition(sp - dt * m_longueur);
			}
		}


		//Si on est accroché et qu'on appuie sur 3, on se rapproche
		/*if (Input.GetKey (KeyCode.Keypad3) && m_target != null) 
		{
			m_longueur -= m_vitesseTractage / 60;
		}*/


		//Si la corde est trop courte, elle se casse
		if (m_longueur < 2) 
		{
			m_target = null;
			m_noGrap = true;
			m_longueur = 2;
		}


		//Si on appuie sur 4, on lache tout
		if (Input.GetKeyDown (KeyCode.Keypad4)) 
		{
			m_target = null;
			m_noGrap = true;
		}


		//Si on appuie sur 3, on en lance un
		if (Input.GetKeyDown (KeyCode.Keypad3) && m_target == null && m_noGrap) 
		{
			GameObject projectile = 
				Instantiate (m_grappin, transform.position + transform.forward, this.gameObject.transform.rotation) as GameObject;
			projectile.GetComponent<GrappinProjectile>().origin = this;
			m_noGrap = false;
		}


		//On dessine la corde
		//if (m_target != null);
			//placerCorde (m_self.transform.position, m_target.transform.position);
	}

	//Une fonction pour donner la distance entre moi et ma target
	float DistTarget ()
	{
		if (m_self != null && m_target != null)
		{
			return (m_self.position - m_target.position).magnitude;
		}
		return 0f;
	}


	//Une fonction pour donner la direction entre moi et ma target
	Vector3 DirectTarget ()
	{
		if (m_self != null && m_target != null)
		{
			return (m_self.position - m_target.position).normalized;
		}
		return Vector3.zero;
	}


	//Une fonction pour dessiner une corde entre nous dans l'éditeur, si on est accrochés
	void OnDrawGizmos ()
	{
		if (m_self != null && m_target != null)
		{
			Gizmos.DrawLine (m_self.position, m_target.position);
		}
	}


	//Place la corde entre les deux
	void placerCorde (Vector3 début, Vector3 fin)
	{
		Vector3 pos = (début + fin) / 2;
		Vector3 orient = (fin - début).normalized;
		float leng = (fin - début).magnitude;

		GameObject corde = Instantiate (m_corde, pos, Quaternion.Euler(orient)) as GameObject; 
		corde.transform.localScale = new Vector3 (leng, 0.5f, 0.5f);
	}
}
