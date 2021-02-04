using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : BaseWindow
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    
    public event Action OnPlayButtonPressed;
    public event Action OnExitButtonPressed;
    
    private void Awake()
    {
        playButton.onClick.AddListener(() => OnPlayButtonPressed?.Invoke());
        exitButton.onClick.AddListener(() => OnExitButtonPressed?.Invoke());
    }
}
