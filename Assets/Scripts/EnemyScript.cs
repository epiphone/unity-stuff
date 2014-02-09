using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float chaseSpeed = 6f;
    public float idleSpeed = 3f;
    public float turnSpeed = 6f;

    /// <summary>
    /// Time waited after looking for player before looking again.
    /// </summary>
    public float linecastInterval = 0.5f;
    public float linecastIntervalWhenChasing = 0.2f;

    private MoveScript moveScript;
    private Transform player;
    private Vector3 playerLastSeenLocation;
    private bool canSeePlayer = false;

    void Awake()
    {
        moveScript = GetComponent<MoveScript>();
        player = GameObject.Find("player").transform;
    }

    void Start()
    {
        playerLastSeenLocation = transform.position;
        StartCoroutine(SeekPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer)
        {
            moveScript.MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            if ((playerLastSeenLocation - transform.position).magnitude < 0.1f)
            {
                moveScript.direction = Vector2.zero; // Player got away.
            }
        }


        var moveDirection = moveScript.direction;
        if (moveDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Look for the player, go after him if found.
    /// </summary
    IEnumerator SeekPlayer()
    {
        if (!Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("Wall")))
        {
            playerLastSeenLocation = player.position;
            canSeePlayer = true;
        }
        else
        {
            if (playerLastSeenLocation != null)
            {
                moveScript.MoveTowards(playerLastSeenLocation, idleSpeed);
            }
            canSeePlayer = false;
        }

        var waitFor = canSeePlayer ? linecastIntervalWhenChasing : linecastInterval;
        yield return new WaitForSeconds(waitFor);

        yield return StartCoroutine(SeekPlayer());
    }
}
