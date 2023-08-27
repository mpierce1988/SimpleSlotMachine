using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private TextMeshProUGUI prizeText;

    [SerializeField]
    private List<Row> rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private bool resultsChecked = false;

    private IInput input;

    private void Awake()
    {
        input = GetComponent<IInput>();
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
            CheckResults();
            SetPrizeText();
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

    #endregion

    #region Scoring
    // Scoring
    private void CheckResults()
    {
        if (IsThreeMatch("Diamond"))
        {
            prizeValue = 200;
        }
        else if (IsThreeMatch("Crown"))
        {
            prizeValue = 600;
        }
        else if (IsThreeMatch("Melon"))
        {
            prizeValue = 1000;
        }
        else if (IsThreeMatch("Bar"))
        {
            prizeValue = 1500;
        }
        else if (IsThreeMatch("Seven"))
        {
            prizeValue = 2000;
        }
        else if (IsThreeMatch("Cherry"))
        {
            prizeValue = 4000;
        }
        else if (IsThreeMatch("Lemon"))
        {
            prizeValue = 8000;
        }
        else if (IsDoubleMatch("Diamond"))
        {
            prizeValue = 100;
        }
        else if (IsDoubleMatch("Crown"))
        {
            prizeValue = 300;
        }
        else if (IsDoubleMatch("Melon"))
        {
            prizeValue = 500;
        }
        else if (IsDoubleMatch("Bar"))
        {
            prizeValue = 700;
        }
        else if (IsDoubleMatch("Seven"))
        {
            prizeValue = 1000;
        }
        else if (IsDoubleMatch("Cherry"))
        {
            prizeValue = 2000;
        }
        else if (IsDoubleMatch("Lemon"))
        {
            prizeValue = 4000;
        }

    }


    private bool IsDoubleMatch(string slotType)
    {
        return (rows[0].stoppedSlot == slotType && rows[1].stoppedSlot == slotType)
            || (rows[1].stoppedSlot == slotType && rows[2].stoppedSlot == slotType)
            || (rows[0].stoppedSlot == slotType && rows[2].stoppedSlot == slotType);
    }

    private bool IsThreeMatch(string slotType)
    {
        return rows[0].stoppedSlot == slotType && rows[1].stoppedSlot == slotType && rows[2].stoppedSlot == slotType;
    }

    #endregion

    #region Text Display

    // Text display
    private void SetPrizeText()
    {
        prizeText.enabled = true;
        prizeText.text = "Prize: " + prizeValue;
    }

    private void ResetPrizeValues()
    {
        prizeValue = 0;
        prizeText.enabled = false;
        resultsChecked = false;
    }

    #endregion
}
