using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    public event Action OnSpinningStart;
    public event Action OnSpinningEnd;

    [SerializeField]
    private float bottomBoundary = 2f;
    [SerializeField]
    private float startPosition = 3f;
    [SerializeField]
    private int numSlots = 8;

    //private int randomValueDivisibleBySteps;
    private int stepsPerSlot = 3;
    //private float timeInterval;
    //private float movementInterval;

    private bool rowStopped = true;
    public bool RowStopped => rowStopped;
    public string stoppedSlot;

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

        // calculate movement per step. Divide steps per slot
        //movementInterval = GetMovementInterval(startPosition, bottomBoundary, numSlots);
    }

    private void OnDestroy()
    {
        GameControl.OnStartSpin -= StartRotating;
        rowMover.OnSpinningStart -= SetStartSpinState;
        rowMover.OnSpinningEnd -= SetEndSpinState;
    }

    private void StartRotating()
    {
        rowMover.StartRotating();
    }


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
