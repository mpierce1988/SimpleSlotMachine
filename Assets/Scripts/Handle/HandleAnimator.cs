using System;
using System.Collections;
using UnityEngine;

public class HandleAnimator : MonoBehaviour, IAnimateHandle
{
    public event Action OnHandleAnimationStart;
    public event Action OnHandleAnimationApex;
    public event Action OnHandleAnimationEnd;

    [SerializeField]
    private Transform handle;


    public void PullHandle()
    {
        StartCoroutine(PullHandleAnimation());
    }

    private IEnumerator PullHandleAnimation()
    {
        OnHandleAnimationStart?.Invoke();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

        OnHandleAnimationApex?.Invoke();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        OnHandleAnimationEnd?.Invoke();
    }
}
