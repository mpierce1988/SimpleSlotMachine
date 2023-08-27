using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };
    public static event Action<int> PrizeWon;

    [SerializeField]
    private List<Row> rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private bool resultsChecked = true;

    private IInput input;
    private ICalculateScore scoreCalculator;

    private void Awake()
    {
        input = GetComponent<IInput>();
        scoreCalculator = GetComponent<ICalculateScore>();
    }

    private void Start()
    {
        if (input != null)
            input.OnClick += HandleMouseClick;
    }

    // Update is called once per frame
    void Update()
    {
        // If any of the three rows are not stopped, set values to 0/false
        if (!AllRowsStopped())
        {
            ResetPrizeValues();
        }
        else if (!resultsChecked)
        {
            // rows have stopped, but results have not been checked yet
            //CheckResults();
            prizeValue = scoreCalculator.CalculatePrize(rows);
            PrizeWon?.Invoke(prizeValue);
            //SetPrizeText();
            resultsChecked = true;
        }
    }

    private void OnDestroy()
    {
        if (input != null)
            input.OnClick -= HandleMouseClick;
    }

    private void HandleMouseClick()
    {
        // If all rows are stopped, and mouse is clicked, then start spinning rows
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
        }
    }

    #region Handle Animation and call event

    // Handle animation, call event at apex of animation
    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion

    #region Game State Checking

    // Game State
    private bool AllRowsStopped()
    {
        return rows.All(row => row.rowStopped);
    }

    private void ResetPrizeValues()
    {
        prizeValue = 0;
        resultsChecked = false;
    }

    #endregion

}
