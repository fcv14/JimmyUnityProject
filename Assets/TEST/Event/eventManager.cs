using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class eventManager : MonoBehaviour {
public delegate void ClickAction();
public static event ClickAction OnClicked;
/// <summary>
/// OnGUI is called for rendering and handling GUI events.
/// This function can be called multiple times per frame (one call per event).
/// </summary>
void OnGUI()
{
	if (GUI.Button(new Rect(Screen.width/2-50,5,100,30),"Click")){
		if (OnClicked != null)
		{
			OnClicked();
		}
	}	
}
}
