using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public float downStateDuration = 30f;
    public float upStateDuration = 5f;
    
    public int defaultSheepsCount = 12;
    public PlatformConfig Platform;
    public SheepConfig sheep;
}

[Serializable]
public class SheepConfig
{
    public float speed = 1;
}

[Serializable]
public class PlatformConfig
{
    public float highlightTime = 5f;
    public int defaultMaxSheepCapacity = 12;
    public int decreaseCapacityFactor = 1;
}
