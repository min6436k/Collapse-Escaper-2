using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnLate = 2.5f;
    private List<GameObject> _CurrentNPC = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating("Spawn",5f, SpawnLate);
    }
    public void Spawn()
    {
        _CurrentNPC.RemoveAll(x => x == null);

        if (_CurrentNPC.Count >= 3) return;

        int PlayerWayIndex = GameManager.Instance.Player.GetComponent<CarInfo>().CurrentWayIndex;

        var temp = GameManager.Instance.Player.GetComponent<CarInfo>();
        bool IsReverce = System.Convert.ToBoolean(Random.Range(0, 2));
        Vector3 pos = GameManager.Instance.WayPoints[temp.OutIndex(PlayerWayIndex + (IsReverce ? 4 : -2))].position  + Vector3.up;
        Quaternion dir = Quaternion.LookRotation(GameManager.Instance.WayPoints[temp.OutIndex(PlayerWayIndex + (IsReverce ? 3 : -1))].position-pos);

        CarInfo instance = Instantiate(Prefab, pos, dir).GetComponent<CarInfo>();

        instance.GetComponent<CarMoveSystem>().MaxMotor = GameManager.Instance.Player.GetComponent<CarMoveSystem>().MaxMotor + 50;

        instance.IsReverse = IsReverce;

        instance.GetComponent<Rigidbody>().AddForce(10000*instance.transform.forward, ForceMode.Impulse);

        instance.CurrentWayIndex = instance.OutIndex(PlayerWayIndex + (IsReverce ? 3 : -1));

        _CurrentNPC.Add(instance.gameObject);
    }
}
