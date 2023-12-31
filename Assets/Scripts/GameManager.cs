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
    public TextMeshProUGUI scoreValue;
    public GameObject LoseEndPanel;
    public GameObject WinEndPanel;
    public static int level = 1;
    public GameObject[] levelPrefabs;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    
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
                FindObjectOfType<buttonManager>().RestartGame();
            }
        }
    }

    public void Ended()
    {
        playersScript.gameFinished = true;

        if (playersScript.isGameWin)
        {
            WinEndPanel.SetActive(true);
            scoreValue.text = playersScript.money.ToString();
            //leveli artir
        }
        else
        {
            LoseEndPanel.SetActive(true);
            playersScript.money = buttonManager.instance.initialMoney;
        }

        FindObjectOfType<buttonManager>().moneyValue.gameObject.SetActive(false);

    }
}
