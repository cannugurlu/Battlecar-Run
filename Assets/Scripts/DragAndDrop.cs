using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 initialPosition;
    public GameObject level2GunPrefab;
    private float yOffset = 1.01f;

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
                Collider[] colliders = Physics.OverlapSphere(selectedObject.transform.position, 0.1f);
                bool droppedOnSilah = false;

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Silah") && collider.gameObject != selectedObject)
                    {
                        GameObject newObject = Instantiate(level2GunPrefab, collider.gameObject.transform.position, Quaternion.identity);
                        newObject.transform.parent = collider.gameObject.transform.parent;
                        Destroy(selectedObject);
                        Destroy(collider.gameObject);
                        droppedOnSilah = true;
                        break;
                    }
                }

                if (!droppedOnSilah)
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

            Collider[] colliders = Physics.OverlapSphere(selectedObject.transform.position, 0.1f);

            if (colliders.Length == 0)
            {
                yOffset = 1.01f;
            }
            else
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("car") && collider.gameObject != selectedObject)
                    {
                        yOffset += 1f;
                        break;
                    }
                }
            }

            selectedObject.transform.position = new Vector3(worldPosition.x, yOffset, worldPosition.z);
        }
    }

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
