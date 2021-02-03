using System;

public class StateController
{
    private GameState _currentState;
    public event Action<GameState> OnCurrentState;

    public void Init()
    {
        _currentState = GameState.StartScreen;
        OnCurrentState?.Invoke(_currentState);
    }

    public void StartGame()
    {
        _currentState = GameState.Game;
        OnCurrentState?.Invoke(_currentState);
    }

    public void GameOver()
    {
        _currentState = GameState.GameOver;
        OnCurrentState?.Invoke(_currentState);
    }
}