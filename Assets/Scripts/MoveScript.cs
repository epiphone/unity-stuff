using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{
    [HideInInspector]
    public Vector2 direction = new Vector2(0, 0);

    void FixedUpdate()
    {
        rigidbody2D.velocity = direction;
    }

    /// <summary>
    /// Move towards the given point.
    /// </summary>
    public void MoveTowards(Vector3 target, float speedMultiplier)
    {
        var newDir = target - transform.position;
        newDir.z = 0;
        direction = newDir.normalized * speedMultiplier;
    }
}
