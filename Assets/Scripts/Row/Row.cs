using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    public event Action OnSpinningStart;
    public event Action OnSpinningEnd;

    private bool rowStopped = true;
    private string stoppedSlot;

    public bool RowStopped => rowStopped;
    public string StoppedSlot => stoppedSlot;

    private IMoveRow rowMover;
    private ICalulateSlotValue slotValueCalculator;

    private void Awake()
    {
        rowMover = GetComponent<IMoveRow>();
        slotValueCalculator = GetComponent<ICalulateSlotValue>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameControl.OnStartSpin += StartRotating;

        rowMover.OnSpinningStart += SetStartSpinState;
        rowMover.OnSpinningEnd += SetEndSpinState;
    }

    private void OnDestroy()
    {
        GameControl.OnStartSpin -= StartRotating;
        rowMover.OnSpinningStart -= SetStartSpinState;
        rowMover.OnSpinningEnd -= SetEndSpinState;
    }

    #region Event Handlers

    private void StartRotating()
    {
        rowMover.StartRotating();
    }

    #endregion

    #region Row State

    private void SetStartSpinState()
    {
        rowStopped = false;
        stoppedSlot = "";
        OnSpinningStart?.Invoke();
    }

    private void SetEndSpinState()
    {
        rowStopped = true;
        stoppedSlot = slotValueCalculator.GetCurrentSlot();
        OnSpinningEnd?.Invoke();
    }

    #endregion

}
