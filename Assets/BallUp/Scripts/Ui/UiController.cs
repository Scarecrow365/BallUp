using System;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private UiView view;

    public event Action OnPlayButtonPressed;
    public event Action OnExitButtonPressed;

    private void Awake()
    {
        view.OnPlayButtonPressed += StartGame;
        view.OnExitButtonPressed += ExitGame;
    }

    private void OnDestroy()
    {
        view.OnPlayButtonPressed -= StartGame;
        view.OnExitButtonPressed -= ExitGame;
    }

    private void ExitGame()
    {
        OnExitButtonPressed?.Invoke();
    }

    private void StartGame()
    {
        OnPlayButtonPressed?.Invoke();
    }

    public void UpdateJumpScore(int score) => view.JumpScoreCount(score);
    public void UpdateCoinScore(int score) => view.CoinScoreCount(score);

    public void ChangeScreenState(GameState state)
    {
        view.ChangeScreen(state);
    }
}