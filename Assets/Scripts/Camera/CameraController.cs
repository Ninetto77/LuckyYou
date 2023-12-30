using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Скорость передвижения камеры")]
    public float _speed = 1;
    [Tooltip("Доступный радиус перемещения камеры")]
    public float _radius = 10;
    [Tooltip("Точка от которой считается доступный радиус перемещения камеры")]
    public Transform _target;
    [Tooltip("Время перемещения камеры")]
    public float timeToMove = 6f;

    private Touch _touch;
    private Vector3 _targetPos;

    private void Start()
    {
        if (_target == null)
        {
            _target = this.transform;
        }

        _targetPos = _target.position;
    }

    private void Update()
    {
        MoveCamera();
    }
    /// <summary>
    /// Метод передвижения камеры
    /// </summary>
    private void MoveCamera()
    {

        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {

                Vector3 movePos = new Vector3(
                    transform.position.x + _touch.deltaPosition.x * _speed *  Time.deltaTime,
                    transform.position.y,
                    transform.position.z + _touch.deltaPosition.y * _speed *  Time.deltaTime);

                Vector3 distance = movePos - _targetPos;

                if (distance.magnitude < _radius)
                    //transform.position = movePos;
                    transform.position = Vector3.Lerp(transform.position, movePos, timeToMove);
            }
        }
    }

}
