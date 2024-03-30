using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CarMoveSystem _carMoveSystem;
    private Effect _effect;
    void Start()
    {
        _carMoveSystem = GetComponent<CarMoveSystem>();
        _effect = GetComponent<Effect>();
        UpdateEngineUpgrade();
    }

    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        float motor = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        bool brake = Input.GetKey(KeyCode.Space);
        bool drift = Input.GetKey(KeyCode.LeftShift);

        if (GameInstance.Instance.PartsLevel[0] < GameInstance.Instance.CurrentStage)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,-transform.up,out hit, 10,LayerMask.GetMask("Ground")))
            {
                if (hit.collider.gameObject.CompareTag("Slow"))
                {
                    motor *= 0.6f;
                    _effect.Slow(true);
                }
                else
                {
                    _effect.Slow(false);
                }
            }
        }

        _carMoveSystem.Move(motor, steer, brake, drift);
    }

    public void UpdateEngineUpgrade()
    {
        float motorSpeed;
        switch (GameInstance.Instance.PartsLevel[1])
        {
            case 1: motorSpeed = 620; break;
            case 2: motorSpeed = 750; break;
            default: motorSpeed = 500; break;
        }
        _carMoveSystem.MaxMotor = motorSpeed;

        _effect.UpdateEngineUpgrade();
    }

    public void BoostItem(float force)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
