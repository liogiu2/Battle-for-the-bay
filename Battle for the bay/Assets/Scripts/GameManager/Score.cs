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

    // Use this for initialization
    void Start()
    {
        Debug.Log("calling highscorefunction");
        Points = 0;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "PlayerWon" && !_done)
        {
            _done = true;
            _score = GameObject.Find("Menu").transform.Find("Score").gameObject;
            _score.transform.Find("Points").GetComponent<Text>().text = _finalGamePoint.ToString();
            _name = GameObject.Find("Menu").transform.Find("Name").gameObject;
            _name.transform.Find("Button").GetComponent<Button>().onClick.AddListener(SaveHighScore);
        }
        else if (scene.name == "PlayerLost" && !_done)
        {
            _done = true;
            Points = 0;
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
            AddPoints((int)-(moreTime * 100));
        }
        else
        {
            float lessTime = timeSinceLevelLoad - timeSinceLevelLoad;
            AddPoints((int)(lessTime * 100));
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
        if (unityPlayer != "")
        {
            Debug.Log("sending highscore data");
            UnityWebRequest www = UnityWebRequest.Get("https://fast-lowlands-46452.herokuapp.com/registerScore/" + unityPlayer + "/" + _finalGamePoint.ToString());
            www.SendWebRequest();
        }
    }

    public void ResetPoint(){
        Points = 0;
    }
}
