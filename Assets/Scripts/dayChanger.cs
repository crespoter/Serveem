using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayChanger : MonoBehaviour
{

    public float dayTime;
    private float yRotation;
    private float timer;
    private Light light;
    void Start()
    {
        yRotation = 0;
        timer = 0;
        light = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += (Time.deltaTime * 1000);
        light.intensity = 1-(timer / dayTime);
    }
}
