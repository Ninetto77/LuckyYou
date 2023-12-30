using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float Distance = 100;
    public LayerMask MovewmentMask;

    private PlayerMotor motor;
    private Camera mainCamera;
    private Interactable focus;

    void Start()
    {
        mainCamera = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    public void SendRay()
    {
        SendRayToGetTarget();
        SendRayToFocus();
    }

    /// <summary>
    /// Метод отправления луча на курсор мыши
    /// </summary>
    public void SendRayToGetTarget()
    {
        //создание луча
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //если луч что-то задел
        if (Physics.Raycast(ray, out hit, Distance, MovewmentMask))
        {
            //if (hit.transform.gameObject.layer.Equals("UI"))
            //    return;
            Move(hit.point);
            //убрать фокус
            RemoveFocus();
        }
    }

    /// <summary>
    /// Метод отправления луча на курсор мыши для фокуса
    /// </summary>
    public void SendRayToFocus()
    {
        //создание луча
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //если луч что-то задел
        if (Physics.Raycast(ray, out hit, Distance))
        {

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                //установить фокус
                SetFocus(interactable);
            }
        }
    }

    /// <summary>
    /// Метод движения к точке курсора мыши
    /// </summary>
    /// <param name="hit"></param>
    public void Move(Vector3 point)
    {
        motor.MovePlayer(point);
    }

    /// <summary>
    /// Метод обозначения фокуса на предмет
    /// </summary>
    /// <param name="hit"></param>
    public void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            if (focus != null)
                focus.OnDefocused();
            // фокуса на предмет
            focus = newFocus;
            newFocus.OnFocused(transform);
        }


        //движение к предмету
        Move(newFocus.transform.position);
        motor.FollowTarget(newFocus);
    }

    /// <summary>
    /// Метод удаления фокуса с предмета
    /// </summary>
    public void RemoveFocus()
    {
        //удание фокуса с предмета
        if(focus != null)
            focus.OnDefocused();
        focus = null;

        //прекращение движения к предмету
        motor.StopFollowTarget();

    }


}
