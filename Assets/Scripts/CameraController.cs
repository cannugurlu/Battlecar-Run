using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraController : MonoBehaviour
{
    public static cameraController instace;

    public Transform playerTransform;
    private Vector3 offset;
    Vector3 target;
    public float cameraSpeed;
    public float cameraSmoothSpeed;
    public static bool cameraFollow = false;

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
            Vector3 targetPosition = offset + playerTransform.position;
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
            newPosition = Vector3.Lerp(newPosition, newPosition, cameraSpeed);

            transform.position = newPosition;
        }
    }
}