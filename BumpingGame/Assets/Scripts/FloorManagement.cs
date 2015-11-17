using UnityEngine;
using System.Collections;

public class FloorManagement : MonoBehaviour 
{	
	void Update () 
	{
		if (this.transform.position.y < 1)
		{
			GetComponent<Animator>().enabled = false;
		}
	}
}
