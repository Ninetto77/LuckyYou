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
    /// ����� ����������� ���� �� ������ ����
    /// </summary>
    public void SendRayToGetTarget()
    {
        //�������� ����
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //���� ��� ���-�� �����
        if (Physics.Raycast(ray, out hit, Distance, MovewmentMask))
        {
            //if (hit.transform.gameObject.layer.Equals("UI"))
            //    return;
            Move(hit.point);
            //������ �����
            RemoveFocus();
        }
    }

    /// <summary>
    /// ����� ����������� ���� �� ������ ���� ��� ������
    /// </summary>
    public void SendRayToFocus()
    {
        //�������� ����
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //���� ��� ���-�� �����
        if (Physics.Raycast(ray, out hit, Distance))
        {

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                //���������� �����
                SetFocus(interactable);
            }
        }
    }

    /// <summary>
    /// ����� �������� � ����� ������� ����
    /// </summary>
    /// <param name="hit"></param>
    public void Move(Vector3 point)
    {
        motor.MovePlayer(point);
    }

    /// <summary>
    /// ����� ����������� ������ �� �������
    /// </summary>
    /// <param name="hit"></param>
    public void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            if (focus != null)
                focus.OnDefocused();
            // ������ �� �������
            focus = newFocus;
            newFocus.OnFocused(transform);
        }


        //�������� � ��������
        Move(newFocus.transform.position);
        motor.FollowTarget(newFocus);
    }

    /// <summary>
    /// ����� �������� ������ � ��������
    /// </summary>
    public void RemoveFocus()
    {
        //������ ������ � ��������
        if(focus != null)
            focus.OnDefocused();
        focus = null;

        //����������� �������� � ��������
        motor.StopFollowTarget();

    }


}
