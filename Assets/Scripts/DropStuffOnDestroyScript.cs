using UnityEngine;
using System.Collections;

/// <summary>
/// Make objects drop loot when destroyed.
/// </summary>
public class DropStuffOnDestroyScript : MonoBehaviour
{
    public float dropProbability = 0.2f;
    public Transform[] dropObjectPrefabs;

    void OnDestroy()
    {
        if (Random.value <= dropProbability)
        {
            var drop = dropObjectPrefabs[Random.Range(0, dropObjectPrefabs.Length)];
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Prevent script from dropping loot when game is stopped.
    /// </summary>
    void OnApplicationQuit()
    {
        DestroyImmediate(this);
    }
}
