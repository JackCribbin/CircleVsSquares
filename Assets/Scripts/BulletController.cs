using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("Seconds before this bullet is auto-destroyed.")]
    public float maxLifetime = 5f;

    void Start()
    {
        // Destroy this GameObject after maxLifetime seconds
        Destroy(gameObject, maxLifetime);
    }
}
