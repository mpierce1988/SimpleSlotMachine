using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Double Match Strategy", menuName = "Data/Strategies/Double Match")]
public class DoubleMatchStrategy : MatchStrategy
{
    public override bool TryGetMatch(List<Row> rows, SlotData slotData, out string match)
    {
        match = "";

        for (int i = 0; i < slotData.SlotValues.Count; i++)
        {
            string slotName = slotData.SlotValues[i].SlotName;
            // For reach slot value, check for a triple match
            int matchesFound = 0;

            foreach (Row row in rows)
            {
                if (row.StoppedSlot == slotName)
                {
                    matchesFound++;
                }
            }

            if (matchesFound >= 2)
            {
                match = slotName;
                break;
            }
        }

        return match != "";
    }
}
