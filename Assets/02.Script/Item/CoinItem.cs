using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : BaseItem
{
    public int Coin;
    public override void GetItem()
    {
        base.GetItem();

        GameInstance.Instance.Coin += Coin;  
    }
}
