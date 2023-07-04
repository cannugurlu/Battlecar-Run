using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    public float cameraSpeed;

    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    void Update()
    {
        int hatCount = FindAnyObjectByType<GameManager>().childCount;
    }

    void LateUpdate()
    {
        int hatCount = FindObjectOfType<GameManager>().childCount;

        Vector3 targetPosition = offset + playerTransform.position;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);

        float zAdjustment = Mathf.Floor(hatCount / 3f);
        float yAdjustment = Mathf.Floor(hatCount / 6f);

        Vector3 adjustmentVector = new Vector3(0f, yAdjustment, -zAdjustment);
        newPosition = Vector3.Lerp(newPosition, newPosition + adjustmentVector, cameraSpeed);

        transform.position = newPosition;
    }

}
