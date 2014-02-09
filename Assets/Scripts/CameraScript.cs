using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public float cameraSpeed = 1.5f;

    private Transform bg;
    private Transform player;

	// Use this for initialization
	void Awake () {
        bg = GameObject.Find("background").transform;
        player = GameObject.Find("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        var target = player.position;
        target.z = transform.position.z;
        transform.position = target;
	}
}
