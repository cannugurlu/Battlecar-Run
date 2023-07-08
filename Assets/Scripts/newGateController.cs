using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGateController : MonoBehaviour
{
    public static newGateController instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Time.timeScale>0)
        {
            gateCustomizer();
        }
    }

    void gateCustomizer()
    {
        int goodorbad = Random.RandomRange(0, 2);
        if (goodorbad == 0) //gate'in pozitif özelliði olacak
        {

        }
        else //gatein negatif özelliði olacak
        {

        }
    }
}
