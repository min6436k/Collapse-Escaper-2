using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI Lap;
    public TextMeshProUGUI RaceTime;

    public Transform Needle;

    public GameObject GameUI;
    public GameObject ShopUI;
    public GameObject EndUI;

    public GameObject ItemNotePrefab;

    public List<Transform> PartLevels = new List<Transform>();

    private CarInfo _playerCarInfo;
    private TimeManager _timeManager;

    private Stack<GameObject> _currentOpenUI = new Stack<GameObject>();
    void Start()
    {
        _playerCarInfo = GameManager.Instance.Player.GetComponent<CarInfo>();
        _timeManager = GameManager.Instance.GetComponentInChildren<TimeManager>();
        _currentOpenUI.Push(GameUI);

        UpdateLevels();
    }

    void Update()
    {
        Lap.text = _playerCarInfo.Lap.ToString();
        UpdateRaceTime();
        Needle.rotation = Quaternion.Euler(0,0,220-_playerCarInfo.SpeedPerHour/5*7);
    }

    void UpdateRaceTime()
    {
        if (GameManager.Instance.bCarMove == false) return;

        float t = Time.time - _timeManager.StartTime;
        RaceTime.text = "TIME : " + (int)t / 60 + ":" + (t % 60).ToString("00.00");
        t = _timeManager.BestTime;
        RaceTime.text += "\nBEST : " + (int)t / 60 + ":" + (t % 60).ToString("00.00");
    }

    public void GetItem(string name)
    {
        TextMeshProUGUI temp = Instantiate(ItemNotePrefab,GameUI.transform).GetComponent<TextMeshProUGUI>();
        temp.text = name + " 아이템을 확득하였습니다.";
        Destroy(temp.gameObject,3f);
    }

    public void GameEnd()
    {
        OpenUI(EndUI);
    }

    public void GoShop()
    {
        GameManager.Instance.TimeStopTogle();
        ShopUI.GetComponent<ShopUI>().UpdateLevels();
        OpenUI(ShopUI);
    }

    public void UpdateLevels()
    {
        for (int i = 0; i < PartLevels.Count; i++)
        {
            for (int j = 0; j < PartLevels[i].childCount; j++)
            {
                if (j < GameInstance.Instance.PartsLevel[i]) PartLevels[i].GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
            }
        }
    }

    public void SetEndUI(bool win)
    {
        var t = EndUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (win)
        {
            t.text = "1st,\t\tYou Win";
            t = EndUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            if (GameInstance.Instance.CurrentStage < 3)
            {
                t.text =    "레이싱 경기에서 승리하셨습니다\n\n" +
                            $"상금으로 {GameInstance.Instance.CurrentStage * 5000000}원을 획득하였습니다.\n\n" +
                            $"상금을 통해 상점에서 차를 강화하거나,\n다음 스테이지에 도전하실 수 있습니다.";
            }
            else
            {
                t.text =    "레이싱 경기에서 승리하셨습니다\n\n" +
                            $"당신은 희망의 도시로 이주할 수 있게 되었습니다.\n\n" +
                            $"랭킹에 점수가 기록되었습니다.";
            }
        }
        else
        {
            t.text = "2st,\t\tYou Lose";
            t = EndUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            t.text = "\n레이싱 경기에서 패배하셨습니다\n\n" +
                        $"아이템으로 얻은 돈을 통해 상점에서 차를 강화하여,\n스테이지에 재도전하실 수 있습니다.";
        }


        OpenUI(EndUI);
    }

    public void OpenUI(GameObject ui)
    {
        _currentOpenUI.Peek().SetActive(false);
        _currentOpenUI.Push(ui);

        ui.SetActive(true);
    }

    public void CloseUI()
    {
        UpdateLevels();

        _currentOpenUI.Pop().SetActive(false);
        _currentOpenUI.Peek().SetActive(true);
    }

    public void CheatItemSpawn(GameObject item)
    {
        Instantiate(item,GameManager.Instance.Player.transform.position + GameManager.Instance.Player.transform.forward*2,Quaternion.identity);
    }

}
