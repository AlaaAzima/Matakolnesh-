using System;

public static class GameEvents
{
    // Gameplay Events
    public static event Action OnEnemyKilled;
    public static void TriggerEnemyKilled() => OnEnemyKilled?.Invoke();

    public static event Action OnArrowSpawned;
    public static void TriggerArrowSpawned() => OnArrowSpawned?.Invoke();

    public static event Action OnArrowDestroyed;
    public static void TriggerArrowDestroyed() => OnArrowDestroyed?.Invoke();

    public static event Action OnPlayerDied;
    public static void TriggerPlayerDied() => OnPlayerDied?.Invoke();

    // Sound Events
    public static event Action<SoundType> OnPlaySound;
    public static void TriggerPlaySound(SoundType type) => OnPlaySound?.Invoke(type);

    // Call this from GameManager OnDestroy to prevent memory leaks when reloading scenes
    public static void Clear()
    {
        OnEnemyKilled = null;
        OnArrowSpawned = null;
        OnArrowDestroyed = null;
        OnPlayerDied = null;
        OnPlaySound = null;
    }
}
