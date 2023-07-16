using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    GameObject car, platform;
    float XClamp,clampedValue;
    void Awake()
    {
        Time.timeScale = 0.0f;
        car = GameObject.Find("CAR");
        platform=GameObject.Find("platform");
        XClamp=((platform.transform.localScale.x-car.transform.localScale.x) /2)-0.3f;
    }
    void Start()
    {
    }
    void Update()
    {
        clampedValue=car.transform.position.x;
        clampedValue = Mathf.Clamp(clampedValue, -XClamp, XClamp);
        car.transform.position= new Vector3(clampedValue,car.transform.position.y,car.transform.position.z);
    }
}
