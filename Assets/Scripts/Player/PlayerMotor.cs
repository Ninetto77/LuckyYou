using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMotor : Motor
{

    /// <summary>
    /// ����� �������� � ��������
    /// </summary>
    /// <param name="newTarget"></param>
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.1f;
        agent.updateRotation = false;

        target = newTarget.transform;
    }

    /// <summary>
    /// ����� ����������� �������� � ��������
    /// </summary>
    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        
        target = null;
    }


}
