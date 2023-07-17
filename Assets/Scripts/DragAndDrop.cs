using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 initialPosition;
    public GameObject level1GunPrefab;
    public GameObject level2GunPrefab;
    public float yOffset = 2.21f;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();
                
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Silah"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    initialPosition = selectedObject.transform.position;
                    Cursor.visible = false;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null)
            {
                Vector3 objeBoyutu = selectedObject.transform.localScale + new Vector3(0, 3, 0);
                Collider[] colliders = Physics.OverlapBox(selectedObject.transform.position, objeBoyutu / 2);
                bool droppedOnSlot = false;

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Silah") && collider.gameObject != selectedObject)
                    {
                        GameObject newObject = Instantiate(level2GunPrefab, collider.gameObject.transform.position, Quaternion.identity);
                        newObject.transform.parent = collider.gameObject.transform.parent;
                        Destroy(selectedObject);
                        Destroy(collider.gameObject);
                        droppedOnSlot = true;
                        break;
                    }
                }

                if (!droppedOnSlot)
                {
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Slot") && collider.gameObject != selectedObject)
                        {
                            Destroy(selectedObject);
                            Vector3 newPosition = new Vector3(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y + 0.5f, collider.gameObject.transform.position.z);
                            GameObject newObject = Instantiate(level1GunPrefab, newPosition, Quaternion.identity, collider.gameObject.transform);
                            droppedOnSlot = true;
                            break;
                        }
                    }
                }

                if (!droppedOnSlot)
                {
                    selectedObject.transform.position = initialPosition;
                }

                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if (Input.GetMouseButton(0) && selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, yOffset, worldPosition.z);
        }
    }

    /* private void OnDrawGizmos()
    {
        if (selectedObject != null)
        {
            Vector3 objeBoyutu = selectedObject.transform.localScale + new Vector3(0, 3, 0);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(selectedObject.transform.position, objeBoyutu);
        }
    } */

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
