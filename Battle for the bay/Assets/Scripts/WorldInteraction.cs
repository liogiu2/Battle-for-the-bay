using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {
    NavMeshAgent playerAgent;

    private void Start() {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        if (Input.GetMouseButtonDown(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            getIntercation();
        }

        if (Input.GetKeyDown("s")) {
            Debug.Log("pressing S");
            playerAgent.Stop(true);
            playerAgent.ResetPath();
        }
    }

    void getIntercation() {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if(Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if(interactedObject.tag == "Interactive Object") {
                
            } else {
                playerAgent.destination = interactionInfo.point;

            }
        }
    }
}