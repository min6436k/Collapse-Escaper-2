using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform WayPointParent;
    public List<Transform> WayPoints = new List<Transform>();

    public GameObject Player;
    public GameObject Enumy;

    public GameObject CheatF1;

    public int MaxWayIndex;

    public int StageMaxLap = 3;

    public bool bCarMove = false;

    public bool bFreeShoping = false;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        MaxWayIndex = WayPointParent.childCount;

        foreach (Transform item in WayPointParent.transform)
        {
            WayPoints.Add(item);
        }
    }

    private void Update()
    {
        Cheat();
    }

    public void GoMain()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void GameClear()
    {
        Debug.Log(Time.time - GetComponentInChildren<TimeManager>().StartTime);
        bCarMove = false;
        GameInstance.Instance.StageClearTime[GameInstance.Instance.CurrentStage - 1] = Time.time - GetComponentInChildren<TimeManager>().StartTime;
        GameInstance.Instance.Coin += GameInstance.Instance.CurrentStage * 5000000;

        Player.GetComponent<Effect>().SetWheelEmission(250);

        GetComponentInChildren<InGameUI>().SetEndUI(true);

        if (GameInstance.Instance.CurrentStage == 3) GameInstance.Instance.AddRanking();
        else GameInstance.Instance.CurrentStage++;
    }

    public void GameOver()
    {
        GetComponentInChildren<InGameUI>().SetEndUI(false);
        GameInstance.Instance.ReTryNum++;
        bCarMove = false;
    }

    public void TimeStopTogle()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    //ġƮ

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GetComponentInChildren<InGameUI>().OpenUI(CheatF1);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            bFreeShoping = true;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadSceneAsync(GameInstance.Instance.CurrentStage);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (GameInstance.Instance.CurrentStage == 3) return;

            GameInstance.Instance.CurrentStage++;

            SceneManager.LoadSceneAsync(GameInstance.Instance.CurrentStage);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameManager.Instance.TimeStopTogle();
        }
    }
}
