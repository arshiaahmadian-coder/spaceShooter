using UnityEngine;

public class TurretAiming : MonoBehaviour
{
    public Joystick aimJoystick;
    public Transform firePoint;
    public float rotationSpeed = 15f;
    public float deadZone = 0.05f;

    public bool canShoot;

    private Vector2 aimDirection = Vector2.up;

    public Vector2 AimDirection => aimDirection;

    void Start()
    {
        aimJoystick = GameObject.Find("attack joystick").GetComponent<Joystick>();
    }

    private void Update()
    {
        if (aimJoystick == null) return;

        Vector2 input = aimJoystick.Direction;

        if (input.sqrMagnitude > deadZone * deadZone)
        {
            canShoot = true;
            aimDirection = input.normalized;
            float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRot = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 1f - Mathf.Exp(-rotationSpeed * Time.deltaTime));
        }
        else canShoot = false;
    }
}
