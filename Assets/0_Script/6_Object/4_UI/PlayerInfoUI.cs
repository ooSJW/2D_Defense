using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class PlayerInfoUI : MonoBehaviour // Data Field
{
    [SerializeField] private Image playerExpFillImage;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerExpText;
    [SerializeField] private TextMeshProUGUI playerCoinText;
}
public partial class PlayerInfoUI : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        RefreshPlayerInfoUI();
    }
    private void Setup()
    {

    }
}
public partial class PlayerInfoUI : MonoBehaviour // Property
{
    public void RefreshPlayerInfoUI()
    {
        Player player = MainSystem.Instance.PlayerManager.Player;

        playerLevelText.text = $"Lv : {player.Level}";
        int maxExp, currentExp;
        maxExp = player.MaxExp;
        currentExp = player.Exp;
        float fillAmount = maxExp > 0 ? (float)currentExp / maxExp : 0f;

        playerExpFillImage.fillAmount = fillAmount;
        playerExpText.text = $"{(fillAmount * 100):0.0}";
        playerCoinText.text = MainSystem.Instance.PlayerManager.Coin.ToString();
    }
}