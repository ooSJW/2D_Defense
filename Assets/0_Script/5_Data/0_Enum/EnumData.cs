using UnityEngine;

public enum SceneName
{
    None,
    InitializeScene,
    LoadingScene,
    LobbyScene,
    Stage00Scene,
    Stage01Scene,
    Stage02Scene,
    Stage03Scene,
    Stage04Scene,
}

public enum SceneType
{
    Initialize,
    NotCombat,
    Combat,
}

public enum TagType
{
    Default,
    Structable,
    UnStructable,
    Enemy,
}
public enum BuildingName
{
    None,
    SmallTankA,
    SmallTankB,
    SmallTankC,
    MediumTankA,
    MediumTankB,
    MediumTankC
}

public enum BulletType
{
    MachineGun,
    Cannon,
}
public enum BulletName
{
    BulletA,
    BulletB,
    BulletC,
    CannonA,
    CannonB,
    CannonC,
}

public enum EnemyName
{
    Knight,
    Merchant,
    Peasant,
    Priest,
    Soldier,
    Thief,
}
public enum EnemyState
{
    Walk,
    Die,
}

public enum EnemyLayer
{
    Enemy,
    DeathEnemy,
}