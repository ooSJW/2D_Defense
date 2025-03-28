using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class PoolManager : MonoBehaviour // Data Field
{
    private Dictionary<string, Pool> poolDict;
    private Dictionary<string, EnemyPool> enemyPoolDict;
}
public partial class PoolManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        poolDict = new Dictionary<string, Pool>();
        enemyPoolDict = new Dictionary<string, EnemyPool>();
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

        enemyPoolDict.Clear();
        List<Enemy> poolableEnemyList = MainSystem.Instance.SceneManager.ActiveScene.poolableEnemyList;
        for (int i = 0; i < poolableEnemyList.Count; i++)
        {
            EnemyPool enemyPool = new EnemyPool(poolableEnemyList[i].gameObject);
            enemyPool.Register();
            enemyPoolDict.Add(poolableEnemyList[i].name, enemyPool);
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

    public Enemy SpawnEnemy(string poolObjectName, Transform activeParent = null, Vector3 spawnPosition = default)
    {
        if (enemyPoolDict.ContainsKey(poolObjectName))
            return enemyPoolDict[poolObjectName].SpawnEnemy(activeParent, spawnPosition);
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

    public void DespawnEnemy(Enemy despawnenemy)
    {
        if (enemyPoolDict.ContainsKey(despawnenemy.name))
            enemyPoolDict[despawnenemy.name].DespawnEnemy(despawnenemy);
        else
            Debug.LogWarning($"Despawn Error [name : {despawnenemy.name}]");
    }
}


public partial class PoolManager : MonoBehaviour // Inner class
{
    public class Pool
    {
        protected Queue<GameObject> poolQueue;
        protected GameObject originPrefab;
        protected Transform parent;
        protected int spawnCount;

        public Pool(GameObject prefabValue, int spawnCountValue = 10)
        {
            poolQueue = new Queue<GameObject>();
            originPrefab = prefabValue;
            spawnCount = spawnCountValue;

            parent = new GameObject() { name = $"[Pool] : {originPrefab.name}" }.transform;
        }

        public virtual void Register()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject newObject = Instantiate(originPrefab, parent.position, Quaternion.identity, parent);
                newObject.name = originPrefab.name;
                newObject.SetActive(false);
                poolQueue.Enqueue(newObject);
            }
        }
        public GameObject Spawn(Transform activeParent = null, Vector3 spawnPosition = default)
        {
            GameObject poolObject;
            if (poolQueue.Count > 0)
            {
                poolObject = poolQueue.Dequeue();
                poolObject.transform.SetParent(activeParent);
                poolObject.transform.position = spawnPosition;
                poolObject.SetActive(true);
                print("IF");
            }
            else
            {
                poolObject = Instantiate(originPrefab, spawnPosition, Quaternion.identity, activeParent);
                poolObject.name = originPrefab.name;
                print("else");
            }
            return poolObject;
        }
        public void Despawn(GameObject despawnObject)
        {
            despawnObject.transform.SetParent(parent);
            despawnObject.SetActive(false);
            poolQueue.Enqueue(despawnObject);
        }
    }

    public class EnemyPool : Pool
    {
        private Queue<Enemy> enemyPoolQueue;
        public EnemyPool(GameObject originPrefabValue, int initlaiCount = 10) : base(originPrefabValue, initlaiCount)
        {
            enemyPoolQueue = new Queue<Enemy>();
        }

        public override void Register()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject newObject = Instantiate(originPrefab, parent.position, Quaternion.identity, parent);
                newObject.name = originPrefab.name;
                newObject.SetActive(false);
                Enemy enemy = newObject.GetComponent<Enemy>();
                enemyPoolQueue.Enqueue(enemy);
            }
        }

        public Enemy SpawnEnemy(Transform activeParent = null, Vector3 spawnPosition = default)
        {
            Enemy poolEnemy;
            if (enemyPoolQueue.Count > 0)
            {
                poolEnemy = enemyPoolQueue.Dequeue();
                poolEnemy.transform.SetParent(activeParent);
                poolEnemy.transform.position = spawnPosition;
                poolEnemy.gameObject.SetActive(true);
            }
            else
            {
                poolEnemy = Instantiate(originPrefab, spawnPosition, Quaternion.identity, activeParent).GetComponent<Enemy>();
                poolEnemy.gameObject.name = originPrefab.name;
            }
            return poolEnemy;
        }

        public void DespawnEnemy(Enemy despawnEnemy)
        {
            despawnEnemy.transform.SetParent(parent);
            despawnEnemy.gameObject.SetActive(false);
            enemyPoolQueue.Enqueue(despawnEnemy);
        }
    }
}