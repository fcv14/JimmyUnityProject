using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : MonoBehaviour {

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
void Start()
{
	Debug.Log("Start");	
}
/// <summary>
/// This function is called when the object becomes enabled and active.
/// </summary>
void OnEnable()
{
	Debug.Log("Enable");
}
/// <summary>
/// This function is called when the behaviour becomes disabled or inactive.
/// </summary>
void OnDisable()
{
	Debug.Log("Disable");
}
}
