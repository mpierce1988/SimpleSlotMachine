using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Triple Match Strategy", menuName = "Data/Strategies/Triple Match")]
public class TripleMatchStrategy : MatchStrategy
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

            if (matchesFound >= 3)
            {
                match = slotName;
                break;
            }
        }

        return match != "";
    }
}
