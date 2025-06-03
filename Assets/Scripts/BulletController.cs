using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("Seconds before this bullet is auto-destroyed.")]
    public float maxLifetime = 5f;

    [Tooltip("How much damage this bullet deals when it hits an enemy.")]
    public int damage = 40;

    void Start()
    {
        // Destroy this bullet after maxLifetime seconds, in case it never hits anything.
        Destroy(gameObject, maxLifetime);
    }
}
