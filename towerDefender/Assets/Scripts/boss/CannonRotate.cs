using UnityEngine;

public class CannonRotate : MonoBehaviour
{
    [Header("Player & Rotation")]
    public Transform player;
    public float rotateSpeed = 120f;
    public float maxAngle = 60f;

    [Range(-180, 180)]
    public float angleOffset = -90f;

    public float fireRate = 2f;

    private Transform reference;
    private float initialAngle;
    private float nextFireTime;
    [SerializeField] GameObject rocketBullet;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int bulletDamage;
    [SerializeField] Transform firePos;
    [SerializeField] AudioClip shootClip;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        reference = transform.parent;
        while (reference != null && reference.parent != null)
            reference = reference.parent;

        initialAngle = NormalizeAngle(transform.localEulerAngles.z);

        nextFireTime = Time.time;
    }

    void Update()
    {
        if (player == null || reference == null) return;

        Vector3 worldDir = (player.position - transform.position);
        if (worldDir.sqrMagnitude < 0.01f) return;

        Vector3 localDir = reference.InverseTransformDirection(worldDir);
        Vector2 localDir2D = new Vector2(localDir.x, localDir.y).normalized;

        float targetAngle = Vector2.SignedAngle(Vector2.right, localDir2D) + angleOffset;
        targetAngle = NormalizeAngle(targetAngle);

        float relativeAngle = Mathf.DeltaAngle(initialAngle, targetAngle);

        bool inRange = Mathf.Abs(relativeAngle) <= maxAngle;
        if (inRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / fireRate);
        }

        float clampedRelative = Mathf.Clamp(relativeAngle, -maxAngle, maxAngle);

        float finalTargetAngle = initialAngle + clampedRelative;

        float currentAngle = NormalizeAngle(transform.localEulerAngles.z);

        float newAngle = Mathf.MoveTowardsAngle(currentAngle, finalTargetAngle, rotateSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(0, 0, newAngle);
    }

    public void Shoot()
    {
        GameObject b = Instantiate(rocketBullet, firePos.position, firePos.rotation);
        b.GetComponent<EnemyBullet>().speed = bulletSpeed;
        b.GetComponent<EnemyBullet>().damage = bulletDamage;

        AudioManager.instance.PlayLowSFX(shootClip);
    }

    private float NormalizeAngle(float angle)
    {
        angle = (angle + 180f) % 360f - 180f;
        return angle;
    }
}