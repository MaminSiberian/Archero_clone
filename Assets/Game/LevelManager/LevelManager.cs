using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int coinsValue {  get; private set; }

    public static event Action<int> OnCoinValueChangedEvent;

    private void OnEnable()
    {
        Coin.OnCoinCollectedEvent += OnCoinCollected;
    }
    private void OnDisable()
    {
        Coin.OnCoinCollectedEvent -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        coinsValue++;

        OnCoinValueChangedEvent?.Invoke(coinsValue);
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
