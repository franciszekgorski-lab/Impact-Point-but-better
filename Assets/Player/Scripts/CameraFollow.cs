using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [Range(0f, 1f)]
    public float mouseInfluence = 0.3f; // jak bardzo mysz przesuwa kamerę
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        // Przelicz ręcznie bez ScreenToWorldPoint
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 offset = (screenPos - screenCenter) / screenCenter;

        Vector2 mouseWorld = (Vector2)player.position + new Vector2(offset.x * halfWidth, offset.y * halfHeight);

        Vector2 targetPos = Vector2.Lerp(
            (Vector2)player.position,
            mouseWorld,
            mouseInfluence
        );

        Vector3 desired = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}