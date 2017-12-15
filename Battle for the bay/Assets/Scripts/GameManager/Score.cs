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
    private bool _done = false;

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
            GameObject.Find("Menu").transform.Find("Won").GetComponent<Text>().text = "Yoy won matey!\nScore: "+Points;
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
        SaveHighScore();
    }

    private void SaveHighScore()
    {
        Debug.Log("sending highscore data");
        UnityWebRequest www = UnityWebRequest.Get("https://fast-lowlands-46452.herokuapp.com/registerScore/unityPlayer/" + Points.ToString());
        www.SendWebRequest();
    }
}
