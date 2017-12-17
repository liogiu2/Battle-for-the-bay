using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Score : MonoBehaviour
{

    public float TimeMinutesToFinish = 15;
    public int Points;
    private int _finalGamePoint;
    private bool _done = false;
    private GameObject _score;
    private GameObject _name;
    private GameObject _leaderBoard;
    private bool _alreadySent = false;
    private Text _scoreOnGUI;
    private GameObject _tutorial;
    private string _oldSceneName;


    // Use this for initialization
    void Start()
    {
        Points = 0;
        DontDestroyOnLoad(this);
        _scoreOnGUI = GameObject.Find("HUD").transform.Find("Canvas").transform.Find("Panel").transform.Find("Score").transform.Find("ScoreLabel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_scoreOnGUI)
        {
            _scoreOnGUI.text = "Score: " + Points;
        }
        else
        {
            _scoreOnGUI = null;
        }
        Scene scene = SceneManager.GetActiveScene();
        if (_oldSceneName != scene.name)
        {
            _oldSceneName = scene.name;
            if (GameObject.Find("GameManager") != null)
            {
                _tutorial = GameObject.Find("HUD").transform.Find("Canvas").transform.Find("Tutorial").gameObject;
                _tutorial.SetActive(true);
            }
        }
        if (scene.name == "PlayerWon" && !_done)
        {
            _done = true;
            _score = GameObject.Find("Menu").transform.Find("Score").gameObject;
            _score.transform.Find("Points").GetComponent<Text>().text = _finalGamePoint.ToString();
            _name = GameObject.Find("Menu").transform.Find("Name").gameObject;
            _name.transform.Find("Button").GetComponent<Button>().onClick.AddListener(SaveHighScore);
            _leaderBoard = GameObject.Find("Menu").transform.Find("OpenLeaderboard").gameObject;
            _leaderBoard.GetComponent<Button>().onClick.AddListener(OpenLeaderboard);
        }
    }

    public int GetPoints()
    {
        return Points;
    }

    public void AddPoints(int points)
    {
        Points += points;
    }

    public void FinishGame()
    {
        float timeSinceLevelLoad = Time.timeSinceLevelLoad / 60;
        if (timeSinceLevelLoad > TimeMinutesToFinish)
        {
            float moreTime = timeSinceLevelLoad - timeSinceLevelLoad;
            AddPoints((int)-(moreTime * 300));
        }
        else
        {
            float lessTime = timeSinceLevelLoad - timeSinceLevelLoad;
            AddPoints((int)(lessTime * 300));
        }
        if (Points < 0)
        {
            Points = 0;
        }
        _finalGamePoint = Points;
        Points = 0;
    }

    private void SaveHighScore()
    {
        string unityPlayer = _name.transform.Find("InputName").GetChild(0).GetComponent<InputField>().text;
        if (unityPlayer != "" && !_alreadySent)
        {
            Debug.Log("sending highscore data");
            UnityWebRequest www = UnityWebRequest.Get("https://fast-lowlands-46452.herokuapp.com/registerScore/" + unityPlayer + "/" + _finalGamePoint.ToString());
            www.SendWebRequest();
            _name.transform.Find("Button").gameObject.SetActive(false);
            _alreadySent = true;
        }
    }

    public void ResetPoint()
    {
        Points = 0;
        _done = false;
        _alreadySent = false;
        _scoreOnGUI = GameObject.Find("HUD").transform.Find("Canvas").transform.Find("Panel").transform.Find("Score").transform.Find("ScoreLabel").GetComponent<Text>();
    }

    private void OpenLeaderboard()
    {
        Application.OpenURL("https://fast-lowlands-46452.herokuapp.com");
    }
}
