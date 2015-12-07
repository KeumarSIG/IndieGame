using UnityEngine;
using System.Collections;

//Cette classe sert à tirer des grappins et à les gérer. A mettre sur les voitures.
public class Grappin : MonoBehaviour {

	[Tooltip("Le prefab de corde qu'on va utiliser")]
	public GameObject m_corde;
	GameObject m_cordeInstance;

	//Deux bool pour dire si je me tire et si je tire ma cible. Je vais les retirer pour faire marcher ca autrement
	public bool pullTarget;
	//True si ce joueur n'a aucun grappin présent sur la scène (nécessaire pour tirer)
	public bool m_noGrap;

    [HideInInspector]
	//Ce qu'on a accroché
	//On se sert de cette variable pour déterminer si on a accroché quelque chose. Si m_target == null, on n'a rien.
	public Rigidbody m_target;
	//Le tireur
    Rigidbody m_self;

    //La longueur de la corde (décidée quand le grappin s'accroche)
    //[HideInInspector]
    public float m_longueur;
	[Tooltip("La vitesse à laquelle on ramène la corde")]
	public float m_vitesseTractage;
    float m_hauteurTerrain;


	void Start () 
	{
		m_self = GetComponent<Rigidbody> ();
		m_cordeInstance = Instantiate (m_corde, transform.position, Quaternion.identity) as GameObject;
	}


	void FixedUpdate () 
	{ 
		//Si on est accroché on ne peut pas s'éloigner
		if (m_target != null) 
		{
			m_longueur -= m_vitesseTractage / 60;
			Vector3 dt = DirectTarget();
			Vector3 sp = m_self.position;
			Vector3 tp = m_target.position;
			m_self.MovePosition(tp + dt * m_longueur);
			if (pullTarget)
			{
				m_target.MovePosition(sp - dt * m_longueur);
			}
		}


		//Si la corde est trop courte, elle se casse
		if (m_longueur < 2 && m_target != null) 
		{
			m_target.AddForce(new Vector3 (0,150,0));
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

		//Placer la corde entre soi et la cible
		if (m_self != null && m_target != null)
        {
			m_cordeInstance.SetActive (true);
			placerCorde (m_self.transform.position, m_target.transform.position);
		} 
		else if (m_self != null && m_target == null && m_noGrap == false) 
		{
			m_cordeInstance.SetActive (true);
		}
		else
		{
			m_cordeInstance.SetActive(false);
		}

		if (!m_noGrap || m_target != null)
			transform.position = new Vector3 (transform.position.x, m_hauteurTerrain, transform.position.z);
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
        else
        {
            return Vector3.zero;
        }
	}


	//Une fonction pour dessiner une corde entre nous dans le jeu, si on est accrochés
	public void placerCorde (Vector3 début, Vector3 fin)
	{
		Vector3 pos = (début + fin) / 2;
		Quaternion orient = Quaternion.LookRotation(fin - début);
		float leng = (fin - début).magnitude;

		m_cordeInstance.transform.position = pos;
		m_cordeInstance.transform.rotation = orient * Quaternion.Euler (0, 90, 0);
		m_cordeInstance.transform.localScale = new Vector3 (leng, 0.5f, 0.5f);
	}


	//Une fonction pour lancer le grappin
	public void lancerGrappin (GameObject grappin)
	{
        m_hauteurTerrain = transform.position.y;
		if (m_target == null && /*m_noGrap &&*/ transform.position.y >= 0) 
		{
			GameObject projectile = 
				Instantiate (grappin, transform.position + transform.forward, this.gameObject.transform.rotation) as GameObject;
			projectile.GetComponent<GrappinProjectile>().origin = this;
			m_noGrap = false;
		}
	}
}