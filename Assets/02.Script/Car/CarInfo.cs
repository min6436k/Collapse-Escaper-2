using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    public int Lap = 1;
    public float SpeedPerHour;
    public int CurrentWayIndex = 0;
    public float WayDistance = 20;
    public bool IsFinish = false;
    public bool IsReverse = false;

    private Rigidbody _rigid;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        SpeedPerHour = _rigid.velocity.magnitude * 4.5f;

        if (Vector3.Distance(GameManager.Instance.WayPoints[CurrentWayIndex].position, transform.position) < WayDistance)
        {
            CurrentWayIndex = OutIndex(CurrentWayIndex + (IsReverse ? -1 : 1));
        }
    }


    public int OutIndex(int index)
    {
        if (index >= GameManager.Instance.MaxWayIndex)
        {
            IsFinish = true;
            index -= GameManager.Instance.MaxWayIndex;
        }
        if (index < 0) index = GameManager.Instance.MaxWayIndex + index;


        return index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFinish && other.CompareTag("FinishLine"))
        {

            var gm = GameManager.Instance;

            if (gameObject.CompareTag("Player"))
            {
                IsFinish = false;

                gm.GetComponentInChildren<ItemManager>().SpawnItem();
                gm.GetComponentInChildren<TimeManager>().BestTimeCheak();

                if (Lap >= gm.StageMaxLap) gm.GameClear();
                else  Lap++;
            }

            if (gameObject.CompareTag("Enemy"))
            {
                IsFinish = false;

                if (Lap >= gm.StageMaxLap) gm.GameOver();
                else Lap++;
            }
            
        }
    }
}
