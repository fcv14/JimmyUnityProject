using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRandomColor : MonoBehaviour {
/// <summary>
/// This function is called when the object becomes enabled and active.
/// </summary>
void OnEnable()
{
	eventManager.OnClicked+=randomColor;
}
/// <summary>
/// This function is called when the behaviour becomes disabled or inactive.
/// </summary>
void OnDisable()
{
	eventManager.OnClicked-=randomColor;
}
void randomColor(){
	Color col = new Color(Random.value,Random.value,Random.value);
	GetComponent<Renderer>().material.color = col;
}
}
