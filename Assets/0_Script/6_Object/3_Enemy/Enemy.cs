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
            if (enemyInformation == null || enemyInformation.name != value.name)
                enemyInformation = new EnemyInformation()
                {
                    index = value.index,
                    name = value.name,
                    move_speed = value.move_speed,
                    max_hp = value.max_hp,
                    drop_exp = value.drop_exp,
                    drop_coin = value.drop_coin,
                    damage = value.damage,
                };
            maxHp = value.max_hp;
            hp = value.max_hp;
        }
    }

    private int maxHp;

    private int hp;
    public int Hp
    {
        get => hp;
        private set
        {
            if (value > 0)
                hp = value;
            else
            {
                hp = 0;
                EnemyState = EnemyState.Die;
            }
        }
    }

    private EnemyState enemyState;
    public EnemyState EnemyState
    {
        get => enemyState;
        private set
        {
            if (enemyState != value)
            {
                enemyState = value;
                if (enemyState == EnemyState.Die)
                {
                    isAlive = false;
                    DropReward();
                    MainSystem.Instance.SoundManager.SoundController.PlaySoundEffect(deathSound);
                    EnemyAnimation.SetAnimation();
                    Death();
                }
            }
        }
    }

    private bool isAlive;
    [SerializeField] private SoundEffectName deathSound;
}
public partial class Enemy : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        TargetPosition = MainSystem.Instance.SceneManager.ActiveScene.EnemyTarget.transform.position;

        if (Enum.TryParse<EnemyName>(name, true, out EnemyName enemyName))
            EnemyInformation = MainSystem.Instance.DataManager.EnemyData.GetData(enemyName);
        else
            Debug.LogWarning($"Enemy Name Parse Error [Name : {name}]");

        gameObject.layer = LayerMask.NameToLayer(EnemyLayer.Enemy.ToString());
        enemyState = EnemyState.Walk;
        isAlive = true;
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
        if (isAlive)
        {
            EnemyMovement.Progress();
        }

    }
    private void LateUpdate()
    {
        if (isAlive)
        {
            EnemyAnimation.LateProgress();
        }
    }
}

public partial class Enemy : MonoBehaviour // Property
{
    public void GetDamage(int damage)
    {
        Hp -= damage;
    }
    private void DropReward()
    {
        int dropCoin, dropExp;
        dropCoin = EnemyInformation.drop_coin;
        dropExp = EnemyInformation.drop_exp;

        MainSystem.Instance.StageManager.GetCoin(dropCoin);
        MainSystem.Instance.PlayerManager.GetReawrd(0, dropExp);
    }
    public void Death()
    {
        gameObject.layer = LayerMask.NameToLayer(EnemyLayer.DeathEnemy.ToString());
        OnEnemyDeath?.Invoke(transform);
        MainSystem.Instance.EnemyManager.SigndownEnemy(this);
    }

    public void DespawnSelf()
    {
        MainSystem.Instance.PoolManager.DespawnEnemy(this);
    }
}