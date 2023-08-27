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
    private float movementInterval;

    private bool rowStopped = true;
    public bool RowStopped => rowStopped;
    public string stoppedSlot;

    // Start is called before the first frame update
    void Start()
    {

        GameControl.OnStartSpin += StartRotating;

        OnSpinningStart += SetStartSpinState;
        OnSpinningEnd += SetEndSpinState;

        // calculate movement per step. Divide steps per slot
        movementInterval = GetMovementInterval(startPosition, bottomBoundary, numSlots);
    }

    private void OnDestroy()
    {
        GameControl.OnStartSpin -= StartRotating;
        OnSpinningStart -= SetStartSpinState;
        OnSpinningEnd -= SetEndSpinState;
    }

    #region Movement

    public float GetMovementInterval(float startPosition, float bottomBoundary, int numSlots)
    {
        float totalHeightOfSlots = startPosition - bottomBoundary;
        float heightPerSlot = totalHeightOfSlots / (numSlots - 1); // subtract 1, last slot is repeat of first slot
        float heightPerInterval = heightPerSlot / stepsPerSlot; // divide by number of steps between slots (3)
        return heightPerInterval;
    }

    private void StartRotating()
    {
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        //rowStopped = false;
        float timeInterval = 0.025f;
        OnSpinningStart?.Invoke();

        // constant portion of spinning
        for (int i = 0; i < 10 * stepsPerSlot; i++)
        {
            MoveRowDown();

            yield return new WaitForSeconds(timeInterval);
        }


        int randomValueDivisibleBySteps = GetRandomNumberDivisibleBySteps();

        // final spin, slowing down as it reaches its final destination
        for (int i = 0; i < randomValueDivisibleBySteps; i++)
        {
            // if row is at the bottom, move it to the top
            MoveRowDown();

            timeInterval = GetSlowTimeInterval(i, randomValueDivisibleBySteps);
            yield return new WaitForSeconds(timeInterval);
        }

        //stoppedSlot = GetCurrentSlot();

        //rowStopped = true;
        OnSpinningEnd?.Invoke();
    }

    private float GetSlowTimeInterval(int i, int randomValueDivisibleBySteps)
    {
        float timeInterval = 0.25f;
        // manipulate timeInterval to slow down spin
        // as i gets closer to randomValue, timeInterval increases
        if (i < Mathf.RoundToInt(randomValueDivisibleBySteps * movementInterval))
        {
            // i is 0% to 25% of randomValue
            timeInterval = 0.05f;
        }
        else if (i < Mathf.RoundToInt(randomValueDivisibleBySteps * 0.5f))
        {
            // i is 25% to 50% of randomValue
            timeInterval = 0.1f;
        }
        else if (i < Mathf.RoundToInt(randomValueDivisibleBySteps * 0.75f))
        {
            // i is 50% to 75% of randomValue
            timeInterval = 0.15f;
        }
        else if (i < Mathf.RoundToInt(randomValueDivisibleBySteps * 0.95f))
        {
            // i is 75% to 95% of randomValue
            timeInterval = 0.2f;
        }
        else
        {
            // i is 95% to 100% of randomValue
            timeInterval = 0.25f;
        }

        return timeInterval;
    }

    private int GetRandomNumberDivisibleBySteps()
    {
        // get random value between 60 and 100
        int randomValueDivisibleBySteps = UnityEngine.Random.Range(60, 100);

        for (int i = 1; i < stepsPerSlot; i++)
        {
            if (randomValueDivisibleBySteps % stepsPerSlot == i)
            {
                randomValueDivisibleBySteps += (stepsPerSlot - i);
                break;
            }
        }

        return randomValueDivisibleBySteps;
    }

    private void MoveRowDown()
    {
        // if row is at the bottom, move it to the top
        ResetRowIfBelowBoundary();

        // move row down
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - movementInterval);
    }

    private void ResetRowIfBelowBoundary()
    {
        if (transform.localPosition.y <= bottomBoundary)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, startPosition);
        }
    }

    #endregion

    #region Row State

    private void SetStartSpinState()
    {
        rowStopped = false;
        stoppedSlot = "";
    }

    private void SetEndSpinState()
    {
        rowStopped = true;
        stoppedSlot = GetCurrentSlot();
    }

    #endregion

    #region Get Slot Value

    private string GetCurrentSlot()
    {
        string stoppedSlot = "Unknown";

        if (transform.localPosition.y == bottomBoundary)
        {
            stoppedSlot = "Diamonds";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 1 * stepsPerSlot))
        {
            stoppedSlot = "Crown";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 2 * stepsPerSlot))
        {
            stoppedSlot = "Melon";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 3 * stepsPerSlot))
        {
            stoppedSlot = "Bar";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 4 * stepsPerSlot))
        {
            stoppedSlot = "Seven";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 5 * stepsPerSlot))
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 6 * stepsPerSlot))
        {
            stoppedSlot = "Lemon";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 7 * stepsPerSlot))
        {
            stoppedSlot = "Diamond";
        }
        else
        {
            stoppedSlot = "Unknown";
        }

        return stoppedSlot;
    }

    #endregion




}
