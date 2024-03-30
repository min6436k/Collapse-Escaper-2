using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public Transform Target;

    private void Start()
    {
        transform.SetParent(GameManager.Instance.Player.transform);
        transform.localPosition = new Vector3(0,0.1f,-0.15f);
        Destroy(gameObject,7f);
    }
    void Update()
    {
        transform.LookAt(Target);
    }
}
