using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour
{
    public Vector2 direction = new Vector2(0, 0);
    public MoveEnum moveMode = MoveEnum.Normal; // Testing different methods.
    public float acceleration = 10;

    public enum MoveEnum
    {
        Normal,
        Lerp
    }

    void FixedUpdate()
    {
        Vector2 velocity = direction;
        if (moveMode == MoveEnum.Lerp)
        {
            velocity = Vector2.Lerp(rigidbody2D.velocity, direction, acceleration * Time.deltaTime);
        }

        rigidbody2D.velocity = velocity;
    }

    /// <summary>
    /// Move towards the given point.
    /// </summary>
    public void MoveTowards(Vector3 target, float speedMultiplier)
    {
        var newDir = target - transform.position;
        newDir.z = 0;
        direction = speedMultiplier * newDir.normalized;
    }
}
