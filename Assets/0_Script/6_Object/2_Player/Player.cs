using UnityEngine;
using static PlayerData;

public partial class Player : MonoBehaviour // Data Field
{
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; } = null;
}

public partial class Player : MonoBehaviour // Data Property
{
    private PlayerInformation playerInformation;
    public PlayerInformation PlayerInformation
    {
        get => playerInformation;
        private set
        {
            playerInformation = new PlayerInformation()
            {
                index = value.index,
                level = value.level,
                max_exp = value.max_exp,
                max_level = value.max_level,
                unlocked_building_array = value.unlocked_building_array,
            };
        }
    }
}


public partial class Player : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        PlayerInput.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Player : MonoBehaviour // 
{

}