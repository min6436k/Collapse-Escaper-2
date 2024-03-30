using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform Target;
    public Vector3 Interval;
    void Update()
    {
        transform.position = Target.position + Interval;

        Vector3 temp = Target.position;
        temp.y = Interval.y;

        transform.LookAt(temp + Target.forward);
    }
}
