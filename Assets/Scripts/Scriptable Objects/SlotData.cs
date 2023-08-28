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
    private List<SlotValue> slotValues;

    public float StartPosition => startPosition;
    public float BottomBoundary => bottomBoundary;
    public int StepsPerSlot => stepsPerSlot;
    public List<SlotValue> SlotValues => slotValues;
}

[System.Serializable]
public struct SlotValue
{
    [SerializeField]
    public string SlotName;
    [SerializeField]
    public int DoubleMatchValue;
    [SerializeField]
    public int TripleMatchValue;
}
