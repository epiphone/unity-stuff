using UnityEngine;
using System.Collections;

public class HealthPickupScript : MonoBehaviour
{
    public int healthRestored = 2;
    public AudioClip healingSound;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var healthScript = other.transform.GetComponentInChildren<HealthScript>();
        if (healthScript != null && !healthScript.isEnemy)
        {
            healthScript.Heal(healthRestored);
            AudioSource.PlayClipAtPoint(healingSound, transform.position);
            Destroy(gameObject);
        }
    }
}
