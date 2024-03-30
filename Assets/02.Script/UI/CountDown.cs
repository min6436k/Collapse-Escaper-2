using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{

    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        Invoke("GameStart", 3f);
    }
    public void Count(int num)
    {
        _text.text = num.ToString();
    }

    public void GameStart()
    {
        GameManager.Instance.GetComponentInChildren<TimeManager>().StartRecord();
        GameManager.Instance.bCarMove = true;

        Destroy(gameObject,1f);
    }
}
