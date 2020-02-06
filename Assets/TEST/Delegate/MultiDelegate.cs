using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDelegate : MonoBehaviour {

	delegate void myMultiDelegate();
	myMultiDelegate mymultidelegate ;
    Vector3 
    void Start()
    {
	    mymultidelegate += TurnRed;
	    mymultidelegate += PowerUp;

	    if (mymultidelegate != null)
	    {
		    mymultidelegate();
	    }
    }
    
	void PowerUp(){
		print("Orb is powering up!");
	}

	void TurnRed()
	{
		GetComponent<Renderer>().material.color = Color.red;
	}
}
