using System;
using UnityEngine;

public class MouseInput : MonoBehaviour, IInput
{
    public event Action OnClick;

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}
