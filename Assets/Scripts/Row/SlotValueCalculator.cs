using System.Collections.Generic;
using UnityEngine;

public class SlotValueCalculator : MonoBehaviour, ICalulateSlotValue
{
    [SerializeField]
    private SlotData slotData;

    private float stepHeight;

    private void Start()
    {
        stepHeight = GetStepHeight();
    }

    private float GetStepHeight()
    {
        float totalHeightOfSlots = slotData.StartPosition - slotData.BottomBoundary;
        float heightPerSlot = totalHeightOfSlots / (slotData.SlotValues.Count - 1); // subtract 1 to get proper height

        return heightPerSlot;
    }

    public string GetCurrentSlot()
    {
        string stoppedSlot = "Unknown";

        for (int i = 0; i < slotData.SlotValues.Count; i++)
        {
            float heightThreshold = slotData.BottomBoundary + (stepHeight * i);
            if (transform.localPosition.y == heightThreshold)
            {
                stoppedSlot = slotData.SlotValues[i];
                break;
            }
        }

        return stoppedSlot;
    }
}
