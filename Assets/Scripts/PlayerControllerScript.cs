using UnityEngine;
using UnityEngine.InputSystem;  // ← Required for New Input System
using UnityEngine.UI;          // ← For the XP Slider

public class PlayerController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    [Header("XP Settings")]
    public Slider xpSlider;      // drag your UI Slider here
    public int maxXP = 100;      // XP needed to “fill” bar
    private int currentXP = 0;   // starts at 0

    void Start()
    {
        // ensure slider is set up
        if (xpSlider != null)
        {
            xpSlider.minValue = 0;
            xpSlider.maxValue = maxXP;
            xpSlider.value    = currentXP;
        }
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            ShootBullet();
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

    /// <summary>
    /// Call this whenever the player should gain XP (e.g. from an enemy death).
    /// </summary>
    public void GainXP(int amount)
    {
        currentXP += amount;
        currentXP = Mathf.Clamp(currentXP, 0, maxXP);

        if (xpSlider != null)
            xpSlider.value = currentXP;

        // (Optional) check for level‑up:
        // if (currentXP >= maxXP) { LevelUp(); }
    }
}
