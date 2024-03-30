using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private CarInfo _carInfo;
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineVirtualCamera>();
        _carInfo = GetComponent<CarInfo>();
    }

    void Update()
    {
        _camera.m_Lens.FieldOfView = Mathf.Lerp(_camera.m_Lens.FieldOfView,Mathf.Clamp(60+_carInfo.SpeedPerHour*0.2f,60,100),Time.deltaTime);
    }
}
 