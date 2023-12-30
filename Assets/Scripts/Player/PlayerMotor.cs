using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMotor : Motor
{

    /// <summary>
    /// Метод движения к предмету
    /// </summary>
    /// <param name="newTarget"></param>
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.1f;
        agent.updateRotation = false;

        target = newTarget.transform;
    }

    /// <summary>
    /// Метод прекращения движения к предмету
    /// </summary>
    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        
        target = null;
    }


}
