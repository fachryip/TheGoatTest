using UnityEngine;

public class HomeObjectLogic : MonoBehaviour
{
    [SerializeField] private AnimationFinishCallback ObjectFinishCallback;
    [SerializeField] private GameObject ReadyObject;

    private void OnEnable()
    {
        ObjectFinishCallback.OnFinishEvent.AddListener(AnimationFinish);
    }

    private void OnDisable()
    {
        ObjectFinishCallback.OnFinishEvent.RemoveListener(AnimationFinish);
    }

    public void AnimationFinish()
    {
        GameLifetimeEvent.OnReady?.Invoke();
    }

    public void SetReady()
    {
        ReadyObject.SetActive(true);
    }
}