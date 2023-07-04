using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    GameObject lastHat;
    public Transform stackParentTransform;
    public GameObject hatPrefab;
    private int addHat;
    private int totalHats;
    private float hatHeight = 0.3f;
    private List<GameObject> hatsToDestroy = new List<GameObject>();
    private List<GameObject> hatsToThrow = new List<GameObject>();

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Hat")
        {
            Transform colliderTransform = other.transform;
            Rigidbody colliderRb = colliderTransform.GetComponent<Rigidbody>();

            colliderRb.isKinematic = true;
            other.collider.enabled = false;

            Vector3 newColliderPos = stackParentTransform.position + Vector3.up * (stackParentTransform.childCount + 1) * (hatHeight);
            colliderTransform.position = newColliderPos;
            colliderTransform.GetComponent<HatSwing>().enabled = true;
            if (stackParentTransform.childCount > 0)
            {
                colliderTransform.GetComponent<HatSwing>().lowerObjectTransform = stackParentTransform.GetChild(stackParentTransform.childCount - 1);
            }
            colliderTransform.parent = stackParentTransform;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate")
        {
            Destroy(other.gameObject);
            addHat = other.GetComponent<Gate>().gateNumber;

            if (addHat > 0)
            {
                for (int i = 0; i < addHat; i++)
                {
                    Vector3 spawnPos = stackParentTransform.position + Vector3.up * (stackParentTransform.childCount +1) * hatHeight;
                    Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
                    GameObject newHat = Instantiate(hatPrefab, spawnPos, rotation);

                    newHat.GetComponent<Rigidbody>().isKinematic = true;
                    newHat.GetComponent<Collider>().enabled = false;
                    newHat.GetComponent<HatSwing>().enabled = true;

                    newHat.transform.SetParent(stackParentTransform);
                }
            }
            else
            {
                int numberDestroy = Mathf.Abs(addHat);

                foreach (Transform child in stackParentTransform)
                {
                    hatsToDestroy.Add(child.gameObject);
                }

                hatsToDestroy.Sort((x, y) => y.transform.position.y.CompareTo(x.transform.position.y));

                for (int i = 0; i < numberDestroy && i < hatsToDestroy.Count; i++)
                {
                    Destroy(hatsToDestroy[i]);
                }
            }
        }
        if (other.tag == "Obstacle")
        {
            totalHats = stackParentTransform.childCount;

            if (totalHats > 5)
            {
                foreach (Transform child in stackParentTransform)
                {
                    hatsToThrow.Add(child.gameObject);
                }

                hatsToThrow.Sort((x, y) => y.transform.position.y.CompareTo(x.transform.position.y));

                int numHatsToThrow = hatsToThrow.Count - 5;

                for (int i = 0; i < numHatsToThrow; i++)
                {
                    GameObject hatToThrow = hatsToThrow[i];
                    hatToThrow.transform.parent = null;
                    Rigidbody hatRb = hatToThrow.GetComponent<Rigidbody>();

                    if (hatRb != null)
                    {
                        hatRb.isKinematic = false;
                        hatToThrow.GetComponent<Collider>().enabled = true;
                        hatToThrow.GetComponent<HatSwing>().enabled = false;
                        Vector3 throwDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                        float throwForce = 5f;
                        hatRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
                    }
                }
            }

        }
        if (other.tag == "FinishLine")
        {
            FindAnyObjectByType<GameManager>().EndGame();
        }
    }
}
