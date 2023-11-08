using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI HPText;
        [SerializeField] private TextMeshProUGUI coinsText;

        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject levelPassedScreen;

        #region monobehs
        private void OnEnable()
        {
            PlayerController.OnPlayerHealthChangedEvent += OnHPValueChanged;
            PlayerController.OnPlayerDeathEvent += OnPlayerDeath;
            PlayerController.OnEnemiesDefeatedEvent += OnLevelFinished;
            LevelManager.OnCoinValueChangedEvent += OnCoinsValueChanged;
        }
        private void OnDisable()
        {
            PlayerController.OnPlayerHealthChangedEvent -= OnHPValueChanged;
            PlayerController.OnPlayerDeathEvent -= OnPlayerDeath;
            PlayerController.OnEnemiesDefeatedEvent -= OnLevelFinished;
            LevelManager.OnCoinValueChangedEvent -= OnCoinsValueChanged;
        }
        private void Start()
        {
            Time.timeScale = 1f;
            HPText.text = "HP: " + FindObjectOfType<PlayerModel>().maxHP;
            coinsText.text = "Coins: " + 0;
        }
        #endregion

        [Button]
        private void OnPlayerDeath()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            gameOverScreen.SetActive(true);
        }
        [Button]
        private void OnLevelFinished()
        {
            TurnOffAll();
            levelPassedScreen.SetActive(true);
            pauseButton.SetActive(true);
        }
        private void TurnOffAll()
        {
            pauseButton.SetActive(false);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            levelPassedScreen.SetActive(false);
        }

        private void OnHPValueChanged(float value)
        {
            HPText.text = "HP: " + value;
        }
        private void OnCoinsValueChanged(int value)
        {
            coinsText.text = "Coins: " + value;
        }

        #region pause
        public void PauseGame()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            pauseScreen.SetActive(true);
        }
        public void UnpauseGame()
        {
            TurnOffAll();
            pauseButton.SetActive(true);
            Time.timeScale = 1f;
        }
        #endregion
    }
}
