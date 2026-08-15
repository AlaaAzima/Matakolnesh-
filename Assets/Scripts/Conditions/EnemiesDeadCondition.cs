using UnityEngine;

public class EnemiesDeadCondition : LevelCondition
{
    public override bool IsConditionMet()
    {
        if (GameManagerJE.Instance == null) return false;

        return GameManagerJE.Instance.remainingEnemies <= 0;
    }
}
