using UnityEngine;
using System.Collections;

public class WpnBoostPickupScript : MonoBehaviour
{
    public float newShootingRate = 0.2f;
    public float durationSeconds = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        var weaponScript = other.transform.GetComponentInChildren<WeaponScript>();
        if (weaponScript != null)
        {
            weaponScript.GiveTemporaryBonus(newShootingRate, durationSeconds);
            Destroy(gameObject);
        }
    }
}
