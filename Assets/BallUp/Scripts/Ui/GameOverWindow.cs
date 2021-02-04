using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : BaseWindow
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    
    public event Action OnRetryButtonPressed;
    public event Action OnExitButtonPressed;

    private void Awake()
    {
        retryButton.onClick.AddListener(() => OnRetryButtonPressed?.Invoke());
        exitButton.onClick.AddListener(() => OnExitButtonPressed?.Invoke());
    }
    
}
