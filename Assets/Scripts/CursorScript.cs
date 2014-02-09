using UnityEngine;
using System.Collections;

/// <summary>
/// Moves the cursor according to weapon rotation.
/// </summary>
public class CursorScript : MonoBehaviour
{

    private Transform weapon;

    void Start()
    {
        weapon = GameObject.Find("weapon1").transform;
    }

    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var wpnToMouseDir = mousePos - weapon.position;
        wpnToMouseDir.z = 0;

        var cursorPos = weapon.rotation * new Vector3(1, 0, 0) * wpnToMouseDir.magnitude;
        transform.position = weapon.position + cursorPos;
    }
}
