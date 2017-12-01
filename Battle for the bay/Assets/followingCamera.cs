using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingCamera : MonoBehaviour {
    public float cameraY = 12;
    public float cameraZ = 5;
    private Camera mainCamera;
    private GameObject player;

    // Use this for initialization
    void Start () {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 playerInfo = player.transform.transform.position;
        Debug.Log(mainCamera.transform.rotation);
        mainCamera.transform.position = new Vector3(playerInfo.x, playerInfo.y + cameraY, playerInfo.z - cameraZ);
    }
}
