using UnityEngine;

public class NoActiveArrowsCondition : LevelCondition
{
    public override bool IsConditionMet()
    {
        if (GameManagerJE.Instance == null) return false;

        return GameManagerJE.Instance.activeArrowCount <= 0;
    }
}
