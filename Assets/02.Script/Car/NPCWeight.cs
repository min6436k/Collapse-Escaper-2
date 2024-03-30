using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWeight : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") ||
            (other.CompareTag("Player") && GameInstance.Instance.PartsLevel[2] == 1))
        {
            GetComponentInParent<Rigidbody>().mass = 50;
        }

    }
}
