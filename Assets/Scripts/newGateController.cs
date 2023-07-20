using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using DG.Tweening;


public class newGateController : MonoBehaviour
{
    public int effectiveNumber;
    GameObject[] childsofGate;
    UnityEngine.Color hedefRenk;
    private float gecenSure = 0f;
    private bool renkDegisti = false;
    public float sure = 1f;
    float transparency = 1f; 
    float duration = 0.5f;
    int randomVariable;
    bool isCoroutineRunning = false;
    bool isCoroutineDone = false;
    bool shouldStart=false;
    float zamanSayaci = 0.0f;
    float deltaFireRate, deltaLifeTime;
    bool isChanging = false;
    GunScript gunScript;

    private void Awake()
    {
        gunScript = Object.FindObjectOfType<GunScript>();
    }
    private void Start()
    {








        //Invoke(nameof(gateCustomizer), 0.05f);
        //// Child nesnelerini al
        //childsofGate = new GameObject[transform.childCount];
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    childsofGate[i] = transform.GetChild(i).gameObject;
        //}

        //// Hedef renk olarak (100, 214, 255) kullan
        //hedefRenk = new UnityEngine.Color(100f / 255f, 214f / 255f, 255f / 255f, 50f/255f);
    }
    void Update()
    {
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "bullet")
    //    {
    //        Destroy(other.gameObject);
    //        effectiveNumber+=10;
    //        if (randomVariable == 0) // atýþ hýzý
    //        {
    //            if (effectiveNumber < 0)
    //            {
    //                GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n" + effectiveNumber.ToString();
    //            }
    //            else if (effectiveNumber == 0)
    //            {
    //                shouldStart = true;
    //            }
    //            else
    //            {
    //                GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n+" + effectiveNumber.ToString();
    //            }
    //            if (shouldStart)
    //            {
    //                if (!isCoroutineRunning && !isCoroutineDone)
    //                {
    //                    StartCoroutine(redtogreen());
    //                }
    //            }
    //        }
    //        if (randomVariable == 1) // menzil
    //        {
    //            if (effectiveNumber < 0)
    //            {
    //                GetComponentInChildren<TextMeshPro>().text = "Menzil\n" + effectiveNumber.ToString();
    //            }
    //            else if (effectiveNumber == 0)
    //            {
    //                shouldStart=true;
    //            }
    //            else
    //            {
    //                GetComponentInChildren<TextMeshPro>().text = "Menzil\n+" + effectiveNumber.ToString();
    //            }
    //            if (shouldStart)
    //            {
    //                if (!isCoroutineRunning && !isCoroutineDone)
    //                {
    //                    StartCoroutine(redtogreen());
    //                }
    //            }
    //        }
    //    }

    //    if (other.gameObject.name == "CAR")
    //    {
    //        StartCoroutine(FadeOut(gameObject, transparency, duration));
    //        gameObject.GetComponent<BoxCollider>().enabled = false;

    //        if (randomVariable == 1 && effectiveNumber < 0) menzildegisimi(false);
    //        if (randomVariable == 1 && effectiveNumber > 0) menzildegisimi(true);
    //        if (randomVariable == 0 && effectiveNumber < 0) ratedegisimi(false);
    //        if (randomVariable == 0 && effectiveNumber > 0) ratedegisimi(true);

    //    }
    //}




    //void gateCustomizer()
    //{
    //    effectiveNumber=Random.Range(1, 3)*100;
    //    if (randomVariable == 0) // atýþ hýzý
    //    {
    //        if (gameObject.tag == "GateGood")
    //        {
    //            GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n +" + effectiveNumber.ToString();
    //        }
    //        else if (gameObject.tag == "GateBad")
    //        {
    //            effectiveNumber = -effectiveNumber;
    //            GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n " + effectiveNumber.ToString();
    //        }
    //    }
    //    else if(randomVariable == 1) // menzil
    //    {
    //        if (gameObject.tag == "GateGood")
    //        {
    //            GetComponentInChildren<TextMeshPro>().text = "Menzil\n +" + effectiveNumber.ToString();
    //        }
    //        else if (gameObject.tag == "GateBad")
    //        {
    //            effectiveNumber = -effectiveNumber;
    //            GetComponentInChildren<TextMeshPro>().text = "Menzil\n" + effectiveNumber.ToString();
    //        }
    //    }
    //}


    //void menzildegisimi(bool positive)
    //{
    //    print("menzil calisti");
    //    isChanging = true;
    //    if(positive)
    //    {
    //        gunScript.bulletLifeTime += effectiveNumber;
    //        print("menzil eklendi");
    //    }
    //    if(!positive)
    //    {
    //        gunScript.bulletLifeTime += effectiveNumber ;
    //        if (gunScript.bulletLifeTime <= gunScript.minBulletLifeTime)
    //        {
    //            gunScript.bulletLifeTime = gunScript.minBulletLifeTime;
    //        }
    //        print("menzil çýkarýldý");
    //    }
    //    isChanging =false;
    //}
    //void ratedegisimi(bool positive)
    //{
    //    print("rate calisti");
    //    isChanging = true;
    //    if (positive)
    //    {
    //        gunScript.fireRate -= effectiveNumber/10;
    //        print("Rate eklendi");
    //    }
    //    if (!positive)
    //    {
    //        gunScript.fireRate -= effectiveNumber/10;
    //        if (gunScript.fireRate <= gunScript.minFireRate)
    //        {
    //            gunScript.fireRate = gunScript.minFireRate;
    //        }
    //        print("Rate çýkarýldý");
    //    }
    //    isChanging = false;
    //}




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



        IEnumerator redtogreen()
        {
            print("calisti");
            isCoroutineRunning = true;
            // Her frame'de renkleri güncelle
            for (int i = 0; i < childsofGate.Length; i++)
            {
                print("1. for");
                Renderer renderer = childsofGate[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Renk geçiþini saðla
                    renderer.material.color = UnityEngine.Color.Lerp(renderer.material.color, hedefRenk, 1);
                    print("if");
                }
                if (childsofGate[0].GetComponent<Renderer>().material.color == hedefRenk) shouldStart = false;
            }
            isCoroutineRunning = false;
            isCoroutineDone = true;
            yield return null;
        }


    //IEnumerator redtogreen(GameObject parentObject, float duration)
    //{
    //    isCoroutineRunning = true;
    //    List<Renderer> objects = new List<Renderer>();
    //    foreach (Transform child in parentObject.transform)
    //    {
    //        if (child.name != "GateNumber")
    //        {
    //            objects.Add(child.gameObject.GetComponent<Renderer>());
    //        }
    //    }

    //    foreach (Renderer renderer in objects)
    //    {
    //        Material material = renderer.material;
    //        UnityEngine.Color color = material.color;

    //        float elapsedTime = 0f;

    //        while (elapsedTime < duration)
    //        {
    //            elapsedTime += Time.deltaTime;

    //            color.r = Mathf.Lerp(100, 0f, elapsedTime / duration);
    //            yield return null;
    //            color.g = Mathf.Lerp(214, 0f, elapsedTime / duration);
    //            yield return null;
    //            color.b = Mathf.Lerp(255, 0f, elapsedTime / duration);
    //            material.color = color;

    //            yield return null;
    //        }
    //    }
    //    isCoroutineRunning = false;
    //    isCoroutineDone = true;
    //}
}
