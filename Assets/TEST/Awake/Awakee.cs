using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awakee : MonoBehaviour {

//enable後只會呼叫一次   disable再enable之後都不會
/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
void Awake()
{
	Debug.Log("Awake");
}
}
