using UnityEngine;
using System.Collections;

/// <summary>
/// Player controls.
/// </summary>
public class PlayerScript : MonoBehaviour
{

    public float moveSpeed = 3;
    public float turnSpeed = 7;
    public float weaponTurnSpeed = 12;
    public float weaponMaxRotation = 30;

    private Transform weapon;
    private WeaponScript weaponScript;
    private MoveScript moveScript;

    void Start()
    {
        weapon = transform.Find("weapon1");
        weaponScript = weapon.GetComponent<WeaponScript>();
        moveScript = transform.GetComponent<MoveScript>();
    }

    void Update()
    {
        // Rotate and move player:
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        moveScript.direction = new Vector2(moveSpeed * inputX, moveSpeed * inputY);

        var currentPos = transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var moveDirection = mousePos - currentPos;
        moveDirection.z = 0;
        moveDirection.Normalize();

        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);

        // Rotate weapon:
        var wpnDirection = mousePos - weapon.position;
        wpnDirection.z = 0;
        wpnDirection.Normalize();

        float wpnTargetAngle = Mathf.Atan2(wpnDirection.y, wpnDirection.x) * Mathf.Rad2Deg;
        if (wpnTargetAngle < 0)
        {
            wpnTargetAngle = 360 + wpnTargetAngle;
        }

        wpnTargetAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, wpnTargetAngle,
            weaponMaxRotation);

        weapon.rotation = Quaternion.Slerp(weapon.rotation,
            Quaternion.Euler(0, 0, wpnTargetAngle), weaponTurnSpeed * Time.deltaTime);

        // Shoot:
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            if (weaponScript && weaponScript.CanAttack)
            {
                weaponScript.Attack(false);
                // TODO sound fx
            }
        }  
    }
}
