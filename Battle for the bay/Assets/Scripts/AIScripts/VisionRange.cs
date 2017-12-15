using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{

    public AIRootMovement ai;
    // private List<GameObject> targetsInVision;
    // private GameObject currentTarget;
    // Use this for initialization
    void Start()
    {
        // targetsInVision = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // targetsInVision.RemoveAll(item => item == null);
        // if (ai.gameObject.tag == "EnemyMinion")
        // {
        //     if(other.gameObject.tag == "Player") ai.target = other.gameObject;
        //     else if(other.gameObject.tag == "PlayerMinion" && ai.target.tag != "PlayerMinion" && ai.target.tag != "Player") ai.target = other.gameObject;
        //     ai.ChangeState(AIRootMovement.STATE.Chase);
        // }
        // if (ai.gameObject.tag == "PlayerMinion")
        // {
        //     if(other.gameObject.tag == "Enemy") ai.target = other.gameObject;
        //     else if(other.gameObject.tag == "EnemyMinion" && ai.target.tag != "EnemyMinion" && ai.target.tag != "Enemy") ai.target = other.gameObject;
        //     ai.ChangeState(AIRootMovement.STATE.Chase);
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        // if(ai.targetsInVision != null) ai.targetsInVision.RemoveAll(item => item == null);
        if (ai.gameObject.tag == "EnemyMinion" && (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerTower" ))
        {
            ai.targetsInVision.Add(other.gameObject);
            // if(currentTarget == null) currentTarget = other.gameObject;
            // else if(currentTarget.tag != "Player" && other.gameObject.tag == "Player") currentTarget = other.gameObject;
            // ai.target = currentTarget;
            ai.SetDestination(AIRootMovement.DESTINATION.Opponent);
            ai.ChangeState(AIRootMovement.STATE.Chase);
        }
        if (ai.gameObject.tag == "PlayerMinion" && (other.gameObject.tag == "EnemyMinion" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyTower"))
        {
            ai.targetsInVision.Add(other.gameObject);
            // if(currentTarget == null) currentTarget = other.gameObject;
            // else if(currentTarget.tag != "Enemy" && other.gameObject.tag == "Enemy") currentTarget = other.gameObject;
            // ai.target = currentTarget;
            ai.SetDestination(AIRootMovement.DESTINATION.Opponent);
            ai.ChangeState(AIRootMovement.STATE.Chase);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ai.gameObject.tag == "EnemyMinion" && (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerTower"))
        {
            ai.targetsInVision.Remove(other.gameObject);
        }
        if (ai.gameObject.tag == "PlayerMinion" && (other.gameObject.tag == "EnemyMinion" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyTower"))
        {
            ai.targetsInVision.Remove(other.gameObject);
        }
        if (ai.targetsInVision.Count == 0 || ai.targetsInVision == null) ai.ChangeState(AIRootMovement.STATE.GoToOpponentBase);
        // if(targetsInVision != null) targetsInVision.RemoveAll(item => item == null);
        // if (ai.gameObject.tag == "EnemyMinion" &&  (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" ))
        // {
        //     targetsInVision.Remove(other.gameObject);
        //     if(currentTarget == other.gameObject){
        //         GameObject player = targetsInVision.Find(item => item.tag == "Player");
        //         if(player != null){
        //             currentTarget = player;
        //             ai.target = currentTarget;
        //             ai.ChangeState(AIRootMovement.STATE.Chase);
        //         } else if(targetsInVision.Count > 0){
        //             currentTarget = targetsInVision[0];
        //             ai.target = currentTarget;
        //             ai.ChangeState(AIRootMovement.STATE.Chase);
        //         } else{
        //             ai.ChangeState(AIRootMovement.STATE.GoToOpponentBase);
        //         }
        //     }
        // }
        // if (ai.gameObject.tag == "PlayerMinion" &&  (other.gameObject.tag == "EnemyMinion" || other.gameObject.tag == "Enemy" ))
        // {
        //     targetsInVision.Remove(other.gameObject);
        //     if(currentTarget == other.gameObject){
        //         GameObject player = targetsInVision.Find(item => item.tag == "Enemy");
        //         if(player != null){
        //             currentTarget = player;
        //             ai.target = currentTarget;
        //             ai.ChangeState(AIRootMovement.STATE.Chase);
        //         } else if(targetsInVision.Count > 0){
        //             currentTarget = targetsInVision[0];
        //             ai.target = currentTarget;
        //             ai.ChangeState(AIRootMovement.STATE.Chase);
        //         } else{
        //             ai.ChangeState(AIRootMovement.STATE.GoToOpponentBase);
        //         }
        //     }
        // }
    }
}
