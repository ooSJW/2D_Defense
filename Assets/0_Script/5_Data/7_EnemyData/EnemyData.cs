using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public partial class EnemyData  // Information
{
    [System.Serializable]
    public class EnemyInformation : BaseInformation
    {
        public string name;
        public float move_speed;
        public int max_hp;
        public int drop_exp;
        public int drop_coin;
    }
}

public partial class EnemyData  // Data Field
{
    private Dictionary<string, EnemyInformation> enemyInformationDict;
}

public partial class EnemyData  // Initialize
{
    private void Allocate()
    {
        enemyInformationDict = new Dictionary<string, EnemyInformation>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.DataManager.LoadCsv<EnemyInformation>("EnemyData", enemyInformationDict);
    }
}
public partial class EnemyData  // Property
{
    public EnemyInformation GetData(EnemyName enemyName)
    {
        return enemyInformationDict.FirstOrDefault(elem => elem.Value.name == enemyName.ToString()).Value;
    }
}