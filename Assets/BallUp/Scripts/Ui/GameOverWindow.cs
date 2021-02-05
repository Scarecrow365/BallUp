using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : BaseWindow
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        retryButton.onClick.AddListener(RetryButtonPressed);
        exitButton.onClick.AddListener(ExitButtonPressed);
    }
}