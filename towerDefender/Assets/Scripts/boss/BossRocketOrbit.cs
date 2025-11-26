using UnityEngine;

public class BossRocketOrbit : MonoBehaviour
{
    public Transform target;

    [Header("Orbit Settings")]
    public float orbitDistance = 5f; 
    public float orbitSpeed = 50f;    

    [Header("Follow Settings")]
    public float followSpeed = 3f;

    [Header("Rotation Settings")]
    public float rotateSpeed = 120f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
            target = FindFirstObjectByType<PlayerMovement>().transform;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector2 dirToPlayer = (target.position - transform.position);
        float distance = dirToPlayer.magnitude;

        Vector2 orbitDir = Vector2.Perpendicular(dirToPlayer).normalized;

        Vector2 velocity = Vector2.zero;

        if (distance > orbitDistance)
        {
            Vector2 moveDir = dirToPlayer.normalized;
            velocity += moveDir * followSpeed;
        }

        velocity += orbitDir * orbitSpeed * Time.fixedDeltaTime;

        rb.linearVelocity = velocity;

        float targetAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        targetAngle -= 90f;

        float angle = Mathf.MoveTowardsAngle(
            transform.eulerAngles.z,
            targetAngle,
            rotateSpeed * Time.fixedDeltaTime
        );

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
