using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance;

    public int Coin;
    public int CurrentStage = 1;

    public float[] PartsLevel = { 0, 0, 0 };
    public float[] StageClearTime = { 0, 0, 0 };

    public int ReTryNum = 0;

    public List<float> Rankings = new List<float>();

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void AddRanking()
    {
        Rankings.Add(500-StageClearTime.Sum()- ReTryNum*10);
        Rankings.Sort();
        Rankings.Reverse();

        if (Rankings.Count > 5) Rankings.RemoveAt(5);

        InitGame();
    }

    void InitGame()
    {
        PartsLevel = new float[] { 0, 0, 0 };
        StageClearTime = new float[] { 0, 0, 0 };

        Coin = 0;
        CurrentStage = 1;
    }
}
