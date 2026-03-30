using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 1000f;

    [SerializeField]
    private float bulletLifetime = 5f;

    [SerializeField]
    private Rigidbody2D ribo;

    void Start()
    {
        ribo = GetComponent<Rigidbody2D>();

        Destroy(gameObject, bulletLifetime);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    public void setDirection(bool direction)
    {
        if (!direction)
        {
            bulletSpeed = -bulletSpeed;
        }

        if (ribo != null)
        {
            ribo.AddForce(Vector2.right * bulletSpeed);
        }
    }
}
