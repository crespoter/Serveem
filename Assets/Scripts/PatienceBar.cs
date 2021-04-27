using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceBar : MonoBehaviour {

    // Use this for initialization

    public Transform target;

	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        var wantedPos = Camera.main.WorldToViewportPoint(target.position);
        transform.position = wantedPos;
    }

    /*var target : Transform;
 
    /*function Update()
    {
        var wantedPos = Camera.main.WorldToViewportPoint(target.position);
        transform.position = wantedPos;
    }*/
}
