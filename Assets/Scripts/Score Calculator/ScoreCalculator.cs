using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour, ICalculateScore
{
    private List<Row> rows;
    // Scoring
    public int CalculatePrize(List<Row> rowsToCalculate)
    {
        rows = rowsToCalculate;
        int prizeValue = 0;
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
        else
        {
            prizeValue = 0;
        }

        return prizeValue;

    }


    private bool IsDoubleMatch(string slotType)
    {
        return (rows[0].StoppedSlot == slotType && rows[1].StoppedSlot == slotType)
            || (rows[1].StoppedSlot == slotType && rows[2].StoppedSlot == slotType)
            || (rows[0].StoppedSlot == slotType && rows[2].StoppedSlot == slotType);
    }

    private bool IsThreeMatch(string slotType)
    {
        return rows[0].StoppedSlot == slotType && rows[1].StoppedSlot == slotType && rows[2].StoppedSlot == slotType;
    }
}
