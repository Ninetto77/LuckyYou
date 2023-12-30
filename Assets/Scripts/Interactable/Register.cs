using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : Interactable
{
    public static event Action OnOrderAccepted;   

    public override void Interact()
    {
        base.Interact();
        Animate();     
        GameManager.Instance.ShowTutorial(2);

        if (player.TryGetComponent(out IStopMoving stopMoving))
        {
            stopMoving.StopMoving(1);
        }
        OnOrderAccepted?.Invoke();
    }

    public void Animate()
    {
        if (player.TryGetComponent(out IAnimator animator))        
            animator.SetTrigger("TakeOrder");
        if (player.TryGetComponent(out IStopMoving stopMoving))
            stopMoving.StopMoving(1.5f);
    }
}
