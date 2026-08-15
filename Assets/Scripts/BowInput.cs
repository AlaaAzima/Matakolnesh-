using UnityEngine;
using System;

public class BowInput : MonoBehaviour
{
    public event Action OnFireStarted;
    public event Action OnFireHeld;
    public event Action OnFireReleased;

    void Update()
    {
        if (GameManagerJE.Instance == null || GameManagerJE.Instance.CurrentState == null || !GameManagerJE.Instance.CurrentState.CanShoot) return;

        if (Input.GetButtonDown("Fire1"))
        {
            OnFireStarted?.Invoke();
        }

        if (Input.GetButton("Fire1"))
        {
            OnFireHeld?.Invoke();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            OnFireReleased?.Invoke();
        }
    }
}