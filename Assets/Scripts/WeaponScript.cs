using UnityEngine;
using System.Collections;

/// <summary>
/// Spawn shots, manage cooldown.
/// </summary>
public class WeaponScript : MonoBehaviour
{
    /// <summary>
    /// Projectile for shooting.
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Cooldown in seconds.
    /// </summary>
    public float shootingRate = 0.25f;

    public AudioClip weaponSound;
    public AudioClip bonusStartSound;
    public AudioClip bonusEndSound;

    private float shootCooldown;
    private ParticleSystem shellParticle;

    // Enable cumulative weapon boosts
    private float baseShootingRate;
    private bool isBoosted = false;
    private float additionalBoostSeconds;


    // Use this for initialization
    void Start()
    {
        baseShootingRate = shootingRate;
        shootCooldown = 0;
        shellParticle = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        shootCooldown = Mathf.Max(shootCooldown - Time.deltaTime, 0);
    }

    /// <summary>
    /// Instantiate a new shot if weapon has cooled down.
    /// </summary>
    /// <param name="isEnemy"></param>
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            AudioSource.PlayClipAtPoint(weaponSound, transform.position);

            shootCooldown = shootingRate;
            var shot = (Transform)Instantiate(shotPrefab, transform.position, transform.rotation);
            shot.position = transform.position;

            var shotScript = shot.gameObject.GetComponent<ShotScript>();
            shotScript.isEnemyShot = isEnemy;

            var moveScript = shot.gameObject.GetComponent<MoveScript>();
            if (moveScript != null)
            {
                moveScript.direction = shotScript.speed * transform.right;
            }

            // Jolt weapon sprite and emit a shell casing
            iTween.PunchPosition(gameObject, -0.3f * Vector3.right, 0.8f * shootingRate);
            shellParticle.Emit(1);
        }
    }


    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0;
        }
    }

    public void GiveTemporaryBonus(float newShootingRate, float durationSeconds)
    {
        if (isBoosted)
        {
            additionalBoostSeconds += durationSeconds;
        }
        else
        {
            isBoosted = true;
            StartCoroutine(BoostFirerate(newShootingRate, durationSeconds));
        }
    }

    IEnumerator BoostFirerate(float newShootingRate, float durationSeconds)
    {
        AudioSource.PlayClipAtPoint(bonusStartSound, transform.position);
        shootingRate = newShootingRate;
        yield return new WaitForSeconds(durationSeconds);

        yield return new WaitForSeconds(additionalBoostSeconds);
        additionalBoostSeconds = 0;
        
        AudioSource.PlayClipAtPoint(bonusEndSound, transform.position);
        shootingRate = baseShootingRate;
    }
}
