using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class buttonManager : MonoBehaviour
{
    public GameObject Cam;
    public GameObject startButton;
    public GameObject buyButton;
    public GameObject moneyButton;
    gameManager gameManager;
    private List<GameObject> SlotList = new List<GameObject>();
    public Transform Slots;
    public Vector3 camTargetPos, camTargetRot;
    public static float time;
    public GameObject[] carSlots;
    public List<GameObject> guns = new List<GameObject>();

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

    public void buyWeaponButton()
    {
        foreach (Transform child in Slots)
        {
            SlotList.Add(child.gameObject);
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
        /* Bu fonksiyon button managera tanýþacak
        ayrýca silahlarý yukarý doðru bakacak konuma getirecek */
        Time.timeScale = 1;
        startButton.SetActive(false);
        buyButton.SetActive(false);
        moneyButton.SetActive(false);
        cameraMove(camTargetPos, camTargetRot);

        foreach (GameObject slot in carSlots)
        {
            if (slot.transform.childCount > 0)
            {
                print("akoþsg");
                guns.Add(slot.transform.GetChild(0).gameObject);
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
        foreach (GameObject slot in carSlots)
        {
            if(slot.transform.childCount != 0)
            {
                GameObject gun = slot.transform.GetChild(0).gameObject;
                slot.transform.GetChild(0).SetParent(null);
                gun.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                gun.transform.SetParent(GameObject.Find("CAR").transform);
            }
        }
    }
}
