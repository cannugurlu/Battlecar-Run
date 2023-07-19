using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class hedefTahtasiScript : MonoBehaviour
{
    Vector3 initialPos,hedefPos,hedefRot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CAR") bantagit();
    }


    void bantagit()
    {
        initialPos = gameObject.transform.position;
        hedefPos = new Vector3(GameObject.FindWithTag("bant").transform.position.x+0.4f,GameObject.FindWithTag("bant").transform.position.y+0.2f,gameObject.transform.position.z);
        hedefRot = new Vector3(-180, 250, -100);
        gameObject.transform.DOMove(hedefPos, 1);
        gameObject.transform.DORotate(hedefRot, 1).OnComplete(()=>
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0,0,1)*1000*Time.deltaTime);
    }
}
