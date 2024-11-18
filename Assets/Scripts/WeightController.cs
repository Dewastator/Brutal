using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class PucnhController : MonoBehaviour
{

    [SerializeField]
    private UnityEvent OnAnimationFinished;

    public void EnablePunch()
    {
        OnAnimationFinished?.Invoke();
    }
}

