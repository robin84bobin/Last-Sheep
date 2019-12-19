using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
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
    public int defaultMaxSheepCapacity = 12;
    public int decreaseCapacityFactor = 1;
    
    public float appearPeriod = 30f;
    public float highlightTime = 5f;
}
