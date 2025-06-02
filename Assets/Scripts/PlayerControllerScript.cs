using UnityEngine;
using UnityEngine.InputSystem;  // ← Required for New Input System

public class PlayerController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    void Update()
    {
        // New Input System: check if the left mouse button was just pressed
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
    }
}
