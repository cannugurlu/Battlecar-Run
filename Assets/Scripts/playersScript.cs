using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playersScript : MonoBehaviour
{
    Rigidbody rb;
    public float velocity;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float ClampX = 2.5f;
    private Touch touch;
    public static bool minigame = false;
    public static playersScript instance;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 1) * velocity * Time.deltaTime;
    }
    
    void Update()
    {
        if (cameraController.cameraFollow)
        {
            if (!minigame)
            {
                float x = transform.position.x;
                float _x = Mathf.Clamp(x, -ClampX, ClampX);
                transform.position = new Vector3(_x, transform.position.y, transform.position.z);

                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speed, transform.position.y, transform.position.z);
                    }
                }
            }
            else
            {
                //CLAMP
                foreach (GameObject obj in buttonManager.instance.guns)
                {
                    //float rotY = obj.transform.rotation.y;
                    //float _rotY = Mathf.Clamp(rotY, 45, 135);
                    //obj.transform.eulerAngles = new Vector3(obj.transform.rotation.x, _rotY, obj.transform.rotation.z);

                    Vector3 rot = obj.transform.eulerAngles;
                    rot.x = ClampAngle(rot.x, 45, 135);
                }

                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        Debug.Log(buttonManager.instance.guns.Count);
                        //ROTATE
                        foreach (GameObject obj in buttonManager.instance.guns)
                        {
                            if (touch.deltaPosition.x != 0)
                            {
                                obj.transform.Rotate(Vector3.up * touch.deltaPosition.x * Time.deltaTime *rotationSpeed);
                            }
                        }
                    }
                }
            }
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
