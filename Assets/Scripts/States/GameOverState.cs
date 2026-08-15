using UnityEngine;

public class GameOverState : IGameState
{
    public bool CanShoot => false;
    public bool CanPause => false;

    public void EnterState()
    {
        // Debug.Log("Entered Game Over State");
    }

    public void ExitState()
    {
    }
}
