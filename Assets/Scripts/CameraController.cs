using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.Serialization;

public class cameraController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject buyButton;
    public GameObject moneyButton;
    public Vector3 camTargetPos,camTargetRot;
    public float time;
    void Start()
    {
        time = 1.2f;
        camTargetPos=new Vector3(0,6.55f,-3.93f);
        camTargetRot=new Vector3(35,0,0);
    }
    
    public void startLevelButton()
    {
        Time.timeScale = 1.0f;
        startButton.SetActive(false);
        buyButton.SetActive(false);
        moneyButton.SetActive(false);
        cameraMove(camTargetPos, camTargetRot);
        Invoke(nameof(makeChild), time);
    }

    private void cameraMove(Vector3 targetPos, Vector3 targetRot)
    {
        this.gameObject.transform.DOMove(targetPos,time);
        this.gameObject.transform.DORotate(targetRot,time);
    }

    void makeChild()
    {
        gameObject.transform.SetParent(GameObject.Find("car").transform);
    }


}
