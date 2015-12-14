using UnityEngine;
using System.Collections;

public class SelectLevelTemporary : MonoBehaviour 
{
	void Update()
	{
		if (Input.GetButtonDown ("LB_0")) // niveau sans rien
		{
			Application.LoadLevel(1);
		}

		if (Input.GetButtonDown ("RB_0")) // niveau sans rien
		{
			Application.LoadLevel(2);
		}
	}
}