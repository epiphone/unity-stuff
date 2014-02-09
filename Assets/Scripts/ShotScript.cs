using UnityEngine;
using System.Collections;

/// <summary>
/// Clear projectile after 5 seconds.
/// </summary>
public class ShotScript : MonoBehaviour {

    public int damage = 1;
    public float speed = 10;
    
    /// <summary>
    /// Shot by an enemy?
    /// </summary>
    public bool isEnemyShot = false;

    void Start () {
        Destroy(gameObject, 5);
	}
}
