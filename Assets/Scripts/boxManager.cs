using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Linq;
using Microsoft.Win32.SafeHandles;

public class boxManager : MonoBehaviour
{
    GunScript gunScript;
    Vector3 initialPos,hedefPos,hedefRot,carPos;
    private GameObject box, hedef,money;
    private TextMeshPro healthbar;
    public float canBari = 100.0f;
    public float income = 10.0f;
    private bool isTextureChanged=false;
    private bool isTargetMoved = false;
    public float velocity = 200.0f;
    public float rotateVelocity = 45.0f;
    public float zAxis;
    public static bool isAnimCompleted=false;
    private void Awake()
    {
        healthbar = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
        money = gameObject.transform.GetChild(1).gameObject;
        box = this.gameObject;
        hedef=gameObject.transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "bullet")
        {
            canBari -= other.gameObject.GetComponent<bulletManager>().bulletDamagetoBox;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        carPos = GameObject.Find("CAR").transform.position;

        money.transform.Rotate(0, rotateVelocity * Time.deltaTime, 0);

        healthbar.text = canBari.ToString();

        if (canBari < 50)
        {
            if (!isTextureChanged)
            {
                Invoke(nameof(boxTextureChange),0.02f);
            }
        }

        if (canBari <= 0)
        {
            if (!isTargetMoved)
            {
                if (GameObject.Find("FinishLine").transform.position.z < gameObject.transform.position.z)
                {
                    Destroy(box);
                    earnMoneyAnim();
                }
                else
                {
                    earnMoneyAnim();
                    bantagit();
                }
                isTargetMoved = true;
            }
        }
    }

    void earnMoneyAnim()
    {
        GameObject moneyObj = money;
        money.transform.SetParent(null);

        rotateVelocity *= 3;
        moneyObj.transform.DOScale(0.25f, 0.4f);
        moneyObj.transform.DOMoveX(carPos.x, 0.65f).OnComplete(() =>
        {
            playersScript.money += income;
            Destroy(moneyObj);
        });
        moneyObj.transform.DOMoveZ(carPos.z+1.5f, 0.65f);
        moneyObj.transform.DOMoveY(carPos.y + 2.0f, 0.3f).OnComplete(() =>
        moneyObj.transform.DOMoveY(carPos.y, 0.3f));
    }
    void bantagit()
    {
        print("banta git"); 
        GameObject targetObj = hedef;
        hedef.transform.SetParent(null);
        Destroy(box);
        initialPos = hedef.transform.position;
        hedefPos = new Vector3(GameObject.FindWithTag("bant").transform.position.x+0.4f,GameObject.FindWithTag("bant").transform.position.y+0.2f,gameObject.transform.position.z);
        hedefRot = new Vector3(-180, 250, -100);
        targetObj.transform.DOMove(hedefPos, 1);
        targetObj.GetComponent<Rigidbody>().isKinematic = false;
        targetObj.transform.DORotate(hedefRot, 1).OnComplete(()=>
        targetObj.GetComponent<Rigidbody>().velocity=new Vector3(0,0,1)*3000*Time.deltaTime);
        isAnimCompleted = true;
    }

    void boxTextureChange()
    {
        //gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        isTextureChanged = true;
    }
}
