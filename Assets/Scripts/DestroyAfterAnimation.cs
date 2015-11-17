/*
using UnityEngine;
using System.Collections;

public class DestroyAfterAnimation : MonoBehaviour 
{
	Animation anim;
	bool alwaysDead = false;
	
	void Start () 
	{
		anim = GetComponent<Animation>();
		StartCoroutine(Pause());
	}


	void Update()
	{
		if (alwaysDead == true) this.gameObject.SetActive(false);
	}

	IEnumerator Pause()
	{
		yield return new WaitForSeconds(4f);
		alwaysDead = true;
	}
}
*/