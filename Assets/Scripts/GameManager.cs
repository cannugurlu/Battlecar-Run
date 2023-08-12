using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    GameObject car, platform;
    float XClamp,clampedValue;
    public GameObject[] GunPrefabs;
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private GameObject endPanel;
    public static int level = 1;
    void Awake()
    {
        Time.timeScale = 0.0f;
        car = GameObject.Find("CAR");
        platform=GameObject.Find("platform");
        XClamp=((platform.transform.localScale.x-car.transform.localScale.x) /2)-0.3f;
    }
    void Start()
    {
    }
    
    void Update()
    {
        clampedValue=car.transform.position.x;
        clampedValue = Mathf.Clamp(clampedValue, -XClamp, XClamp);
        car.transform.position= new Vector3(clampedValue,car.transform.position.y,car.transform.position.z);

        if (playersScript.gameFinished)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                car.transform.position = FindObjectOfType<playersScript>().initialPosition;
                RestartGame();
            }
        }
    }

    public void Ended()
    {
        playersScript.gameFinished = true;
        endPanel.SetActive(true);
        scoreValue.text = playersScript.money.ToString();
        FindObjectOfType<buttonManager>().moneyValue.gameObject.SetActive(false);

        if (playersScript.minigameFinished)
        {
            level++;
        }
    }

    public void RestartGame()
    {
        playersScript.gameFinished = false;
        endPanel.SetActive(false);
        buttonManager buttonManager = FindObjectOfType<buttonManager>(); 
        buttonManager.moneyValue.gameObject.SetActive(true);
        buttonManager.startButton.SetActive(true);
        buttonManager.buyButton.SetActive(true);
    }
}
