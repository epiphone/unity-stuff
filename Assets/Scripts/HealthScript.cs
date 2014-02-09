using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    public int hp = 1;
    public bool isEnemy = true;
    public Transform healthBarPrefab;

    private int initialHp;
    private Transform healthBarInstance;

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ShotScript shot = other.gameObject.GetComponent<ShotScript>();
        if (shot != null && shot.isEnemyShot != isEnemy)
        {
            Damage(shot.damage);
            Destroy(shot.gameObject); // Destroy the projectile
            if (hp > 0 && healthBarPrefab && !healthBarInstance)
            {
                healthBarInstance = (Transform)Instantiate(healthBarPrefab);
            }
            UpdateHealthBar();
        }
    }

    // Use this for initialization
    void Start()
    {
        initialHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarInstance)
        {
            healthBarInstance.position = new Vector3(transform.position.x,
                transform.position.y + 0.2f, transform.position.z);
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
