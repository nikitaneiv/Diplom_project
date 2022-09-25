using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetFlyTrigger()
    {
        SetTrigger("Fly");
    }
    public void SetSwipeDownTrigger()
    {
        SetTrigger("SwipeDown");
    }
    
    
    public void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);
    
}
