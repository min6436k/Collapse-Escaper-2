using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : BaseItem
{
    public override void GetItem()
    {
        base.GetItem();

        GameManager.Instance.GetComponentInChildren<InGameUI>().GoShop();
    }
}
