using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static PlayerBulletData;

public partial class PlayerBullet : MonoBehaviour // Data Field
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private LayerMask enemyLayer;

    private PlayerBulletInformation playerBulletInformation;
    private List<Enemy> hitEnemyList;
    private int damage;
}
public partial class PlayerBullet : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        hitEnemyList = new List<Enemy>();
    }
    public void Initialize(PlayerBulletInformation bulletInfo, int ownerDamage)
    {
        playerBulletInformation = bulletInfo;
        damage = ownerDamage;
        Allocate();
        Setup();

        PlayEffectWithDamage();
    }
    private void Setup()
    {

    }
}
public partial class PlayerBullet : MonoBehaviour // Main
{
    private void OnParticleSystemStopped()
    {
        MainSystem.Instance.PoolManager.Despawn(gameObject);
    }
}

public partial class PlayerBullet : MonoBehaviour // Property
{
    public void PlayEffectWithDamage()
    {
        hitEnemyList.Clear();

        Vector2 boxSize = new Vector2(playerBulletInformation.damage_range, 1f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                hitEnemyList.Add(enemy);

            if (hitEnemyList.Count >= playerBulletInformation.max_hit_object_count)
                break;
        }
        particle.Play();

        for (int i = 0; i < hitEnemyList.Count; i++)
            hitEnemyList[i].GetDamage(damage);
    }
}