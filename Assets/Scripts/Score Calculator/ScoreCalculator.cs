using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour, ICalculateScore
{
    [SerializeField]
    private SlotData slotData;
    private List<Row> rows;

    public int CalculatePrize(List<Row> rowsToCalculate)
    {
        rows = rowsToCalculate;
        int prizeValue = 0;

        // check for three match
        foreach (SlotValue slotValue in slotData.SlotValues)
        {
            if (IsThreeMatch(slotValue.SlotName))
            {
                prizeValue = slotValue.TripleMatchValue;
                break;
            }
        }

        // If we found a three match, return the prize value
        if (prizeValue > 0)
        {
            return prizeValue;
        }

        // Check for a double match
        foreach (SlotValue slotValue in slotData.SlotValues)
        {
            if (IsDoubleMatch(slotValue.SlotName))
            {
                prizeValue = slotValue.DoubleMatchValue;
                break;
            }
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
