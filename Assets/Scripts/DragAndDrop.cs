using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public int gunLevel;
    GameManager gameManager;
    private Vector3 initialPosition;
    private float dragOffset = 1.35f;

    void Start() 
    {
        gameManager = FindObjectOfType<GameManager>();
    }
 
    void OnMouseDown()
    {
        initialPosition = transform.position;
        transform.GetComponent<Collider>().enabled = false;
    }
    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + new Vector3(0, dragOffset, 0);
    }
 
    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition1() - Camera.main.transform.position;

        bool droppedOnSlot = false;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.CompareTag("Silah"))
            {
                DragAndDrop otherDragScript = hitInfo.transform.GetComponent<DragAndDrop>();

                if (otherDragScript != null && otherDragScript.gunLevel == gunLevel)
                {
                    if (gunLevel + 1 < gameManager.GunPrefabs.Length)
                    {
                        Quaternion newRotation = gameManager.GunPrefabs[gunLevel + 1].transform.rotation;
                        GameObject newObject = Instantiate(gameManager.GunPrefabs[gunLevel + 1], hitInfo.transform.position, newRotation);
                        newObject.transform.parent = hitInfo.transform.parent;
                        Destroy(gameObject);
                        Destroy(hitInfo.transform.gameObject);
                        droppedOnSlot = true;
                    }
                    else
                    {
                        Debug.Log("Maksimum Levele Ulastin");
                    }
                }
            }
            else if (hitInfo.transform.CompareTag("Slot"))
            {
                SlotScript slotScript = hitInfo.transform.gameObject.GetComponent<SlotScript>();

                if (slotScript != null && !slotScript.is_filled)
                {
                    Vector3 newPosition = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + 0.5f, hitInfo.transform.position.z);
                    transform.position = newPosition;
                    transform.parent = hitInfo.transform;
                    
                    droppedOnSlot = true;
                }
            }
        }

        if (!droppedOnSlot)
        {
            transform.position = initialPosition;
        }

        initialPosition = transform.position;
        transform.GetComponent<Collider>().enabled = true;
    }
 
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition; 
        mouseScreenPos.x = Input.mousePosition.x;
        mouseScreenPos.y = Input.mousePosition.y;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return new Vector3 (WorldPosition.x, 1f , WorldPosition.z);
    }

    Vector3 MouseWorldPosition1()
    {
        var mouseScreenPos = Input.mousePosition; 
        mouseScreenPos.x = Input.mousePosition.x;
        mouseScreenPos.y = Input.mousePosition.y;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }

}
