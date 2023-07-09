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

                    //renk kýrmýzýdan maviye dönecek
                    //if (!renkDegisti)
                    //{
                    //    gecenSure += Time.deltaTime;
                    //    float yuzdeTamamlandi = gecenSure / sure;
                    //    GetComponentInChildren<Renderer>().material.color = UnityEngine.Color.Lerp(baslangicRenk, hedefRenk, sure);
                    //    GetComponentInChildren<TextMeshPro>().text = "Atýþ Hýzý\n+" + effectiveNumber.ToString();

                    //    if (gecenSure >= sure)
                    //    {
                    //        renkDegisti = true;
                    //    }
                    //}
                    //redtoblue(gameObject);
                    StartCoroutine(redtogreen(gameObject, 1.0f));
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
                    //renk kýrmýzýdan maviye dönecek
                    //if (!renkDegisti)
                    //{
                    //    gecenSure += Time.deltaTime;
                    //    float yuzdeTamamlandi = gecenSure / sure;
                    //    GetComponentInChildren<Renderer>().material.color = UnityEngine.Color.Lerp(baslangicRenk, hedefRenk, yuzdeTamamlandi);
                    //    GetComponentInChildren<TextMeshPro>().text = "Menzil\n+" + effectiveNumber.ToString();

                    //    if (gecenSure >= sure)
                    //    {
                    //        renkDegisti = true;
                    //    }
                    //}
                    //redtoblue(gameObject);
                    StartCoroutine(redtogreen(gameObject, 1.0f));
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
        Renderer[] childRenderers = parentObject.GetComponentsInChildren<Renderer>();
        List<Renderer> renderers = new List<Renderer>(childRenderers);
        string textsname = "GateNumber";
        for (int i = renderers.Count - 1; i >= 0; i--)
        {
            if (renderers[i].name == textsname)
            {
                renderers.RemoveAt(i);
                break; // Ýlk bulunan öðeyi çýkardýktan sonra döngüden çýk
            }
        }

        foreach (Renderer renderer in childRenderers)
        {
            Material material = renderer.material;
            UnityEngine.Color color = material.color;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // Saydamlýðý azaltarak rengi güncelle
                color.a = Mathf.Lerp(transparency, 0f, elapsedTime / duration);
                material.color = color;

                yield return null;
            }

            // Nesneyi tamamen görünmez hale getir
            color.a = 0f;
            material.color = color;
        }
    }

    IEnumerator redtogreen(GameObject self, float duration)
    {
        Renderer[] childRenderers = self.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            Material material = renderer.material;
            UnityEngine.Color color = material.color;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                color.r = Mathf.Lerp(color.r, 100, elapsedTime / duration);
                color.g = Mathf.Lerp(color.g, 214, elapsedTime / duration);
                color.b = Mathf.Lerp(color.b, 255, elapsedTime / duration);
                material.color = color;

                yield return null;
            }
        }
    }
}
