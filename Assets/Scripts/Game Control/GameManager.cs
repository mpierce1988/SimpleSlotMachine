using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    public event Action<int> OnCurrentCashAmountChange;

    [SerializeField]
    private int initialCashAmount = 1000;

    [SerializeField]
    private int costPerPlay = 100;

    private int currentCashAmount;

    public int CurrentCashAmount => currentCashAmount;

    private void Start()
    {
        Reset();
        Debug.Log("Initial Cash Amount: " + currentCashAmount);
    }

    public void Reset()
    {
        currentCashAmount = initialCashAmount;
        OnCurrentCashAmountChange?.Invoke(currentCashAmount);
    }

    public bool CanPlayRound()
    {
        if (currentCashAmount >= costPerPlay)
        {
            return true;
        }

        return false;
    }

    public void PlayRound()
    {
        currentCashAmount -= costPerPlay;
        OnCurrentCashAmountChange?.Invoke(currentCashAmount);
        Debug.Log("Cash amount after playing round: " + currentCashAmount);
    }

    public void WinPrize(int prizeValue)
    {
        currentCashAmount += prizeValue;
        OnCurrentCashAmountChange?.Invoke(currentCashAmount);
        Debug.Log("Cash Amount after winning prize: " + currentCashAmount);
    }
}
