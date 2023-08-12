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
    public static bool minigameFinished = false;
    public static bool gameFinished = false;
    public static playersScript instance;
    public static float money = 50.0f;
    public Vector3 initialPosition;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 1) * velocity * Time.deltaTime;
        initialPosition = transform.position;
    }
    
    void Update()
    {
        if (cameraController.cameraFollow)
        {
            if (gameFinished)
                return;
                
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

                if (minigameFinished)
                {
                    foreach (GameObject obj in buttonManager.instance.guns)
                    {
                        obj.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                    }
                }
            }
            else
            {
                //CLAMP
                foreach (GameObject obj in buttonManager.instance.guns)
                {
                    float rotY = obj.transform.eulerAngles.y;
                    float _rotY = Mathf.Clamp(rotY, 45, 135);
                    obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, _rotY, obj.transform.eulerAngles.z);

                }

                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        //ROTATE
                        foreach (GameObject obj in buttonManager.instance.guns)
                        {
                            if (touch.deltaPosition.x != 0)
                            {
                                obj.transform.Rotate(Vector3.up * touch.deltaPosition.x * Time.deltaTime * rotationSpeed);
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "money")
        {
            money += 50;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag =="target" || other.gameObject.tag == "box")
        {
            Time.timeScale = 0.0f;
            FindObjectOfType<GameManager>().Ended();
        }
    }
}
