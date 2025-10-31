using UnityEngine;

public class SparkleProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 10f;
    public float lifetime = 10f;
    public bool useGravity = false;

    private Rigidbody rb;
    private ParticleSystem ps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = useGravity;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        if (ps != null)
            ps.Play();

        Destroy(gameObject, lifetime);
    }

    public void Launch(Vector3 direction)
    {
        if (rb != null)
        {
            rb.velocity = direction.normalized * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Optionally handle impact effect or particle burst here
        if (ps != null)
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        Destroy(gameObject, 0.1f);
    }
}
