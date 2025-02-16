using UnityEngine;
using UnityEngine.Events;

public class AnimationFinishCallback : MonoBehaviour
{
    public UnityEvent OnFinishEvent;

    public void AnimationFinish()
    {
        OnFinishEvent?.Invoke();
    }
}
