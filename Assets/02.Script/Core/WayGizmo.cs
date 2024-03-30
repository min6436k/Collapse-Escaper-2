using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayGizmo : MonoBehaviour
{
    public Transform WayPointParent;


    private void OnDrawGizmos()
    {
        foreach (Transform item in WayPointParent)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(item.position,20);

            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(item.position,10);
        }
    }
}
