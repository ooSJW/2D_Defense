using UnityEngine;

public partial class PlayerManager : MonoBehaviour // Data Field
{
    public Player Player { get; private set; } = null;
}
public partial class PlayerManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {

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
public partial class PlayerManager : MonoBehaviour // Sign
{
    public void SignupPlayer(Player player)
    {
        Player = player;
        Player.Initialize();
    }
    public void SigndownPlayer()
    {
        Player = null;
    }
}