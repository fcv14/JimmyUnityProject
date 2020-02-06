using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DelegateScript : MonoBehaviour {
	delegate void MyDelegate(int num);
	MyDelegate myDelegate;


	void Start()
	{
		myDelegate = PrintNum;
		myDelegate(50);	
		myDelegate = PrintDouble;
		myDelegate(50);
		//myDelegate = PrintFloat;
		myDelegate(50);
	}


	void PrintNum(int num){
		print ("printNum"+num);
	} 
	void PrintDouble(int num){
		print ("printDouble"+num*2);
	}   
	
	void PrintFloat(float num){
		print("printFloat"+num);
	}
}
