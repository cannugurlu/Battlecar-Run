using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using DG.Tweening;


public class newGateController : MonoBehaviour
{
    [SerializeField] int effectiveNumber;
    public UnityEngine.Color baslangicRenk;
    public UnityEngine.Color hedefRenk;
    private float gecenSure = 0f;
    private bool renkDegisti = false;
    public float sure = 1f;
    float transparency = 1f; 
    float duration = 0.5f;
    int randomVariable;
    bool isCoroutineRunning = false;
    bool isCoroutineDone = false;

    private void Awake()
    {
    }
    private void Start()
    {
        Invoke(nameof(gateCustomizer), 0.05f);
    }
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
            effectiveNumber+=10;
            if (randomVariable == 0) // atýþ hýzý
            {
                if (effectiveNumber < 0)
                {
                    GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n" + effectiveNumber.ToString();
                }
                else if (effectiveNumber == 0)
                {
                    if(!isCoroutineRunning && !isCoroutineDone)
                    {
                        StartCoroutine(redtogreen(gameObject, duration));
                    }
                }
                else
                {
                    GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n+" + effectiveNumber.ToString();
                }
            }
            else if (randomVariable == 1) // menzil
            {
                if (effectiveNumber < 0)
                {
                    GetComponentInChildren<TextMeshPro>().text = "Menzil\n" + effectiveNumber.ToString();
                }
                else if (effectiveNumber == 0)
                {
                    if (!isCoroutineRunning && !isCoroutineDone)
                    {
                        StartCoroutine(redtogreen(gameObject, duration));
                    }
                }
                else
                {
                    GetComponentInChildren<TextMeshPro>().text = "Menzil\n+" + effectiveNumber.ToString();
                }
            }
        }

        if (other.gameObject.name == "car")
        {
            StartCoroutine(FadeOut(gameObject, transparency, duration));
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void gateCustomizer()
    {
        effectiveNumber=Random.Range(1, 3)*100;
        randomVariable = Random.Range(0, 2);
        if (randomVariable == 0) // atýþ hýzý
        {
            if (gameObject.tag == "GateGood")
            {
                GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n +" + effectiveNumber.ToString();
            }
            else if (gameObject.tag == "GateBad")
            {
                effectiveNumber = -effectiveNumber;
                GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n " + effectiveNumber.ToString();
            }
        }
        else if(randomVariable == 1) // menzil
        {
            if (gameObject.tag == "GateGood")
            {
                GetComponentInChildren<TextMeshPro>().text = "Menzil\n +" + effectiveNumber.ToString();
            }
            else if (gameObject.tag == "GateBad")
            {
                effectiveNumber = -effectiveNumber;
                GetComponentInChildren<TextMeshPro>().text = "Menzil\n" + effectiveNumber.ToString();
            }
        }
    }







    IEnumerator FadeOut(GameObject parentObject, float transparency, float duration)
    {
        List<Renderer> objects=new List<Renderer>();
        foreach(Transform child in parentObject.transform)
        {
            if (child.name != "GateNumber")
            {
                objects.Add(child.gameObject.GetComponent<Renderer>());
            }
        }

        foreach (Renderer renderer in objects)
        {
            Material material = renderer.material;
            UnityEngine.Color color = material.color;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // Saydaml??? azaltarak rengi g?ncelle
                color.a = Mathf.Lerp(transparency, 0f, elapsedTime / duration);
                material.color = color;

                yield return null;
            }

            // Nesneyi tamamen g?r?nmez hale getir
            color.a = 0f;
            material.color = color;
        }
    }


    IEnumerator redtogreen(GameObject parentObject, float duration)
    {
        isCoroutineRunning = true;
        List<Renderer> objects = new List<Renderer>();
        foreach (Transform child in parentObject.transform)
        {
            if (child.name != "GateNumber")
            {
                objects.Add(child.gameObject.GetComponent<Renderer>());
            }
        }

        foreach (Renderer renderer in objects)
        {
            Material material = renderer.material;
            UnityEngine.Color color = material.color;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                color.r = Mathf.Lerp(100, 0f, elapsedTime / duration);
                color.g = Mathf.Lerp(214, 0f, elapsedTime / duration);
                color.b = Mathf.Lerp(255, 0f, elapsedTime / duration);
                material.color = color;

                yield return null;
            }
        }
        isCoroutineRunning = false;
        isCoroutineDone = true;
    }
}
