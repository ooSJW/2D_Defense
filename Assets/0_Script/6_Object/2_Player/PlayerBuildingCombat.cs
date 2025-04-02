using System;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBulletData;

public partial class PlayerBuildingCombat : MonoBehaviour // Data Field
{
    private PlayerBuilding playerBuilding;

    public List<Transform> enemyTransformList;

    private float timer;
    private float attackDelay;

    private PlayerBulletInformation playerBulletInformation;
}

public partial class PlayerBuildingCombat : MonoBehaviour // Data Property
{
    private Transform target;
    public Transform Target
    {
        get => target;
        private set
        {
            if (playerBuilding.IsPlacing == false)
                target = value;
            else
                target = null;
        }

    }
}
public partial class PlayerBuildingCombat : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        enemyTransformList = new List<Transform>();

        string usableBulletName = playerBuilding.PlayerBuildingInformation.usable_bullet_name;
        if (Enum.TryParse<BulletName>(usableBulletName, true, out BulletName bulletName))
            playerBulletInformation = MainSystem.Instance.DataManager.PlayerBulletData.GetData(bulletName);
        else
            Debug.LogWarning($"Bullet name Error [{usableBulletName}]");

        UpdateAttackDelay();
    }
    public void Initialize(PlayerBuilding playerBuildingValue)
    {
        playerBuilding = playerBuildingValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class PlayerBuildingCombat : MonoBehaviour // Main
{
    public void Progress()
    {
        Attack();
    }
}

public partial class PlayerBuildingCombat : MonoBehaviour // Property
{
    private void Attack()
    {
        if (enemyTransformList.Count > 0)
            Target = enemyTransformList[0];
        else
            Target = null;

        if (Target != null)
        {
            timer += Time.deltaTime;
            if (timer >= attackDelay)
            {
                playerBuilding.PlayerBuildingAnimation.AttackTrigger();
                // TODO TEST
                PlayerBullet bullet = MainSystem.Instance.PoolManager.Spawn(playerBulletInformation.name, null, Target.position).GetComponent<PlayerBullet>();
                bullet.Initialize(playerBulletInformation, playerBuilding.PlayerBuildingInformation.damage_array[playerBuilding.CurrentIndex]);
                timer = 0;
            }
        }
    }

    public void UpdateAttackDelay()
    {
        attackDelay = playerBuilding.PlayerBuildingInformation.attack_delay_array[playerBuilding.CurrentIndex];
    }

    public void RemoveEnemy(Transform enemyTransform)
    {
        if (enemyTransformList.Contains(enemyTransform))
            enemyTransformList.Remove(enemyTransform);
    }
}