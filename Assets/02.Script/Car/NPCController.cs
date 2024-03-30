using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private CarMoveSystem _carMoveSystem;
    private CarInfo _carInfo;

    void Start()
    {
        _carMoveSystem = GetComponent<CarMoveSystem>();
        _carInfo = GetComponent<CarInfo>();

        Invoke("DestroyCar",10f);
    }

    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        Vector3 target;

        if(Vector3.Distance(GameManager.Instance.Player.transform.position,transform.position) < 20)
            target = transform.InverseTransformPoint(GameManager.Instance.Player.transform.position).normalized *2;

        else target = transform.InverseTransformPoint(GameManager.Instance.WayPoints[_carInfo.CurrentWayIndex].position).normalized;

        _carMoveSystem.Move(1, target.x);
    }

    void DestroyCar()
    {
        if (GetComponent<Animator>().GetBool("Destroy")) return;
        gameObject.layer = 6;
        var temp = GetComponentInChildren<WheelCollider>().suspensionSpring;
        temp.spring = 0;
        foreach (WheelCollider i in GetComponentsInChildren<WheelCollider>()) i.suspensionSpring = temp;

        GetComponent<Animator>().SetBool("Destroy",true);
        Destroy(gameObject,2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("DestroyCar", 1f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Invoke("DestroyCar", 0.5f);
        }
    }
}
