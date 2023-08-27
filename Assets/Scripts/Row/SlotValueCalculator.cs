using UnityEngine;

public class SlotValueCalculator : MonoBehaviour, ICalulateSlotValue
{
    [SerializeField]
    private float startPosition;
    [SerializeField]
    private float bottomBoundary;
    [SerializeField]
    private int numSlots;
    [SerializeField]
    private int stepsPerSlot = 3;

    private float movementInterval;

    private void Start()
    {
        movementInterval = GetMovementInterval(startPosition, bottomBoundary, numSlots);
    }

    private float GetMovementInterval(float startPosition, float bottomBoundary, int numSlots)
    {
        float totalHeightOfSlots = startPosition - bottomBoundary;
        float heightPerSlot = totalHeightOfSlots / (numSlots - 1); // subtract 1, last slot is repeat of first slot
        float heightPerInterval = heightPerSlot / stepsPerSlot; // divide by number of steps between slots (3)
        return heightPerInterval;
    }

    public string GetCurrentSlot()
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
}
