using System.Collections.Generic;
using UnityEngine;

public abstract class MatchStrategy : ScriptableObject, IMatchStrategy<List<Row>>
{
    public abstract bool TryGetMatch(List<Row> rows, SlotData slotData, out string match);
}