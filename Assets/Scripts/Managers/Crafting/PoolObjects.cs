using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    public GameObject woodPrefab;
    public GameObject stonePrefab;
    public GameObject plasticPrefab;
    public GameObject ironPrefab;

    public Transform woodPool;
    public Transform stonePool;
    public Transform plasticPool;
    public Transform ironPool;

    private void Start()
    {
        ItemManager.Instance.Initiate();
        ObjectPoolManager.Instance.Initiate();
    }
}
