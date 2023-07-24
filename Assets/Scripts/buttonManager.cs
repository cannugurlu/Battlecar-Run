using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    gameManager gameManager;
    private List<GameObject> SlotList = new List<GameObject>();
    public Transform Slots;

    void Start() 
    {
        gameManager = FindObjectOfType<gameManager>();
    }

    public void buyWeaponButton()
    {
        foreach (Transform child in Slots)
        {
            SlotList.Add(child.gameObject);
        }
        foreach (GameObject slot in SlotList)
        {
            SlotScript slotScript = slot.GetComponent<SlotScript>();
            if (!slotScript.IsFilled())
            {
                Vector3 newPosition = new Vector3(slot.transform.position.x, slot.transform.position.y + 0.5f, slot.transform.position.z);
                Quaternion newRotation = gameManager.GunPrefabs[0].transform.rotation;
                Instantiate(gameManager.GunPrefabs[0], newPosition, newRotation, slot.transform);
                break;
            }
        }
    }
}
