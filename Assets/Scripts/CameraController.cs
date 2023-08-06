using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class cameraController : MonoBehaviour
{
    public static cameraController instace;

    public Transform playerTransform;
    private Vector3 offset,camsRot;
    Vector3 target;
    private float rotateTime=0.3f;
    public float cameraSpeed;
    public float cameraSmoothSpeed;
    private float camRotX = 35;
    public static bool cameraFollow = false;
    bool isCamRotated = false;


    private void Awake()
    {
        instace = this;
    }
    void Start()
    {
        StartCoroutine(EnableCameraFollow());
        
    }

    private IEnumerator EnableCameraFollow()
    {
        yield return new WaitForSeconds(buttonManager.time);

        cameraFollow = true;

        offset = transform.position - playerTransform.position;
    }


    void LateUpdate()
    {
        if (cameraFollow)
        {
            if (!playersScript.minigame)
            {
                Vector3 targetPosition = offset + playerTransform.position;
                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
                newPosition = Vector3.Lerp(transform.position, newPosition, cameraSpeed);

                transform.position = newPosition;




                if(transform.eulerAngles.x != 35)
                {
                    if (!isCamRotated)
                    {
                        Vector3 targetRot = transform.eulerAngles;
                        targetRot.x = 35;
                        transform.DORotate(targetRot, rotateTime);
                        isCamRotated = true;
                        Invoke(nameof(boolenChanger), rotateTime + 0.01f);
                    }
                }

            }
            else
            {
                Vector3 targetPosition = offset + playerTransform.position;

                targetPosition.y += 2.5f;
                targetPosition.z += 3.5f;

                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
                newPosition = Vector3.Lerp(transform.position, newPosition, cameraSpeed);

                transform.position = newPosition;


                if (transform.eulerAngles.x != 60)
                {
                    if (!isCamRotated)
                    {
                        Vector3 targetRot = transform.eulerAngles;
                        targetRot.x = 60;
                        transform.DORotate(targetRot, rotateTime);
                        isCamRotated = true;
                        Invoke(nameof(boolenChanger), rotateTime + 0.01f);
                    }
                }
            }
        }
    }

    void boolenChanger()
    {
        isCamRotated = !isCamRotated;
    }
}