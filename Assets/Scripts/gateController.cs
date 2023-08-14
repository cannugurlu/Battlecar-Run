using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class gateController : MonoBehaviour
{
    [SerializeField] GameObject gateGood;
    [SerializeField] GameObject gateBad;
    public int gateNumber;
    private bool isPos;
    public bool isRange;
    private bool isGateDeleted = false;
    //private bool isCarChanged = false;
    public Material blueMaterial;
    public List<GunScript> gunScriptsList= new List<GunScript>();
    public Transform carSlots;
    Vector3 initialScale;
    int controllerNumber = 0;

    public static gateController instance;


    void Start()
    {
        instance = this;

        initialScale = transform.localScale;
        gateGood = gameObject.transform.Find("GateGood").gameObject;
        gateBad = gameObject.transform.Find("GateBad").gameObject;
    }
    void Update()
    {
        if(!isGateDeleted)
        {
            gateCustomizer();
        }

        if(Time.timeScale>0 && controllerNumber==0)
        {
            carSlots = GameObject.Find("CarSlots").transform;
            foreach (GunScript script in carSlots.GetComponentsInChildren<GunScript>())
            {
                gunScriptsList.Add(script);
            }
            controllerNumber++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CAR")
        {
            upgradeCar();

            if (isPos)
            {
                Destroy(gameObject);
                isGateDeleted = true;
            }
            else
            {
                Destroy(gameObject);
                isGateDeleted = true;
            }
        }
        if(other.gameObject.tag == "bullet")
        {
            gateScaler();
            gateNumber += other.gameObject.GetComponent<bulletManager>().bulletDamagetoGate;
            Destroy(other.gameObject);
        }
    }

    void gateScaler()
    {
        gameObject.transform.localScale = initialScale;
        gameObject.transform.DOScale(initialScale * 1.1f, 0.12f).OnComplete(() =>
        gameObject.transform.DOScale(initialScale, 0.12f));
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
            foreach (GunScript g in gunScriptsList)
            {
                g.bulletLifeTime += gateNumber / 200.0f;
            }
        }
        else
        {
            foreach (GunScript g in gunScriptsList)
            {
                g.fireRate -= gateNumber / 200.0f;
            }
        }
    }
}
