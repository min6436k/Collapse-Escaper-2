using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource Engine;
    public float EngineVolumeValue = 0;
    public float EnginePitchValue = 0;

    public AudioSource Drift;
    public float DriftVolumeValue = 0;

    private CarInfo _carInfo;

    private void Start()
    {
        _carInfo = GetComponent<CarInfo>();
    }

    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            EngineVolumeValue = Mathf.Clamp(_carInfo.SpeedPerHour, 0, 0.8f);
            EnginePitchValue = Mathf.Clamp(1+_carInfo.SpeedPerHour/200, 1f, 1.8f);
        }
        else
        {
            EngineVolumeValue = 0;
            EnginePitchValue = 0;
        }

        Drift.volume = Mathf.Lerp(Drift.volume, DriftVolumeValue, Time.deltaTime*5);
        Engine.volume = Mathf.Lerp(Engine.volume, EngineVolumeValue, Time.deltaTime*5);
        Engine.pitch = Mathf.Lerp(Engine.pitch, EnginePitchValue, Time.deltaTime*5);
    }
}
