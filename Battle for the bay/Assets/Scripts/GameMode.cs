using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public GameObject playerBase;
    public GameObject enemyBase;
    public bool GamePaused;
    private GameObject _tutorial;

    public void NewGameBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    void Start()
    {
        GameObject.Find("Score").GetComponent<Score>().ResetPoint();
        _tutorial = GameObject.Find("HUD").transform.Find("Canvas").transform.Find("Panel").transform.Find("Tutorial").gameObject;
        GameObject.Find("HUD").transform.Find("Canvas").transform.Find("Panel").transform.Find("Tutorial").transform.Find("Button").GetComponent<Button>().onClick.AddListener(StopPause);
        GamePaused = true;
    }

    private void Update()
    {
        if (GamePaused && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (!GamePaused && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        if (!playerBase)
        {
            Debug.Log("You lost!");
            SceneManager.LoadScene(3);
        }
        else if (!enemyBase)
        {
            Debug.Log("You won!");
            GameObject.Find("Score").GetComponent<Score>().FinishGame();
            SceneManager.LoadScene(2);
        }
    }

    public void StartPause()
    {
        GamePaused = true;
    }

    public void StopPause()
    {
        GamePaused = false;
        _tutorial.SetActive(false);
    }
}
