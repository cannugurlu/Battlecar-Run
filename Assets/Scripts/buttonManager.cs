using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    private List<GameObject> SlotList = new List<GameObject>();
    public Transform Slots;
    public GameObject level1GunPrefab;

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
                Instantiate(level1GunPrefab, newPosition, Quaternion.identity, slot.transform);
                break;
            }
        }
    }

}
