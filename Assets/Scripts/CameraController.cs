using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject buyButton;
    public GameObject moneyButton;
    public Vector3 camTargetPos,camTargetRot;
    public float time;
    public Transform playerTransform;
    private Vector3 offset;
    Vector3 target;
    public float cameraSpeed;
    public float cameraSmoothSpeed;
    public static bool cameraFollow = false;

    void Start()
    {
        time = 0.7f;
        camTargetPos = new Vector3(0, 6.55f, -3.93f);
        camTargetRot = new Vector3(35, 0, 0);

        StartCoroutine(EnableCameraFollow());
    }

    private IEnumerator EnableCameraFollow()
    {
        yield return new WaitForSeconds(time);

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

    public void startLevelButton()
    {
        Time.timeScale = 1;
        startButton.SetActive(false);
        buyButton.SetActive(false);
        moneyButton.SetActive(false);
        cameraMove(camTargetPos, camTargetRot);
    }

    private void cameraMove(Vector3 targetPos, Vector3 targetRot)
    {
        this.gameObject.transform.DOMove(targetPos, time);
        this.gameObject.transform.DORotate(targetRot, time);
    }
}