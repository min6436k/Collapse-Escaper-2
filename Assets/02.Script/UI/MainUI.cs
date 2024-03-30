using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public List<GameObject> LevelMap = new List<GameObject>();

    public List<Button> PlayButtons = new List<Button>();

    public TextMeshProUGUI RankingText;

    private Stack<GameObject> _currentOpenUI = new Stack<GameObject>();
    void Start()
    {
        _currentOpenUI.Push(transform.GetChild(0).gameObject);

        for (int i = 0; i < LevelMap.Count; i++)
        {
            if(i == GameInstance.Instance.CurrentStage-1) LevelMap[i].SetActive(true);
            else LevelMap[i].SetActive(false);
        }

        UpdateRanking();
        SetPlayButtonActive();
    }

    void SetPlayButtonActive()
    {
        for (int i = 0; i < PlayButtons.Count; i++)
            PlayButtons[i].interactable = i == GameInstance.Instance.CurrentStage-1 ? true : false;
    }

    public void OpenUI(GameObject ui)
    {
        _currentOpenUI.Peek().SetActive(false);
        _currentOpenUI.Push(ui);

        ui.SetActive(true);
    }

    public void CloseUI()
    {
        _currentOpenUI.Pop().SetActive(false);
        _currentOpenUI.Peek().SetActive(true);
    }

    public void OpenStage(int num)
    {
        SceneManager.LoadSceneAsync(num);
    }

    void UpdateRanking()
    {
        if (GameInstance.Instance == null) return;

        string[] temp = { "1st : ","2nd : ","3rd : ","4th : ","5th : "};

        for (int i = 0; i < GameInstance.Instance.Rankings.Count; i++)
        {
            temp[i] += GameInstance.Instance.Rankings[i].ToString(".00");
        }

        RankingText.text = string.Join("\n", temp);
    }
}
