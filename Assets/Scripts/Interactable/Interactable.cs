using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour/*, IInteract*/
{
    [Header("���������")]
    public float radius = 1.5f;
    public Transform interactTransform;

    [Header("�����")]
    public bool isFocused = false;
    
    protected Transform player;
    private bool isInteract = false;

    public virtual void Start()
    {
        if (interactTransform == null)
        {
            interactTransform = transform;
        }
    }

    private void Update()
    {
        if (isFocused && isInteract == false)
        {
            float distance = Vector3.Distance(interactTransform.position, player.position);
            if ( (distance<= radius))
            {
                isInteract = true;
                Interact();
            }
        }
    }


    public virtual void Interact()
    {
        Debug.Log("Intaract");
        StartCoroutine(WaitToDefocus());
    }

    private IEnumerator WaitToDefocus()
    {
        yield return new WaitForEndOfFrame();
        player.GetComponent<PlayerController>().RemoveFocus();
    }

    /// <summary>
    /// ����� ����������� �� �������
    /// </summary>
    /// <param name="playerTransform"></param>
    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        isInteract = false;

        //��� ���������� ���������� ����� ������� � ���������
        player = playerTransform; 
    }

    /// <summary>
    /// ����� ������ ����������� � ��������
    /// </summary>
    public void OnDefocused()
    {
        isFocused = false;
        isInteract = false;

        player = null;
    }

    public void Execute()
    {
        
    }
}
