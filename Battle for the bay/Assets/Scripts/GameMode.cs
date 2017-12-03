using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour {

	public void NewGameBtn(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
    }
}
