using System;
using UnityEngine;
using static EnemyData;

public partial class Enemy : MonoBehaviour // Event
{
    public static event Action<Transform> OnEnemyDeath;
}

public partial class Enemy : MonoBehaviour // Data Property
{
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; } = null;
    [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; } = null;

    private Vector3 targetPosition;
    public Vector3 TargetPosition { get => targetPosition; private set => targetPosition = value; }

    private EnemyInformation enemyInformation;
    public EnemyInformation EnemyInformation
    {
        get => enemyInformation;
        private set
        {
            enemyInformation = new EnemyInformation()
            {
                index = value.index,
                name = value.name,
                move_speed = value.move_speed,
                max_hp = value.max_hp,
                drop_exp = value.drop_exp,
                drop_coin = value.drop_coin,
            };
            maxHp = value.max_hp;
            hp = value.max_hp;
        }
    }

    private int maxHp;
    private int hp;

}
public partial class Enemy : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        TargetPosition = MainSystem.Instance.SceneManager.ActiveScene.EnemyTarget.transform.position;

        if (Enum.TryParse<EnemyName>(name, true, out EnemyName enemyName))
            enemyInformation = MainSystem.Instance.DataManager.EnemyData.GetData(enemyName);
        else
            Debug.LogWarning($"Enemy Name Parse Error [Name : {name}]");
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        EnemyMovement.Initialize(this);
        EnemyAnimation.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Enemy : MonoBehaviour // Main
{
    private void Update()
    {
        EnemyMovement.Progress();
    }
    private void LateUpdate()
    {
        EnemyAnimation.LateProgress();
    }
}

public partial class Enemy : MonoBehaviour // Property
{
    public void GetDamage(int damage)
    {
        print($"Damage [{damage}]");
        hp -= damage;
    }
    private void Death()
    {
        OnEnemyDeath?.Invoke(transform);
        MainSystem.Instance.EnemyManager.SigndownEnemy(this);
        MainSystem.Instance.PoolManager.DespawnEnemy(this);
    }
}