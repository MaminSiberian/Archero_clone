using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void OnEnable()
    {
        PlayerController.OnPlayerHealthChangedEvent += OnHPValueChanged;
        LevelManager.OnCoinValueChangedEvent += OnCoinsValueChanged;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerHealthChangedEvent -= OnHPValueChanged;
        LevelManager.OnCoinValueChangedEvent -= OnCoinsValueChanged;
    }
    private void Start()
    {
        HPText.text = "HP: " + FindObjectOfType<PlayerModel>().maxHP;
        coinsText.text = "Coins: " + 0;
    }
    private void OnHPValueChanged(float value)
    {
        HPText.text = "HP: " + value;
    }
    private void OnCoinsValueChanged(int value)
    {
        coinsText.text = "Coins: " + value;
    }
}
