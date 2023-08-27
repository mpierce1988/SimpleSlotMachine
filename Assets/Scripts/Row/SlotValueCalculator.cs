using System.Collections.Generic;
using UnityEngine;

public class SlotValueCalculator : MonoBehaviour, ICalulateSlotValue
{
    [SerializeField]
    private float startPosition;
    [SerializeField]
    private float bottomBoundary;
    [SerializeField]
    private int stepsPerSlot = 3;
    [SerializeField]
    private List<string> slotValues;

    private float movementInterval;

    private void Start()
    {
        movementInterval = GetMovementInterval();
    }

    private float GetMovementInterval()
    {
        float totalHeightOfSlots = startPosition - bottomBoundary;
        float heightPerSlot = totalHeightOfSlots / (slotValues.Count - 1); // subtract 1 to get proper height
        float heightPerInterval = heightPerSlot / stepsPerSlot; // divide by number of steps between slots (3)
        return heightPerInterval;
    }

    public string GetCurrentSlot()
    {
        string stoppedSlot = "Unknown";

        for (int i = 0; i < slotValues.Count; i++)
        {
            float heightThreshold = bottomBoundary + (movementInterval * i * stepsPerSlot);
            if (transform.localPosition.y == heightThreshold)
            {
                stoppedSlot = slotValues[i];
                break;
            }
        }

        return stoppedSlot;
    }
}
