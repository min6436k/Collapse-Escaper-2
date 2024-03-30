using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public string Name;

    private Animator _animator;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }
    public virtual void GetItem()
    {
        GameManager.Instance.GetComponentInChildren<InGameUI>().GetItem(Name);
        _audioSource.Play();
        _animator.SetTrigger("Get");
        Destroy(gameObject,0.21f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
        }
    }
}
public class ItemManager : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();
    public List<GameObject> Coins = new List<GameObject>();

    public List<GameObject> CurrentSpawnItems = new List<GameObject>();

    private void Start()
    {
        SpawnItem();
    }
    public void SpawnItem()
    {
        foreach (GameObject item in CurrentSpawnItems) Destroy(item);
        CurrentSpawnItems.RemoveAll(x => x == null);

        foreach (var Point in GameManager.Instance.WayPoints)
        {
            if(Random.Range(0,5) == 0)
            {
                for(int i = -1; i < 2; i++)
                {
                    GameObject instance;
                    if (Random.Range(0, Items.Count + 1) == Items.Count)
                         instance = Instantiate(Coins[Random.Range(0, Coins.Count)], Point);

                    else instance = Instantiate(Items[Random.Range(0, Items.Count)], Point);

                    instance.transform.localPosition += new Vector3(i * 3, 1.5f, 0);

                    CurrentSpawnItems.Add(instance);
                }

            }
        }
    }
}
