using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingCamera : MonoBehaviour {
    public float cameraY = 12;
    public float cameraZ = 5;
    private Camera mainCamera;
    private GameObject player;

    //
    bool flag;
    public Camera miniMap;
    //public GameObject mapHolder;
    public LayerMask layerMask;
    public bool mouseEntered { get; set; }
    //

    void Start () {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        flag = true;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            flag = !flag;
        }

        if (flag == true)
        {
            Vector3 playerInfo = player.transform.transform.position;
            mainCamera.transform.position = new Vector3(playerInfo.x, playerInfo.y + cameraY, playerInfo.z - cameraZ);
        }
        else
        {
            //MOVE CAMERA ON CLICK
            if (mouseEntered)
            {
                Debug.Log("MOUSE ENTERED");
                if (Input.GetMouseButton(1))
                {
                    RaycastHit hit;
                    Ray ray = miniMap.ScreenPointToRay(Input.mousePosition);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        Vector3 miniMapPosition = hit.point;
                        Vector3 camViewCenter;
                        RaycastHit cameraView;
                        Vector3 camDestPos;

                        Ray cameraCenter = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                        Physics.Raycast(cameraCenter, out cameraView, Mathf.Infinity, layerMask);
                        Debug.DrawRay(cameraCenter.origin, cameraCenter.direction * 10, Color.blue);
                        camViewCenter = cameraView.point;

                        camDestPos = miniMapPosition - camViewCenter;
                        camDestPos.y = 0;
                        //mapHolder.transform.position += camDestPos;
                        transform.position += camDestPos;
                    }
                }
            }
            //this.transform.position = mapHolder.transform.position;
            //this.transform.position = new Vector3(transform.position.x, 50, transform.position.z);

            //if (miniMap  IsInViewport(Input.mousePosition))
            //{
            //    RaycastHit hit;
            //    Ray ray = player.hud.MiniMap.minimapCamera.ScreenPointToRay(Input.mousePosition);
            //    LayerMask water = (1 << 8);

            //    // Trace ray from minimap viewport, ignoring everything except the ground
            //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, CustomLayerMask.Water))
            //    {
            //        Vector3 miniMapPosition = hit.point;
            //        Vector3 camViewCenter;
            //        RaycastHit cameraView;
            //        Vector3 camDestPos;

            //        // Project a ray from the center of the main camera to find current world position
            //        Ray cameraCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            //        Physics.Raycast(cameraCenter, out cameraView, Mathf.Infinity, CustomLayerMask.Water);
            //        camViewCenter = cameraView.point;

            //        // Calculate change to move from current position to new minimap location                    
            //        camDestPos = miniMapPosition - camViewCenter;
            //        camDestPos.y = 0;           // maintain current height

            //        Debug.Log("Camera: " + Camera.main.transform.position);
            //        Debug.Log("Add to: " + camDestPos);
            //        Camera.main.transform.position += camDestPos;
            //    }
            //}
        }        
    }
}
