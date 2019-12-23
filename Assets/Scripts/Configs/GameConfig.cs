using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public float downStateDuration = 30f;
    public float upStateDuration = 5f;
    public float killStateDuration = 5f;
    
    public int defaultSheepsCount = 12;
    public PlatformConfig Platform;
    public SheepConfig sheep;

    public float GetPlatformHighLightPeriod()
    {
        return downStateDuration - Platform.highlightDuration;
    }
}

[Serializable]
public class SheepConfig
{
    public float speed = 1;
}

[Serializable]
public class PlatformConfig
{
    public float highlightDuration = 5f;
    public float decreaseCapacityFactor = 0.8f;
}
