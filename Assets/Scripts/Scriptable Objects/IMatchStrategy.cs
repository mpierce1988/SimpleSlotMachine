public interface IMatchStrategy<T>
{
    bool TryGetMatch(T rows, SlotData slotData, out string match);
}
