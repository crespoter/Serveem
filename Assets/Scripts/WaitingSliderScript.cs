using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingSliderScript : MonoBehaviour {

    public Slider slider;
    public float remaininTime;
    public float speed = 1f;
    public GameObject myParentCustomer;
    customerManager parent;
    private int flag = 1;
    private bool enabled = true;
    public Vector2 waitingPatienceRange;


    // Use this for initialization
    void Start () {
        flag = 1;

        slider.maxValue = Random.Range(waitingPatienceRange.x, waitingPatienceRange.y);
        slider.value = slider.maxValue;
        parent = myParentCustomer.GetComponent<customerManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (enabled)
        {

            if (flag == 1)
            {
                if (myParentCustomer.tag == "WaitingCustomer")
                {
                    enabled = true;
                    slider.value = slider.maxValue;
                    flag = 0;
                }
            }
            else
            {

                slider.value -= speed * Time.deltaTime * 1000;
                remaininTime = slider.value;
                if (slider.value <= 0)
                {
                    parent.tip += (int)remaininTime;
                }
            }
        }
    }
}
