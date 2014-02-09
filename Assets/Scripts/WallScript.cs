using UnityEngine;
using System.Collections;

/// <summary>
/// If colliding with a shot, destroy the shot.
/// </summary>
public class WallScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        ShotScript shot = coll.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            Destroy(shot.gameObject);
        }
    }
}
