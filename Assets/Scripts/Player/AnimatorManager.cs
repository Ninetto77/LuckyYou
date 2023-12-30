using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorManager : MonoBehaviour, IAnimator
{    
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }       

    public void SetFloat(string floatName, float value)
    {
        _animator.SetFloat(floatName, value);
    }

    public void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    public void SetBool(string boolName, bool value)
    {
        _animator.SetBool(boolName, value);
    }
}
