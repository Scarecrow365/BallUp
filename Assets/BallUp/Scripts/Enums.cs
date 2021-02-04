using System;

public enum GameState
{
    StartScreen,
    Game,
    GameOver
}

public enum ParticleEffect
{
    CoinCount,
    JumpCount
}

[Serializable]
public enum PoolObject
{
    RichPlatform,
    Platform
}