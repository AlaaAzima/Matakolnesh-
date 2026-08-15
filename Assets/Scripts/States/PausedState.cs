using UnityEngine;

public class PausedState : IGameState
{
    public bool CanShoot => false;
    public bool CanPause => true;

    public void EnterState()
    {
        // Debug.Log("Entered Paused State");
        Time.timeScale = 0f;
    }

    public void ExitState()
    {
    }
}
