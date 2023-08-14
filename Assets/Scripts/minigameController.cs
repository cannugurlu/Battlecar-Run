using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class minigameController : MonoBehaviour
{
    private int numberoftargets = 0;
    private Vector3 targetPos;
    public List<GameObject> readyTargets = new List<GameObject>();
    private bool controllerBoolen = false;

    public static minigameController instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        targetPos = new Vector3(-2.25f,0.01f,transform.position.z);
        targetPos.z += 15;
    }
    void Update()
    {
        if (controllerBoolen)
        {
            foreach(GameObject go in readyTargets)
            {
                go.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                numberoftargets++;

                if (numberoftargets % 2 == 1)
                {
                    go.gameObject.transform.DOMove(targetPos, 0.25f * numberoftargets);

                    go.gameObject.transform.DORotate(new Vector3(0, 145, 0), 0.25f * numberoftargets);

                    go.gameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f * numberoftargets);

                }
                else if (numberoftargets % 2 == 0)
                {
                    targetPos.x = -targetPos.x;
                    go.gameObject.transform.DOMove(targetPos, 0.25f * numberoftargets);
                    targetPos.x = -targetPos.x;

                    go.gameObject.transform.DORotate(new Vector3(0, -145, 0), 0.25f * numberoftargets);

                    go.gameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.25f * numberoftargets);

                    targetPos.z += 5.0f;
                }
            }



            controllerBoolen = !controllerBoolen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "target")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            readyTargets.Add(other.gameObject);
        }

        if (other.gameObject.tag == "car")
        {
            playersScript.minigame = true;
            controllerBoolen = true;
        }
    }

}
