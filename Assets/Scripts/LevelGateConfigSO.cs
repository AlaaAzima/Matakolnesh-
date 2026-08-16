using UnityEngine;

[CreateAssetMenu(fileName = "LevelGateConfig", menuName = "Config/LevelGateConfig")]
public class LevelGateConfigSO : ScriptableObject
{
    [Header("Every-5-Levels Gate")]
    public int gateInterval = 5;
    public int minStarsPerLevel = 2;
    public int requiredLevelsMeetingMin = 3;
}