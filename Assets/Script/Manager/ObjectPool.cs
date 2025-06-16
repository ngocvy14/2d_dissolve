using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // [SerializeField] GameObject ObjectToPool;
    public PooledObject[] objectsToPool;

    [Serializable]

    public struct PooledObject
    {
        public string id;
        public GameObject gameObject;
        public int initialCount;
    }

    private Dictionary<string, Queue<GameObject>> objectPoolInStock;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (objectPoolInStock == null)
        {
            objectPoolInStock = new Dictionary<string, Queue<GameObject>>();
        }
        foreach (var PooledObject in objectsToPool)
        {
            if (!objectPoolInStock.ContainsKey(PooledObject.id))
            {
                objectPoolInStock.Add(PooledObject.id, new Queue<GameObject>());
            }
            for (var i = 0; i < PooledObject.initialCount; i++)
            {
                var obj = Instantiate(PooledObject.gameObject, transform);
                obj.SetActive(false);

                objectPoolInStock[PooledObject.id].Enqueue(obj);

            }
        }
    }

    public GameObject GetObject(string id)
    {
        if (objectPoolInStock.ContainsKey(id) && objectPoolInStock[id].Count > 0)
        {
            var obj = objectPoolInStock[id].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        var PooledObject = Array.Find(objectsToPool, o => o.id == id);
        var outPut = Instantiate(PooledObject.gameObject, transform);
        outPut.SetActive(true);
        return outPut;
    }

    public void ReturnObject(string id, GameObject obj)
    {
        if (objectPoolInStock.ContainsKey(id))
        {
            obj.SetActive(false);
            objectPoolInStock[id].Enqueue(obj);
        }
        else
        {
            objectPoolInStock.Add(id, new Queue<GameObject>());
            obj.SetActive(false);
            objectPoolInStock[id].Enqueue(obj);
        }
    }


}
