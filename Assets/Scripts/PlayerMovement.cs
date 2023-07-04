using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed;
    public float horizontalSpeed;
    private float mouseMove;
    private float borderX = 2.95f;
    private Vector3 newPosition;
    public Rigidbody rb;
    void Update()
    {
        PlayerHorizontalMove();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
    }

    void PlayerHorizontalMove()
    {
        mouseMove = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = transform.position + new Vector3(mouseMove * horizontalSpeed * Time.deltaTime, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, -borderX, borderX);
            transform.position = newPosition;
        }
    }
}
