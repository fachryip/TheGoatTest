using UnityEngine;
using UnityEngine.Events;

public class DragRotateObject : MonoBehaviour
{
    public UnityEvent<Quaternion> OnRotated;

    [SerializeField] private float RotationSpeed = 50f;

    private Vector3 _lastMousePosition;
    private Quaternion _targetRotation;

    private void Start()
    {
        _targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition - _lastMousePosition;
            _lastMousePosition = Input.mousePosition;

            var rotateX = delta.y * RotationSpeed * Time.deltaTime;
            var rotateY = -delta.x * RotationSpeed * Time.deltaTime;

            var rotationX = Quaternion.Euler(rotateX, 0, 0);
            var rotationY = Quaternion.Euler(0, rotateY, 0);

            _targetRotation = rotationY * rotationX * _targetRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * RotationSpeed / 2);

            OnRotated?.Invoke(transform.rotation);
        }
    }
}