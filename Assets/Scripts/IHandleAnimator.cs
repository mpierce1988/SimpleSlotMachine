using System;

public interface IAnimateHandle
{
    event Action OnHandleAnimationStart;
    event Action OnHandleAnimationApex;
    event Action OnHandleAnimationEnd;

    void PullHandle();
}