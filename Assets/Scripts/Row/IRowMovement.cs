using System;

public interface IMoveRow
{
    event Action OnSpinningStart;
    event Action OnSpinningEnd;

    void StartRotating();
}