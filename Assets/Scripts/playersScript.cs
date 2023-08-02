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
       // buttonManager.instance.guns
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
                transform.DOMoveX(0,1);
                //CLAMP

                //float x = transform.position.x;
                //float _x = Mathf.Clamp(x, -ClampX, ClampX);
                //transform.position = new Vector3(_x, transform.position.y, transform.position.z);

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
}
