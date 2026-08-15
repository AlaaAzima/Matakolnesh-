using UnityEngine;

public abstract class LevelCondition : MonoBehaviour
{
    // Returns true when the specific condition is successfully met
    public abstract bool IsConditionMet();
}
