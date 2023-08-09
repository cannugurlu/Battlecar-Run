using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonManager : MonoBehaviour
{
    private GameObject Cam;
    public GameObject startButton;
    public GameObject buyButton;
    gameManager gameManager;
    private List<GameObject> SlotList = new List<GameObject>();
    public Transform startSlots;
    public Transform carSlots;
    public Vector3 camTargetPos, camTargetRot;
    public static float time;
    public List<GameObject> guns = new List<GameObject>();
    public TextMeshProUGUI moneyValue;
    public TextMeshProUGUI buyValue;
    public static float buyMoney = 50.0f;

    public static buttonManager instance;

    void Awake() 
    {
        instance = this;

        Cam = GameObject.Find("Main Camera");
        time = 0.7f;
        camTargetPos = new Vector3(0, 6.55f, -3.93f);
        camTargetRot = new Vector3(35, 0, 0);
        gameManager = FindObjectOfType<gameManager>();
    }
    private void Update()
    {
        moneyValue.text = playersScript.money.ToString();
        buyValue.text = buyMoney.ToString();
    }

    public void buyWeaponButton()
    {
        if (playersScript.money >= buyMoney)
        {
            foreach (Transform slot in startSlots)
            {
                SlotList.Add(slot.gameObject);
            }
            foreach (GameObject slot in SlotList)
            {
                SlotScript slotScript = slot.GetComponent<SlotScript>();
                if (!slotScript.IsFilled())
                {
                    Vector3 newPosition = new Vector3(slot.transform.position.x, slot.transform.position.y + 0.5f, slot.transform.position.z);
                    Quaternion newRotation = gameManager.GunPrefabs[0].transform.rotation;
                    Instantiate(gameManager.GunPrefabs[0], newPosition, newRotation, slot.transform);
                    break;
                }
            }
            playersScript.money -= buyMoney;
            buyMoney += 10;
        }
    }

    public void startLevelButton()
    {
        Time.timeScale = 1;
        startButton.SetActive(false);
        buyButton.SetActive(false);
        cameraMove(camTargetPos, camTargetRot);
        carSlots.SetParent(GameObject.Find("CAR").transform);

        //Araba silahları listesi set edilir.
        foreach (Transform slot in carSlots)
        {
            if (slot.gameObject.TryGetComponent(out Renderer slotRenderer))
            {
                Material material = slotRenderer.material;
                material.DOFade(0f, 0.5f);
            }

            if (slot.childCount > 0)
            {
                guns.Add(slot.GetChild(0).gameObject);
            }
        }

        //Start Slotları dolu ise silahları pasife alır.
        foreach (Transform slot in startSlots)
        {
            if(slot.childCount > 0)
            {
                GameObject gun = slot.GetChild(0).gameObject;
                gun.SetActive(false);
            }
        }

        gunAngleModifier();
    }




    private void cameraMove(Vector3 targetPos, Vector3 targetRot)
    {
        Cam.transform.DOMove(targetPos, time);
        Cam.transform.DORotate(targetRot, time);
    }

    private void gunAngleModifier()
    {
        foreach (Transform slot in carSlots)
        {
            if(slot.childCount != 0)
            {
                Transform gun = slot.GetChild(0);
                gun.DORotate(new Vector3(0, 90, 0), 0.5f);
            }
        }
    }
}
