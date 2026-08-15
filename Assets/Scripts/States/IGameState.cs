public interface IGameState
{
    bool CanShoot { get; }
    bool CanPause { get; }
    
    void EnterState();
    void ExitState();
}
