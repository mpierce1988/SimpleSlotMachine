using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour, ICalculateScore
{
    [SerializeField]
    private SlotData slotData;

    [SerializeField]
    private List<MatchStrategy> matchingStrategies;

    private List<Row> rows;

    public int CalculatePrize(List<Row> rowsToCalculate)
    {
        rows = rowsToCalculate;
        int prizeValue = 0;

        // for each strategy, try to find a match
        foreach (MatchStrategy strategy in matchingStrategies)
        {
            if (strategy.TryGetMatch(rows, slotData, out string match))
            {
                // a match was found
                // get slot value
                SlotValue slotValue = slotData.SlotValues.Find(s => s.SlotName == match);

                // find index of this strategy
                MatchStrategy matchingSlotDataStrategy = slotValue.MatchStrategies.Where(s => s.GetType() == strategy.GetType()).FirstOrDefault();

                int indexOfStrategy = slotValue.MatchStrategies.IndexOf(matchingSlotDataStrategy);

                // get point value from slotValue
                prizeValue = slotValue.PrizeValues[indexOfStrategy];
                break;
            }
        }

        return prizeValue;
    }
}


