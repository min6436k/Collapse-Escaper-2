using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public List<Transform> Levels = new List<Transform>();
    public TextMeshProUGUI CoinText;

    private int[] Prices = { 1000000, 5000000, 8000000 };
    void Start()
    {
        UpdateLevels();
    }



    public void UpdateLevels()
    {
        CoinText.text = "보유금 : " + GameInstance.Instance.Coin.ToString() + "원";

        for (int i = 0; i < Levels.Count; i++)
        {
            for (int j = 0; j < Levels[i].childCount; j++)
            {
                if (j < GameInstance.Instance.PartsLevel[i]) Levels[i].GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
            }
        }
    }

    public void Buy(int index)
    {
        if ((GameInstance.Instance.Coin >= Prices[index] || GameManager.Instance.bFreeShoping) &&
            Levels[index].transform.childCount > GameInstance.Instance.PartsLevel[index])
        {
            GameManager.Instance.bFreeShoping = false;

            GameInstance.Instance.Coin -= Prices[index];
            GameInstance.Instance.PartsLevel[index]++;

            UpdateLevels();
            GameManager.Instance?.Player.GetComponent<PlayerController>().UpdateEngineUpgrade();
        }
    }
}
