using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroutine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//coroutine_function();         // nothing happen here
		StartCoroutine(coroutine_function());      //this would work properly	
		Debug.Log("CoroutineTEST-------C");
	}
	

	IEnumerator coroutine_function(){

		Debug.Log("CoroutineTEST-------A");

		yield return new WaitForSeconds(3);	

		Debug.Log("CoroutineTEST-------B");
	}
}
