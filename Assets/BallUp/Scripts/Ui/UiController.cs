using System;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScoreJump;
    [SerializeField] private TextMeshProUGUI textScoreCoin;
    [SerializeField] private MainMenuWindow mainMenuWindow;
    [SerializeField] private GameOverWindow gameOverWindow;

    public event Action OnPlayButtonPressed;
    public event Action OnRestartButtonPressed;
    public event Action OnExitButtonPressed;

    private void Awake()
    {
        mainMenuWindow.OnPlayButtonPressed += StartGame;
        mainMenuWindow.OnExitButtonPressed += ExitGame;
        gameOverWindow.OnRetryButtonPressed += RestartGame;
        gameOverWindow.OnExitButtonPressed += Exit;
        
        UpdateCoinScoreCount(0);
        UpdateJumpScoreCount(0);
    }

    private void OnDestroy()
    {
        mainMenuWindow.OnPlayButtonPressed -= StartGame;
        mainMenuWindow.OnExitButtonPressed -= ExitGame;
        gameOverWindow.OnRetryButtonPressed -= RestartGame;
        gameOverWindow.OnExitButtonPressed -= Exit;
    }

    private void Exit() => OnExitButtonPressed?.Invoke();
    private void RestartGame() => OnRestartButtonPressed?.Invoke();
    private void StartGame() => OnPlayButtonPressed?.Invoke();
    private void ExitGame() => OnExitButtonPressed?.Invoke();

    public void UpdateJumpScoreCount(int count) => textScoreJump.text = $"Jumps: {count}";
    public void UpdateCoinScoreCount(int count) => textScoreCoin.text = $"Coins: {count}";

    public void ChangeScreen(GameState state)
    {
        mainMenuWindow.gameObject.SetActive(state == GameState.StartScreen);
        gameOverWindow.gameObject.SetActive(state == GameState.GameOver);
    }
}