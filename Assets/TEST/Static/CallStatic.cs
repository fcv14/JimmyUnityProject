using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallStatic : MonoBehaviour {

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		Debug.Log("public_STATIC before assign"+Static_1.public_static_num);
		int get_public_StaticNum = Static_1.public_static_num;
		Debug.Log("public_STATIC after assign"+get_public_StaticNum);

		Static_1 static_1_Class = new Static_1();        //new?
		int get_public_Num= static_1_Class.public_num;
		Debug.Log("Not static"+get_public_Num);
	}
	}
