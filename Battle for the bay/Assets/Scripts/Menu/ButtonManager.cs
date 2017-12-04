using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    void OnMouseOver()
    {
        Debug.Log("Mouse is over GameObject.");
    }

    public void NewGameBtn(string newGameLevel) {
        Debug.Log("Loading Map");
        SceneManager.LoadScene(1);
    }
    public void ExitGame() {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
