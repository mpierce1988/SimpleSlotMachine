using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameControl : MonoBehaviour
{
    public static event Action OnStartSpin;
    public static event Action OnHandlePulled;
    public static event Action<int> PrizeWon;

    [SerializeField]
    private List<Row> rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private IInput input;
    private ICalculateScore scoreCalculator;
    private IAnimateHandle handleAnimator;

    private void Awake()
    {
        input = GetComponent<IInput>();
        scoreCalculator = GetComponent<ICalculateScore>();
        handleAnimator = handle.GetComponent<IAnimateHandle>();
    }

    private void Start()
    {
        if (input != null)
            input.OnClick += PullHandleIfAllRowsStopped;

        if (handleAnimator != null)
        {
            handleAnimator.OnHandleAnimationApex += StartSpin;
        }

        foreach (Row row in rows)
        {
            row.OnSpinningEnd += CheckResults;
        }
    }

    private void OnDestroy()
    {
        if (input != null)
            input.OnClick -= PullHandleIfAllRowsStopped;

        if (handleAnimator != null)
        {
            handleAnimator.OnHandleAnimationApex -= StartSpin;
        }

        foreach (Row row in rows)
        {
            row.OnSpinningEnd -= CheckResults;
        }
    }

    #region Event Handlers    

    private void PullHandleIfAllRowsStopped()
    {
        // If all rows are stopped, and mouse is clicked, then start spinning rows
        if (AllRowsStopped())
        {
            handleAnimator.PullHandle();
            ResetPrizeValues();
            OnHandlePulled?.Invoke();
        }
    }

    private void StartSpin()
    {
        OnStartSpin?.Invoke();
    }

    private void CheckResults()
    {
        if (AllRowsStopped())
        {
            prizeValue = scoreCalculator.CalculatePrize(rows);
            PrizeWon?.Invoke(prizeValue);
        }
    }

    #endregion

    #region Game State

    // Game State
    private bool AllRowsStopped()
    {
        return rows.All(row => row.RowStopped);
    }

    private void ResetPrizeValues()
    {
        prizeValue = 0;
    }

    #endregion

}
