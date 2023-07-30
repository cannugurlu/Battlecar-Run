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
    Vector3 initialPos,hedefPos,hedefRot;
    private GameObject box, hedef;
    float canBari = 100.0f;
    private bool isTextureChanged=false;
    private bool isTargetMoved = false;
    public float velocity = 200.0f;
    public float zAxis;
    private void Awake()
    {
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
        gameObject.GetComponentInChildren<TextMeshPro>().text = canBari.ToString();

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
                bantagit();
                isTargetMoved = true;
            }
        }
    }

    void bantagit()
    {
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
    }

    void boxTextureChange()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        isTextureChanged = true;
    }
}
