using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float SprintMultiplier = 2f;
    public float Acceleration = 10f; // im wyżej tym mniej płynnie

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 smoothMovement; // <-- nowe
    Vector2 MousePos;

    void Update()
    {
        movement.x = Keyboard.current.dKey.isPressed ? 1f : Keyboard.current.aKey.isPressed ? -1f : 0f;
        movement.y = Keyboard.current.wKey.isPressed ? 1f : Keyboard.current.sKey.isPressed ? -1f : 0f;

        // Płynne przejście między kierunkami
        smoothMovement = Vector2.Lerp(smoothMovement, movement, Acceleration * Time.deltaTime);

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        MousePos = new Vector2(worldPos.x, worldPos.y);
    }

    void FixedUpdate()
    {
        float speed = MoveSpeed;
        if (Keyboard.current.leftShiftKey.isPressed)
            speed *= SprintMultiplier;

        rb.MovePosition(rb.position + smoothMovement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = MousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}