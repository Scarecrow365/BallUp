using System;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    [SerializeField] private GameState showScreenInState;

    public event Action OnPressButtonPlay;
    public event Action OnPressButtonQuit;
    public event Action OnPressButtonRetry;

    protected void PlayButtonPressed() => OnPressButtonPlay?.Invoke();
    protected void ExitButtonPressed() => OnPressButtonQuit?.Invoke();
    protected void RetryButtonPressed() => OnPressButtonRetry?.Invoke();
    

    public GameState GetWindowState => showScreenInState;
}