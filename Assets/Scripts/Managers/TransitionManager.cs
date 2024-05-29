using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] protected Animator _transition;

    public void StartTransAnimation()
    {
        _transition.SetBool("Start", true);
    }

    public void EndTransAnimation()
    {
        _transition.SetBool("Start", false);
    }
}
