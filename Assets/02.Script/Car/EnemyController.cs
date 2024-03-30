using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CarMoveSystem _carMoveSystem;
    private CarInfo _carInfo;

    private float _returnTime = 0;
    void Start()
    {
        _carMoveSystem = GetComponent<CarMoveSystem>();
        _carInfo = GetComponent<CarInfo>();
    }

    void Update()
    {
        Vector3 target = transform.InverseTransformPoint(GameManager.Instance.WayPoints[_carInfo.CurrentWayIndex].position).normalized;

        _carMoveSystem.Move(1, target.x);

        Return();
    }

    void Return()
    {
        if(_carInfo.SpeedPerHour < 5 && GameManager.Instance.bCarMove)
        {
            _returnTime += Time.deltaTime;

            if(_returnTime > 3)
            {
                transform.position = GameManager.Instance.WayPoints[_carInfo.OutIndex(_carInfo.CurrentWayIndex - 1)].position+Vector3.up;
                transform.LookAt(GameManager.Instance.WayPoints[_carInfo.OutIndex(_carInfo.CurrentWayIndex)]);
                _returnTime = 0;

            }
        }
        else _returnTime = 0;
    }
}
