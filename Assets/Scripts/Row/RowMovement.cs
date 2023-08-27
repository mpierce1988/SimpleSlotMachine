using System;
using System.Collections;
using UnityEngine;

public class RowMovement : MonoBehaviour, IMoveRow
{
    public event Action OnSpinningStart;
    public event Action OnSpinningEnd;

    [SerializeField]
    private SlotData slotData;

    private float movementInterval = 0f;

    private void Start()
    {
        movementInterval = GetMovementInterval();
    }

    public void StartRotating()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        float timeInterval = 0.025f;
        OnSpinningStart?.Invoke();

        // constant portion of spinning
        for (int i = 0; i < 10 * slotData.StepsPerSlot; i++)
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

        OnSpinningEnd?.Invoke();
    }

    private float GetMovementInterval()
    {
        float totalHeightOfSlots = slotData.StartPosition - slotData.BottomBoundary;
        float heightPerSlot = totalHeightOfSlots / (slotData.SlotValues.Count - 1); // subtract 1, last slot is repeat of first slot
        float heightPerInterval = heightPerSlot / slotData.StepsPerSlot; // divide by number of steps between slots (3)
        return heightPerInterval;
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

        for (int i = 1; i < slotData.StepsPerSlot; i++)
        {
            if (randomValueDivisibleBySteps % slotData.StepsPerSlot == i)
            {
                randomValueDivisibleBySteps += (slotData.StepsPerSlot - i);
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
        if (transform.localPosition.y <= slotData.BottomBoundary)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, slotData.StartPosition);
        }
    }

}
