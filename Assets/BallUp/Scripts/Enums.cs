using System;

public enum GameState
{
    StartScreen,
    Game,
    GameOver
}

[Serializable]
public enum PoolObject
{
    RichPlatform,
    Platform,
    CoinCollectEffect,
    JumpCollectEffect
}