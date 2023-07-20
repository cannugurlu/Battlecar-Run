using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 initialPosition;
    public GameObject[] GunPrefabs;
    private float dragOffset = 1.35f;
    Vector3 offset;
 
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
        Vector3 objeBoyutu = transform.localScale + new Vector3(0, 3, 0);
        Collider[] colliders = Physics.OverlapBox(transform.position, objeBoyutu / 2);
        bool droppedOnSlot = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Silah") && collider.gameObject != gameObject)
            {
                GameObject newObject = Instantiate(GunPrefabs[GunSpecs.gunLevel + 1], collider.gameObject.transform.position, Quaternion.identity);
                newObject.transform.parent = collider.gameObject.transform.parent;
                Destroy(gameObject);
                Destroy(collider.gameObject);
                droppedOnSlot = true;
                break;
            }
        }

        if (!droppedOnSlot)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Slot") && collider.gameObject != gameObject)
                {
                    Vector3 newPosition = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.5f, collider.gameObject.transform.position.z);
                    transform.position = newPosition;
                    droppedOnSlot = true;
                    break;
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
            Vector3 objeBoyutu = transform.localScale + new Vector3(0, 3, 0);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, objeBoyutu);
        }
    }
}
