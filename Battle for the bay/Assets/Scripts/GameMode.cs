using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour {
    public GameObject playerBase;
    public GameObject enemyBase;

    public void NewGameBtn(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
    }

    private void Update()
    {
        if (!playerBase) {
            Debug.Log("You lost!");
            SceneManager.LoadScene(3);
        } else if (!enemyBase) {
            Debug.Log("You won!");
            SceneManager.LoadScene(2);
        }
    }
}
