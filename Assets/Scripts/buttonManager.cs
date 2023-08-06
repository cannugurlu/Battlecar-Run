using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class buttonManager : MonoBehaviour
{
    private GameObject Cam;
    public GameObject startButton;
    public GameObject buyButton;
    public GameObject moneyButton;
    gameManager gameManager;
    private List<GameObject> SlotList = new List<GameObject>();
    public Transform startSlots;
    public Transform carSlots;
    public Vector3 camTargetPos, camTargetRot;
    public static float time;
    public List<GameObject> guns = new List<GameObject>();
    public TextMeshProUGUI moneyValue;

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
    }

    public void buyWeaponButton()
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
    }

    public void startLevelButton()
    {
        Time.timeScale = 1;
        startButton.SetActive(false);
        buyButton.SetActive(false);
        moneyButton.SetActive(false);
        cameraMove(camTargetPos, camTargetRot);

        //Araba silahları listesi set edilir.
        foreach (Transform slot in carSlots)
        {
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
                gun.SetParent(null);
                gun.DORotate(new Vector3(0, 90, 0), 0.5f);
                gun.SetParent(GameObject.Find("CAR").transform);
            }
        }
    }
}
