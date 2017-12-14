using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour
{
    private Transform pointerTransform;
    private SpriteRenderer pointerSprite;
    private AIRootScript rootScript;

    public ShipMovement shipMovement;
    public float minMoveRange;
    public GameObject upgradeCanvas;

    // Use this for initialization
    void Start()
    {
        GameObject shipPointer = GameObject.Find("ShipPointer").gameObject;
        pointerTransform = shipPointer.GetComponent<Transform>();
        pointerSprite = shipPointer.GetComponent<SpriteRenderer>();
        rootScript = GetComponent<AIRootScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerTransform.localScale.x > 1f)
        {
            pointerTransform.localScale -= new Vector3(.1f, .1f, .1f);
        }

        if (Input.GetButton("Fire2"))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "Terrain")
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    if(pointerTransform.localScale.x < 2f)  pointerTransform.localScale += new Vector3(1f, 1f, 1f);
                    pointerTransform.position = new Vector3(hit.point.x, pointerTransform.position.y, hit.point.z);

                    if (Vector3.Distance(pointerTransform.position, shipMovement.transform.position) > minMoveRange)
                    {
                        shipMovement.moving = true;
                        pointerSprite.enabled = true;
                    }

                    if (upgradeCanvas.activeSelf == true)
                    {
                        upgradeCanvas.SetActive(false);
                    }

                }

                if (hit.collider.tag == "EnemyMinion")
                {
                    if (rootScript.TargetEnemy)
                    {
                        rootScript.TargetEnemy.SendMessage("DeActivateTarget");
                        rootScript.StopCorutineFire();
                    }
                    GameObject Ship = hit.collider.gameObject;
                    Ship.SendMessage("ActivateTarget");
                    rootScript.TargetEnemy = Ship;
                }

                if (hit.collider.tag == "PlayerBase")
                {
                    upgradeCanvas.SetActive(true);
                }

                if (hit.collider.tag == "Constuctable")
                {
                    Debug.Log("CONSTRUCTABLE");
                }
            }
        }
    }

}
