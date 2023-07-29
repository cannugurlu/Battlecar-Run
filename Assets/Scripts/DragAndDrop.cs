using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    GunScript gunScript;
    gameManager gameManager;
    private Vector3 initialPosition;
    private float dragOffset = 1.35f;
    Vector3 offset;

    void Start() 
    {
        gameManager = FindObjectOfType<gameManager>();
        gunScript = GetComponent<GunScript>();
    }
 
    void OnMouseDown()
    {
        initialPosition = transform.position;
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }
 
    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset + new Vector3(0, dragOffset, 0);
    }
 
    void OnMouseUp()
    {
        Vector3 objeBoyutu = transform.localScale + new Vector3(0, 4, 0);
        Collider[] colliders = Physics.OverlapBox(transform.position, objeBoyutu / 2);
        bool droppedOnSlot = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Silah") && collider.gameObject != gameObject)
            {
                GunScript otherGunScript = collider.gameObject.GetComponent<GunScript>();

                if (otherGunScript != null && otherGunScript.gunLevel == gunScript.gunLevel)
                {
                    Quaternion newRotation = gameManager.GunPrefabs[gunScript.gunLevel + 1].transform.rotation;
                    GameObject newObject = Instantiate(gameManager.GunPrefabs[gunScript.gunLevel + 1], collider.gameObject.transform.position, newRotation);
                    newObject.transform.parent = collider.gameObject.transform.parent;
                    Destroy(gameObject);
                    Destroy(collider.gameObject);
                    droppedOnSlot = true;
                    break;
                }
            }
        }

        if (!droppedOnSlot)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Slot") && collider.gameObject != gameObject)
                {
                    SlotScript slotScript = collider.gameObject.GetComponent<SlotScript>();

                    if (slotScript != null && !slotScript.is_filled)
                    {
                        Vector3 newPosition = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.5f, collider.gameObject.transform.position.z);
                        transform.position = newPosition;
                        transform.parent = collider.gameObject.transform;
                        
                        droppedOnSlot = true;
                    }
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

    private void OnDrawGizmos()
    {
        if (gameObject != null)
        {
            Vector3 objeBoyutu = transform.localScale + new Vector3(0, 4, 0);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, objeBoyutu);
        }
    }
}
