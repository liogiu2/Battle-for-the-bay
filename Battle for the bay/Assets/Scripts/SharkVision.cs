using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkVision : MonoBehaviour {
    public SharkBehaviour sharkBehaviour;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // if(ai.targetsInVision != null) ai.targetsInVision.RemoveAll(item => item == null);
        if (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyMinion")
        {
            sharkBehaviour.targetsInVision.Add(other.gameObject);
            sharkBehaviour.ChangeState(SharkBehaviour.STATE.Chase);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyMinion")
        {
            sharkBehaviour.targetsInVision.Remove(other.gameObject);
        }
        if (sharkBehaviour.targetsInVision.Count == 0 || sharkBehaviour.targetsInVision == null) sharkBehaviour.ChangeState(SharkBehaviour.STATE.Wander);
    }
}
