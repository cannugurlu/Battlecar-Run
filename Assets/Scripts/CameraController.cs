using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class cameraController : MonoBehaviour
{
    public Vector3 camTargetPos,camTargetRot;
    public float time;
    void Start()
    {
        time = 1.2f;
        camTargetPos=new Vector3(0,1,-10);
        camTargetRot=new Vector3(0,0,-0);
    }

    public void startLevelBtn() 
    {
        cameraMove(camTargetPos,camTargetRot);
    }

    private void cameraMove(Vector3 targetPos, Vector3 targetRot)
    {
        this.gameObject.transform.DOMove(targetPos,time);
        this.gameObject.transform.DORotate(targetRot,time);
    }
}
