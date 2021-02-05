using System;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScoreJump;
    [SerializeField] private TextMeshProUGUI textScoreCoin;
    [SerializeField] private BaseWindow[] uiWindows;

    public event Action OnPlayButtonPressed;
    public event Action OnRestartButtonPressed;
    public event Action OnExitButtonPressed;

    public void Init()
    {
        foreach (var window in uiWindows)
        {
            window.OnPressButtonPlay += StartGame;
            window.OnPressButtonRetry += RestartGame;
            window.OnPressButtonQuit += ExitGame;
        }
        
        UpdateCoinScoreCount(0);
        UpdateJumpScoreCount(0);
    }

    private void OnDestroy()
    {
        foreach (var window in uiWindows)
        {
            window.OnPressButtonPlay -= StartGame;
            window.OnPressButtonRetry -= RestartGame;
            window.OnPressButtonQuit -= ExitGame;
        }
    }
    
    private void RestartGame() => OnRestartButtonPressed?.Invoke();
    private void StartGame() => OnPlayButtonPressed?.Invoke();
    private void ExitGame() => OnExitButtonPressed?.Invoke();

    public void UpdateJumpScoreCount(int count) => textScoreJump.text = $"Jumps: {count}";
    public void UpdateCoinScoreCount(int count) => textScoreCoin.text = $"Coins: {count}";

    public void ChangeScreen(GameState state)
    {
        foreach (var window in uiWindows)
        {
            window.gameObject.SetActive(state == window.GetWindowState);
        }
    }
}