using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pool : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public int poolSize = 10;
    private Queue<GameObject> objectPool;

    void Awake()
    {
        objectPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
