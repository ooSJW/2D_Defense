using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class PoolManager : MonoBehaviour // Data Field
{
    private Dictionary<string, Pool> poolDict;
}
public partial class PoolManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        poolDict = new Dictionary<string, Pool>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class PoolManager : MonoBehaviour // Property
{
    public void Register()
    {
        poolDict.Clear();
        List<GameObject> poolableObjectList = MainSystem.Instance.SceneManager.ActiveScene.poolableObjectList;

        for (int i = 0; i < poolableObjectList.Count; i++)
        {
            Pool pool = new Pool(poolableObjectList[i]);
            pool.Register();
            poolDict.Add(poolableObjectList[i].name, pool);
        }
    }

    public GameObject Spawn(string poolObjectName, Transform activeParent = null, Vector3 spawnPosition = default)
    {
        if (poolDict.ContainsKey(poolObjectName))
            return poolDict[poolObjectName].Spawn(activeParent, spawnPosition);
        else
        {
            Debug.LogWarning($"Spawn Error [name : {poolObjectName}]");
            return null;
        }
    }

    public void Despawn(GameObject despawnObject)
    {
        if (poolDict.ContainsKey(despawnObject.name))
            poolDict[despawnObject.name].Despawn(despawnObject);
        else
            Debug.LogWarning($"Despawn Error [name : {despawnObject.name}]");
    }
}


public partial class PoolManager : MonoBehaviour // Inner class
{
    public class Pool
    {
        private List<GameObject> poolList;
        private GameObject originPrefab;
        private Transform parent;
        private int spawnCount;

        public Pool(GameObject prefabValue, int spawnCountValue = 10)
        {
            poolList = new List<GameObject>();
            originPrefab = prefabValue;
            spawnCount = spawnCountValue;

            parent = new GameObject() { name = $"[Pool] : {originPrefab.name}" }.transform;
        }

        public void Register()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject newObject = Instantiate(originPrefab, parent.position, Quaternion.identity, parent);
                newObject.name = originPrefab.name;
                newObject.SetActive(false);
                poolList.Add(newObject);
            }
        }
        public GameObject Spawn(Transform activeParent = null, Vector3 spawnPosition = default)
        {
            GameObject poolObject;
            if (poolList.Count > 0)
            {
                poolObject = poolList[0];
                poolList.Remove(poolObject);
                poolObject.transform.SetParent(activeParent);
                poolObject.transform.position = spawnPosition;
                poolObject.SetActive(true);
            }
            else
            {
                poolObject = Instantiate(originPrefab, spawnPosition, Quaternion.identity, activeParent);
                poolObject.name = originPrefab.name;
            }
            return poolObject;
        }
        public void Despawn(GameObject despawnObject)
        {
            despawnObject.transform.SetParent(parent);
            despawnObject.SetActive(false);
            poolList.Add(despawnObject);
        }
    }
}