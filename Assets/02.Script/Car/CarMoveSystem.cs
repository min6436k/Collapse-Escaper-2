using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class WheelInfo
{
    public string Name;

    public WheelCollider Left;
    public WheelCollider Right;

    public bool Motor;
    public bool Steer;
}
public class CarMoveSystem : MonoBehaviour
{
    public List<WheelInfo> Wheels = new List<WheelInfo>();
    public float MaxMotor;
    public float MaxSteer;

    private WheelFrictionCurve stiffness;

    private void Start()
    {
        stiffness = Wheels[0].Left.sidewaysFriction;
    }

    public void Move(float Motor, float Steer, bool Brake =false, bool Drift = false)
    {
        if (GameManager.Instance.bCarMove == false) Brake = true;

        foreach (WheelInfo wheel in Wheels)
        {
            if (wheel.Motor)
            {
                wheel.Left.motorTorque = Motor * MaxMotor;
                wheel.Right.motorTorque = Motor * MaxMotor;
            }

            if (wheel.Steer)
            {
                wheel.Left.steerAngle = Steer * MaxSteer;
                wheel.Right.steerAngle = Steer * MaxSteer;
            }

            if (Brake)
            {
                wheel.Left.brakeTorque = 1000000;
                wheel.Right.brakeTorque = 1000000;
            }
            else
            {
                wheel.Left.brakeTorque = 0;
                wheel.Right.brakeTorque = 0;
            }

            WheelPos(wheel.Left);
            WheelPos(wheel.Right);
        }


        if (Drift) stiffness.stiffness = 2;
        else stiffness.stiffness = 5;

        Wheels[1].Left.sidewaysFriction = stiffness;
        Wheels[1].Right.sidewaysFriction = stiffness;
    }

    void WheelPos(WheelCollider wheel)
    {
        wheel.GetWorldPose(out Vector3 Pos, out Quaternion rot);

        Transform temp = wheel.transform.GetChild(0);
        temp.position = Pos;
        temp.rotation = rot;
    }
}
