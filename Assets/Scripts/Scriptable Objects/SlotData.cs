using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slot Data", menuName = "Data/Slot Data")]
public class SlotData : ScriptableObject
{
    [SerializeField]
    private float startPosition;

    [SerializeField]
    private float bottomBoundary;

    [SerializeField]
    private int stepsPerSlot;

    [SerializeField]
    private List<string> slotValues;

    public float StartPosition => startPosition;
    public float BottomBoundary => bottomBoundary;
    public int StepsPerSlot => stepsPerSlot;
    public List<string> SlotValues => slotValues;
}
