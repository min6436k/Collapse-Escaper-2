using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float StartTime;
    public float BestTime = 0;

    public float LastTime;
    public void StartRecord()
    {
        StartTime = Time.time;
    }

    public void BestTimeCheak()
    {
        float temp = Time.time - LastTime;
        if (temp < BestTime)
        {
            BestTime = temp;
        }
        else if(BestTime == 0)
        {
            BestTime = temp - StartTime;
        }

        LastTime = Time.time;
    }
}
