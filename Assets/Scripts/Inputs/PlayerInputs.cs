using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputs : MonoBehaviour/*, IPointerClickHandler*/
{
    [Header("��� ����������")]
    [SerializeField] private TypeOfManagment type;

    private PlayerController playerController;
    private Vector2 startTouch;
    private bool canMove;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (type == TypeOfManagment.Mouse)
        {
            GetMouseInput();
        }
        if (type == TypeOfManagment.Touch)
        {
            GetTouchInput();
        }
    }


    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //���������� ��������� ��������� ����
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = Input.touches[0].position;
                canMove = !IsPointerOverGameObject(touch.fingerId);  
            }
            //���� ��������� ��������� ���� �����
            //�������� ��������� ����
            if (
                touch.phase == TouchPhase.Ended &&
                startTouch == Input.touches[0].position)
            {
                if (canMove)
                {
                    playerController.SendRay();
                }
            }

        }
    }

    private void GetMouseInput()
    {
        ///���������, ������ �� ���
        if (Input.GetMouseButtonDown(0))
        {
            playerController.SendRayToGetTarget();
        }

        ///���������, ������ �� ���
        if (Input.GetMouseButtonDown(1))
        {
            playerController.SendRayToFocus();
        }
    }

    /// <summary>
    /// ����� ����������� ����� �� ��� � UI
    /// </summary>
    /// <param name="fingerId"></param>
    /// <returns></returns>
    bool IsPointerOverGameObject(int fingerId)
    {
        EventSystem eventSystem = EventSystem.current;
        return (eventSystem.IsPointerOverGameObject(fingerId)
            && eventSystem.currentSelectedGameObject != null);
    }
}

public enum TypeOfManagment
{
    Mouse,
    Touch
}