using System;

public class BossInteractable : Interactable
{
    public static event Action OnBossHere;
    public override void Interact()
    {
        base.Interact();
        if (player.TryGetComponent(out IStopMoving stopMoving))
        {
            stopMoving.StopMoving(1);
        }
        OnBossHere?.Invoke();
    }
}
