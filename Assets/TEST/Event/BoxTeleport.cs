using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTeleport : MonoBehaviour {
/// <summary>
/// This function is called when the object becomes enabled and active.
/// </summary>
void OnEnable()
{
	eventManager.OnClicked+=Teleport;
}
/// <summary>
/// This function is called when the behaviour becomes disabled or inactive.
/// </summary>
void OnDisable()
{
	eventManager.OnClicked-=Teleport;
}

void Teleport(){
	Vector3 pos = transform.position;
	pos.y = Random.Range(0.3f,1.0f);
	transform.position = pos;
}

}
