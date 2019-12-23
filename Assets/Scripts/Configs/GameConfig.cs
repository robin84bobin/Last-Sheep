using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public int defaultSheepsCount = 12;
    public float killStateDuration = 5f;
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
    public float decreaseFactor = 0.8f;
    public float downStateDuration = 20f;
    public float highlightDuration = 5f;
    public float upStateDuration = 5f;
}