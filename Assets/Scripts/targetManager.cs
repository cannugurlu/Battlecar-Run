using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class targetManager : MonoBehaviour
{
    GameObject parentObj;
    public GameObject moneyPrefab;
    Vector3 carPos;
    float healthofTarget = 100f;
    public float swayBackVelocity = 250.0f;
    public float animTimer = 0.65f;
    public float income = 15.0f;
    bool isAnimStarted = false;
    void Start()
    {
        parentObj = gameObject.transform.parent.gameObject;
    }
    void Update()
    {
        carPos = GameObject.Find("CAR").transform.position;
        gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = healthofTarget.ToString();
        if (boxManager.isAnimCompleted) gameObject.transform.GetChild(0).gameObject.SetActive(true);

        if (healthofTarget <= 0.0f)
        {
            healthofTarget = 0.0f;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);

            //DT ANIM -> SONUNA SETACT FALSE
            if (!isAnimStarted)
            {
                moneyGeneratorAndAnimator();
                dotweenSeq();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            healthofTarget -= other.gameObject.GetComponent<bulletManager>().bulletDamagetoBox;
            Destroy(other.gameObject);
        }
    }

    private void dotweenSeq()
    {

        isAnimStarted = true;

        Vector3 initialPos = gameObject.transform.position;
        Vector3 targetRot = new Vector3(-90, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        gameObject.transform.DORotate(targetRot, animTimer);

        gameObject.transform.DOMoveY(initialPos.y + 2.0f, animTimer / 2).OnComplete(() =>
        gameObject.transform.DOMoveY(initialPos.y + 0.5f, animTimer / 2));

        gameObject.GetComponent<Rigidbody>().velocity = -transform.forward * swayBackVelocity * Time.deltaTime;
        Invoke(nameof(velocityCleaner), animTimer/4f);

        Invoke(nameof(DoFadeChildren), animTimer);
    }

    private void velocityCleaner()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void DoFadeChildren()
    {
        foreach (Transform transform in gameObject.GetComponentInChildren<Transform>())
        {
            if (transform.gameObject.TryGetComponent(out Renderer targetRenderer))
            {
                Material material = targetRenderer.material;
                material.DOFade(0f, 0.5f);
            }  
        }
    }

    private void moneyGeneratorAndAnimator()
    {
        isAnimStarted = true;

        GameObject moneyObj = Instantiate(moneyPrefab, gameObject.transform.position, Quaternion.identity);
        moneyObj.transform.DOScale(0.25f, 0.4f);
        moneyObj.transform.DOMoveX(carPos.x, 0.65f).OnComplete(() =>
        {
            playersScript.money += income;
            Destroy(moneyObj);
        }); ;
        moneyObj.transform.DOMoveZ(carPos.z+1.5f, 0.65f);
        moneyObj.transform.DOMoveY(carPos.y + 2.0f, 0.3f).OnComplete(() =>
        moneyObj.transform.DOMoveY(carPos.y, 0.3f));
    }
}
