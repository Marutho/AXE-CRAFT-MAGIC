using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Transform parent;
        public int size;

        public Pool(string t, GameObject pref, Transform par, int s)
        {
            tag = t;
            prefab = pref;
            parent = par;
            size = s;
        }
    }

    public PoolObjects objectPools;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    protected ObjectPoolManager() { }

    public void Initiate()
    {
        //Does nothing.
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPools = GameObject.FindGameObjectWithTag("ObjectPool").GetComponent<PoolObjects>();

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        pools = new List<Pool>();

        GeneratePools();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }
    }

    void GeneratePools()
    {
        pools.Add(new Pool("i_Wood", objectPools.woodPrefab, objectPools.woodPool, 50));
        pools.Add(new Pool("i_Stone", objectPools.stonePrefab, objectPools.stonePool, 50));
        pools.Add(new Pool("i_Plastic", objectPools.plasticPrefab, objectPools.plasticPool, 50));
        pools.Add(new Pool("i_Iron", objectPools.ironPrefab, objectPools.ironPool, 50));
    }

    public void GetObjectFromPool(string pool, Vector3 pos, Quaternion rot)
    {
        GameObject objectFromPool = poolDictionary[pool].Dequeue();
        objectFromPool.transform.position = pos;
        objectFromPool.transform.rotation = rot;
        objectFromPool.SetActive(true);

        float xForce = Random.Range(-3.0f, 3.0f);
        float yForce = Random.Range(3.0f, 5.0f);
        float zForce = Random.Range(-3.0f, 3.0f);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        objectFromPool.GetComponent<Rigidbody>().velocity = force;
    }

    public void ReturnObjectToPool(GameObject obj, string pool)
    {
        obj.SetActive(false);
        poolDictionary[pool].Enqueue(obj);
        obj.transform.position = objectPools.woodPool.position;
        obj.transform.rotation = objectPools.woodPool.rotation;
        
    }

}
