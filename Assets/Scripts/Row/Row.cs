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

    private int randomValue;
    private float timeInterval;
    public float movementInterval;

    public bool rowStopped = false;
    public string stoppedSlot;

    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        GameControl.OnStartSpin += StartRotating;

        // Remove one, last slot is repeat of first slot
        numSlots = numSlots - 1;

        // calculate movement per step. Divide 3 (3 steps per movement between slots)
        movementInterval = ((startPosition - bottomBoundary) / numSlots) / 3;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;
        OnSpinningStart?.Invoke();

        // constant portion of spinning
        for (int i = 0; i < 30; i++)
        {
            // if row is at the bottom, move it to the top
            if (transform.localPosition.y <= bottomBoundary)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, startPosition);
            }

            // move row down
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - movementInterval);

            yield return new WaitForSeconds(timeInterval);
        }

        // get random value between 60 and 100
        randomValue = UnityEngine.Random.Range(60, 100);

        // ensure randomValue is divisible by 3
        // there are three steps between each slot
        if (randomValue % 3 == 1)
        {
            randomValue += 2;
        }
        else if (randomValue % 3 == 2)
        {
            randomValue += 1;
        }

        // final spin, slowing down as it reaches its final destination
        for (int i = 0; i < randomValue; i++)
        {
            // if row is at the bottom, move it to the top
            if (transform.localPosition.y <= bottomBoundary)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, startPosition);
            }

            // move row down
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - movementInterval);

            // manipulate timeInterval to slow down spin
            // as i gets closer to randomValue, timeInterval increases
            if (i < Mathf.RoundToInt(randomValue * movementInterval))
            {
                // i is 0% to 25% of randomValue
                timeInterval = 0.05f;
            }
            else if (i < Mathf.RoundToInt(randomValue * 0.5f))
            {
                // i is 25% to 50% of randomValue
                timeInterval = 0.1f;
            }
            else if (i < Mathf.RoundToInt(randomValue * 0.75f))
            {
                // i is 50% to 75% of randomValue
                timeInterval = 0.15f;
            }
            else if (i < Mathf.RoundToInt(randomValue * 0.95f))
            {
                // i is 75% to 95% of randomValue
                timeInterval = 0.2f;
            }
            else
            {
                // i is 95% to 100% of randomValue
                timeInterval = movementInterval;
            }

            yield return new WaitForSeconds(timeInterval);
        }

        if (transform.localPosition.y == bottomBoundary)
        {
            stoppedSlot = "Diamonds";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 1 * 3))
        {
            stoppedSlot = "Crown";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 2 * 3))
        {
            stoppedSlot = "Melon";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 3 * 3))
        {
            stoppedSlot = "Bar";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 4 * 3))
        {
            stoppedSlot = "Seven";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 5 * 3))
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 6 * 3))
        {
            stoppedSlot = "Lemon";
        }
        else if (transform.localPosition.y == bottomBoundary + (movementInterval * 7 * 3))
        {
            stoppedSlot = "Diamond";
        }
        else
        {
            stoppedSlot = "Unknown";
        }

        rowStopped = true;
        OnSpinningEnd?.Invoke();
    }

    private void OnDestroy()
    {
        GameControl.OnStartSpin -= StartRotating;
    }
}
