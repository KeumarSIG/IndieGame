using UnityEngine;
using System.Collections;

public class TimeReset : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
    {
        Time.timeScale = 1f; // basic time scale
        Time.fixedDeltaTime = 0.02f; // basic fixedDeltaTime
    }
}