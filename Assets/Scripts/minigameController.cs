using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class minigameController : MonoBehaviour
{
    private int numberoftargets = 0;
    private Vector3 targetPos;
    void Start()
    {
        targetPos = new Vector3(-1.65f,0.01f,transform.position.z);
        targetPos.z += 10;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "target")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            numberoftargets++;

            if (numberoftargets % 2 == 1)
            {
                other.gameObject.transform.DOMove(targetPos, 0.25f * numberoftargets);

                other.gameObject.transform.DORotate(new Vector3(0,145,0), 0.25f * numberoftargets);

                other.gameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f * numberoftargets);

            }
            else if (numberoftargets % 2 == 0)
            {
                targetPos.x = -targetPos.x;
                other.gameObject.transform.DOMove(targetPos, 0.25f * numberoftargets);
                targetPos.x = -targetPos.x;

                other.gameObject.transform.DORotate(new Vector3(0,-145,0), 0.25f * numberoftargets);

                other.gameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f*numberoftargets);

                targetPos.z += 5.0f;
            }
        }

        if (other.gameObject.tag == "car")
        {
            playersScript.minigame = true;
        }
    }

}
