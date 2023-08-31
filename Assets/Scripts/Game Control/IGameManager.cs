using System;

public interface IGameManager
{
    int CurrentCashAmount { get; }

    event Action<int> OnCurrentCashAmountChange;

    bool CanPlayRound();
    void PlayRound();
    void Reset();
    void WinPrize(int prizeValue);
}