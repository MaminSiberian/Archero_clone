using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
