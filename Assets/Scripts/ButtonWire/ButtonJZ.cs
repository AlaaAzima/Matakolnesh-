using UnityEngine;
using System;

public class ButtonJZ : MonoBehaviour, IInteractable
{
    public event Action OnButtonClick;

    [SerializeField] private Animator animator;
    [SerializeField] private string clickTriggerName = "IsClicked";

    private void Awake()
    {

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void Interact()
    {

        if (animator != null)
        {
            animator.SetTrigger(clickTriggerName);
        }

        OnButtonClick?.Invoke();
    }
}