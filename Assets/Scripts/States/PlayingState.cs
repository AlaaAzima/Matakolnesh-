using UnityEngine;

public class PlayingState : IGameState
{
    public bool CanShoot => true;
    public bool CanPause => true;

    public void EnterState()
    {
        // Debug.Log("Entered Playing State");
        Time.timeScale = 1f;
    }

    public void ExitState()
    {
    }
}
