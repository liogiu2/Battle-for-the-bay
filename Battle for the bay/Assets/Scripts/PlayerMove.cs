using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject camera;
    public GameObject hinge;
    public GameObject hingeLeft;
    public GameObject hingeRight;
    Vector3 bulletPosition;

    float y;
    
    void Start()
    {
        y = transform.rotation.y;
    }

    // Update is called once per frame
    void Update ()
    {
        //var x = Input.GetAxis("Horizontal") * 0.1f;
        //var z = Input.GetAxis("Vertical") * 0.1f;

        //transform.Translate(x, 0, z, Space.World);

        //FIRE LEFT
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("FIRE LEFT");
            FireLeft();
        }

        //FIRE RIGHT
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("FIRE RIGHT");
            FireRight();
        }

        //MOVE THE SHIP
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, -0.06f, Space.Self);
            if (Input.GetKey(KeyCode.A))
            {
                y -= 1.2f;
                
            }

            if (Input.GetKey(KeyCode.D))
            {
                y += 1.2f;
                
                //TILT EFFECT
                //hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, hingeRight.transform.rotation, Time.deltaTime);
                
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, -0.1f, Space.Self);
            }
            
        }

        //UPDATE SHIP POSITION EACH FRAME
        transform.rotation = Quaternion.Euler(0, y, 0);

        //CONTROL VERTICAL VIEW WITH SCROLLWHEEL

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (camera.transform.position.y > 13)
            {
                float y = camera.transform.position.y;
                y += Input.GetAxis("Mouse ScrollWheel");
                Debug.Log("MOVING WHEEL BY: " + y);
                camera.transform.position = new Vector3(camera.transform.position.x, y, camera.transform.position.z);
            }
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (camera.transform.position.y < 22)
            {
                float y = camera.transform.position.y;
                y += Input.GetAxis("Mouse ScrollWheel");
                camera.transform.position = new Vector3(camera.transform.position.x, y, camera.transform.position.z);
            } 
        }

    }    

    void FireRight()
    {
        //bulletPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        
        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + transform.forward,
            Quaternion.Euler(-10, y - 90, 0));

        //COLOR THE BULLET
        bullet.GetComponent<MeshRenderer>().material.color = Color.black;

        //GIVE INITIAL VELOCITY TO THE BULLET
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
        
        Destroy(bullet, 3.0f);
    }

    void FireLeft()
    {
        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + transform.forward,
            Quaternion.Euler(-10, y + 90, 0));
        
        //COLOR THE BULLET
        bullet.GetComponent<MeshRenderer>().material.color = Color.black;

        //GIVE INITIAL VELOCITY TO THE BULLET
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
        
        Destroy(bullet, 3.0f);
    }
}
