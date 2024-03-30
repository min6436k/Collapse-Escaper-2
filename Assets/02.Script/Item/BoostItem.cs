using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : BaseItem
{
    public float Force;
    public GameObject BoostPrefab;
    public override void GetItem()
    {
        base.GetItem();

        GameManager.Instance.Player.GetComponent<PlayerController>().BoostItem(Force);

        GameObject instance = Instantiate(BoostPrefab, GameManager.Instance.Player.transform);
        Destroy(instance, 4f);
    }
}
