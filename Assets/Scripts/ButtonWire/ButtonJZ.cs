using UnityEngine;
using System;
public class ButtonJZ : MonoBehaviour,IInteractable
{
    public event Action OnButtonClick;
    public void Interact()
    {
        OnButtonClick?.Invoke();
    }
}
