using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PunchController : MonoBehaviour
{

    [SerializeField]
    private UnityEvent OnAnimationFinished;

    [SerializeField]
    private UnityEvent<string> OnPunch;

    public void EnablePunch()
    {
        OnAnimationFinished?.Invoke();
    }

    public void TakeDamage(string type)
    {
        OnPunch.Invoke(type);
    }
}

