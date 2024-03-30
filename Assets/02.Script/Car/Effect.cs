using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public List<GameObject> EngineEffects = new List<GameObject>();
    public List<ParticleSystem> BoostEffects = new List<ParticleSystem>();
    public List<ParticleSystem> WheelEffects = new List<ParticleSystem>();

    public ParticleSystem SlowEffect;

    public GameObject CrashPrefab;

    private CarInfo _carInfo;
    private PlayerSound _playerSound;

    private ParticleSystem.EmissionModule _emissionModule;

    private void Start()
    {
        _carInfo = GetComponent<CarInfo>();
        _playerSound = GetComponent<PlayerSound>();
    }
    private void Update()
    {
        WheelParticleInput();
    }

    public void UpdateEngineUpgrade()
    {
        for (int i = 0; i < EngineEffects.Count; i++)
        {
            if (GameInstance.Instance.PartsLevel[1] * 2 > i) EngineEffects[i].SetActive(true);
        }
    }

    void WheelParticleInput()
    {
        if (Input.GetKey(KeyCode.Space))
            SetWheelEmission(250);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            SetWheelEmission(250);
            _playerSound.DriftVolumeValue = 1;
        }


        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.LeftShift) || _carInfo.SpeedPerHour < 10)
        {
            SetWheelEmission(0);
            _playerSound.DriftVolumeValue = 0;
        }



    }

    public void Slow(bool b)
    {
        _emissionModule = SlowEffect.emission;
        _emissionModule.rateOverTime = b ? 2 : 0;
    }

    public void SetWheelEmission(int value)
    {
        foreach (var item in WheelEffects)
        {
            _emissionModule = item.emission;
            _emissionModule.rateOverTime = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.GetMask("Ground"))
        {
            GameObject instance = Instantiate(CrashPrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(instance, 2f);
        }
    }

}
