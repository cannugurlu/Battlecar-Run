using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gateController : MonoBehaviour
{
    [SerializeField] GameObject gateGood;
    [SerializeField] GameObject gateBad;
    public int gateNumber;
    private bool isPos;
    public bool isRange;
    private bool isGateDeleted=false;
    private bool isCarChanged = false;
    public Material blueMaterial;
    GunScript[] gunScript;


    void Start()
    {
        gunScript = Object.FindObjectsOfType<GunScript>();
        gateGood = gameObject.transform.Find("GateGood").gameObject;
        gateBad = gameObject.transform.Find("GateBad").gameObject;
    }
    void Update()
    {
        if(!isGateDeleted)
        {
            gateCustomizer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CAR")
        {
            upgradeCar();

            if (isPos)
            {
                Destroy(gameObject.transform.Find("GateGood").gameObject);
                isGateDeleted = true;
            }
            else
            {
                Destroy(gameObject.transform.Find("GateBad").gameObject);
                isGateDeleted = true;
            }
        }
        if(other.gameObject.tag == "bullet")
        {
            foreach (GunScript g in gunScript)
            {
                gateNumber += g.damagetogate;
                
            }

            Destroy(other.gameObject);
        }
    }

    void gateCustomizer()
    {
        if (gateNumber >= 0) isPos = true;
        else if (gateNumber < 0) isPos = false;

        if (isPos) // toplama i�areti
        {
            gateBad.SetActive(false);
            gateGood.SetActive(true);
            if (isRange)
            {
                gateGood.GetComponentInChildren<TextMeshPro>().text = "Range\n+" + gateNumber.ToString();
            }
            else
            {
                gateGood.GetComponentInChildren<TextMeshPro>().text = "Attack Speed\n+" + gateNumber.ToString();
            }
        }
        else // ��karma i�areti
        {
            gateGood.SetActive(false);
            gateBad.SetActive(true);
            if (isRange)
            {
                gateBad.GetComponentInChildren<TextMeshPro>().text = "Range\n" + gateNumber.ToString();
            }
            else
            {
                gateBad.GetComponentInChildren<TextMeshPro>().text = "Attack Speed\n" + gateNumber.ToString();
            }
        }   
    }

    void upgradeCar()
    {
        if(isRange)
        {
            foreach (GunScript g in gunScript)
            {
                g.bulletLifeTime += gateNumber / 200.0f;
            }
        }
        else
        {
            foreach (GunScript g in gunScript)
            {
                g.fireRate -= gateNumber / 200.0f;
            }
        }
    }
}
