using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private string fly = "Fly";
    private string swipeDown = "SwipeDown";
    private string boost = "Boost";
    public void SetFlyTrigger()
    {
        SetTrigger(fly);
    }
    public void SetSwipeDownTrigger()
    {
        SetTrigger(swipeDown);
    }
    public void SetBoostTrigger()
    {
        SetTrigger(boost);
    }
    
    
    public void SetTrigger(string triggerName) => _animator.SetTrigger(triggerName);
    
}
