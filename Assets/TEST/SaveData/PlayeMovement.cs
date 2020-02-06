using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMovement : MonoBehaviour {

    Vector3 pos;
    
    // Update is called once per frame
    void Update () {
        pos.x = Random.Range(-5,5);
        pos.y = Random.Range(-5, 5);
        pos.z = Random.Range(-5, 5);
        this.transform.position = Vector3.Lerp(this.transform.position,pos,0.05f);
        Debug.Log(this.transform.position);
	}
}
