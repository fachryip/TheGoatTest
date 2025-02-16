using UnityEngine;

public class HomeObjectLogic : MonoBehaviour
{
    [SerializeField] private AnimationFinishCallback ObjectFinishCallback;
    [SerializeField] private GameObject ReadyObject;
    [SerializeField] private DragRotateObject RotateObject;

    private void Awake()
    {
        RotateObject.enabled = false;
    }

    private void OnEnable()
    {
        ObjectFinishCallback.OnFinishEvent.AddListener(AnimationFinish);
        RotateObject.OnRotated.AddListener(OnObjectRotated);
    }

    private void OnDisable()
    {
        ObjectFinishCallback.OnFinishEvent.RemoveListener(AnimationFinish);
        RotateObject.OnRotated.RemoveListener(OnObjectRotated);
    }

    public void AnimationFinish()
    {
        RotateObject.enabled = true;
        GameLifetimeEvent.OnReady?.Invoke();
    }

    public void SetReady()
    {
        ReadyObject.SetActive(true);
    }

    public void SetActive(bool isActive)
    {
        ReadyObject.SetActive(isActive);
        RotateObject.gameObject.SetActive(isActive);
    }

    private void OnObjectRotated(Quaternion rotation)
    {
        UnityInterop.ObjectRotated(rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
    }
}