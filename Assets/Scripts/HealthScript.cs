using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    public int hp = 1;
    public bool isEnemy = true;
    public Transform healthBarPrefab;
    public float healthBarYOffset = 0.2f;

    private int initialHp;
    private Transform healthBarInstance;

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            if (isEnemy)
            {
                var enemy = transform.GetComponentInChildren<EnemyScript>();
                if (enemy && enemy.deathSound)
                {
                    AudioSource.PlayClipAtPoint(enemy.deathSound, transform.position);   
                }
            }
            Destroy(gameObject);
        }
        else if (healthBarPrefab && !healthBarInstance)
        {
            healthBarInstance = (Transform)Instantiate(healthBarPrefab);
        }

        UpdateHealthBar();
    }

    /// <summary>
    /// Reduce health when collided with a shot.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if (shot != null && shot.isEnemyShot != isEnemy)
        {
            AudioSource.PlayClipAtPoint(shot.shotHitSound, transform.position);
            Damage(shot.damage);
            Destroy(shot.gameObject);
        }
    }

    void Start()
    {
        initialHp = hp;
    }

    void Update()
    {
        if (healthBarInstance)
        {
            healthBarInstance.position = new Vector3(transform.position.x,
                transform.position.y + healthBarYOffset, transform.position.z);
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarInstance)
        {
            var redBar = healthBarInstance.transform.Find("healthbar-inner");
            redBar.transform.localScale = new Vector3((float)hp / initialHp, 1, 1);
        }
    }

    void OnDestroy()
    {
        if (healthBarInstance)
        {
            Destroy(healthBarInstance.gameObject);
        }
    }
}
