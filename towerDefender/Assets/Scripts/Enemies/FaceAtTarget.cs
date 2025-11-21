using UnityEngine;

public class FaceAtTarget : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
