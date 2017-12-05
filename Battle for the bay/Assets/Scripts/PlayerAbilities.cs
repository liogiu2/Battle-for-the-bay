using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    int aimMode = 0;
    public Transform areaPointer;
    public Transform rangePointer;
    SpriteRenderer areaSprite;
    SpriteRenderer rangeSprite;

    void Start() {
        areaSprite = areaPointer.GetComponent<SpriteRenderer>();
        rangeSprite = rangePointer.GetComponent<SpriteRenderer>();
    }

    void Update() {
        areaSprite.enabled = false;
        rangeSprite.enabled = false;
        // Toggling aim mode
        if (Input.GetKeyDown("q"))
        {
            aimMode = (aimMode == 0) ? 1 : 0;
        }
        else if (Input.GetKeyDown("w"))
        {
            aimMode = (aimMode == 0) ? 2 : 0;
        }
        else if (Input.GetKeyDown("e"))
        {
            aimMode = (aimMode == 0) ? 3 : 0;
        }

        if (aimMode != 0) {
            Debug.Log("in aiming mode");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                switch (aimMode)
                {
                    case 1:

                        break;
                    case 2:
                        // Range mode
                        rangePointer.position = this.gameObject.transform.position;
                        rangeSprite.enabled = true;
                        break;
                    case 3:
                        // Splash Area mode
                        areaPointer.position = new Vector3(hit.point.x, areaPointer.position.y, hit.point.z);
                        areaSprite.enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}