using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    int prepType = 0;

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown("1")) {
            prepType = 1;
        } else if (Input.GetKeyDown("2")) {
            prepType = 2;
        } else if (Input.GetKeyDown("3")) {
            prepType = 3;
        }
    }
}