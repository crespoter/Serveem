using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimator : MonoBehaviour {

    // Use this for initialization

    public float maxDist = 1;
    public float speed = 0.5f;
    bool isMoving = true;
    public Vector3 moveVector = Vector3.up;
    int init = 0;
    Transform tempTransform;
	void Start () {
        tempTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = tempTransform.position + moveVector * (maxDist * Mathf.Sin(Time.timeSinceLevelLoad * speed));
	}
}
