using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private bool resultsChecked = false;


    // Update is called once per frame
    void Update()
    {
        // If any of the three rows are not stopped, set values to 0/false
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }
        else if (!resultsChecked)
        {
            // rows have stopped, but results have not been checked yet
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize: " + prizeValue;
        }
    }

    private void OnMouseDown()
    {
        // If all rows are stopped, and mouse is clicked, then start spinning rows
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");
        }
    }

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

        resultsChecked = true;
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
}
