using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform target;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
       // transform.position = target.position;
       
        Vector3 newPosition = new Vector3(target.position.x + 7, target.position.y, target.position.z);
        transform.position = newPosition;

    }
}
