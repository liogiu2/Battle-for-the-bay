using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    public float TimeMinutesToFinish = 15;
    public static int Points;

    // Use this for initialization
    void Start()
    {
        Points = 0;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

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
    }
}
