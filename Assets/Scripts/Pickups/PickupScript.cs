using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Common behaviour for all pickup items.
/// </summary>
public class PickupScript : MonoBehaviour
{
    public float lifetime = 6;

    void Start()
    {
        Destroy(gameObject, lifetime);
        StartCoroutine(Wobble());
        StartCoroutine(FlashBeforeDestroy());
    }

    IEnumerator Wobble()
    {
        while (true)
        {
            iTween.ShakePosition(gameObject, new Vector3(0.05f, 0.05f, 0), 2);
            yield return new WaitForSeconds(1.6f);
        }
    }

    IEnumerator FlashBeforeDestroy()
    {
        yield return new WaitForSeconds(0.6f * lifetime);
        while (true)
        {
            gameObject.renderer.enabled = !gameObject.renderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
