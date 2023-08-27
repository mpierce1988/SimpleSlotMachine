using System;
using System.Collections;
using UnityEngine;

public class RowMovement : MonoBehaviour, IMoveRow
{
    public event Action OnSpinningStart;
    public event Action OnSpinningEnd;

    [SerializeField]
    private int stepsPerSlot = 3;
    [SerializeField]
    private float startPosition = 2.59f;
    [SerializeField]
    private float bottomBoundary = -2.66f;
    [SerializeField]
    private int numSlots = 8;

    private float movementInterval = 0f;

    private void Start()
    {
        movementInterval = GetMovementInterval(startPosition, bottomBoundary, numSlots);
    }

    public void StartRotating()
    {
        StartCoroutine(Rotate());
    }

    private float GetMovementInterval(float startPosition, float bottomBoundary, int numSlots)
    {
        float totalHeightOfSlots = startPosition - bottomBoundary;
        float heightPerSlot = totalHeightOfSlots / (numSlots - 1); // subtract 1, last slot is repeat of first slot
        float heightPerInterval = heightPerSlot / stepsPerSlot; // divide by number of steps between slots (3)
        return heightPerInterval;
    }

    private IEnumerator Rotate()
    {
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

}
