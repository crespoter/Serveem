using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderScript : MonoBehaviour {
    public Slider slider;
    public Image m_FillImage;
    public float remaininTime;
    public float speed = 1f;
    public Color full = Color.green;
    public Color empty = Color.red;
    tableScript tbRef;
    public GameObject myParentCustomer;
    customerManager parent;
    private int flag = 1;
    private bool paused = false;
    private bool enabled = false;

    // Use this for initialization
    void Start () {
        flag = 1;

    }

	// Update is called once per frame
	void Update () {
        if (enabled)
        {
            
            if (flag == 1)
            {
                if (myParentCustomer.tag == "Eating Customer")
                {
                    slider.maxValue = myParentCustomer.GetComponent<customerManager>().myTable.GetComponent<tableScript>().PatienceTime();
                    slider.value = slider.maxValue;
                    m_FillImage.color = Color.Lerp(empty,full,((float)slider.value)/((float)slider.maxValue));
                    flag = 0;
                }
            }
            else
            {

                if (!paused)
                {
                    slider.value -= speed * Time.deltaTime * 1000;
                    remaininTime = slider.value;
                    m_FillImage.color = Color.Lerp(empty, full, ((float)slider.value) / ((float)slider.maxValue));
                    if((remaininTime/slider.maxValue)*100<40)
                    {
                        SliderAnimator saRef = gameObject.GetComponent<SliderAnimator>();
                        saRef.enable = true;
                    }
                    else
                    {
                        SliderAnimator saRef = gameObject.GetComponent<SliderAnimator>();
                        saRef.enable = false;
                    }
                    if (slider.value <= 0)
                    {
                        myParentCustomer.GetComponent<customerManager>().LeaveTable();
                        flag = 1;
                    }
                }
            }
        }
	}

    public void toggleSlider()
    {
        if (enabled)
        {
            enabled = false;
        }
        else
            enabled = true;
    }

    public void customerLeft()
    {
        flag = 1;
    }
    public void Pause()
    {
        paused = true;
    }
    public void Resume()
    {
        paused = false;
    }
}
