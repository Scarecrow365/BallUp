using System;
using UnityEngine;
using UnityEngine.UI;

public class UiWindow : MonoBehaviour
{
    [SerializeField] private Button bigButton;
    [SerializeField] private Button smallButton;
    [SerializeField] private GameState showScreenInState;

    public GameState GetWindowState => showScreenInState;
    public event Action OnBigButtonPressed;
    public event Action OnSmallButtonPressed;

    private void Awake()
    {
        bigButton.onClick.AddListener(() => OnBigButtonPressed?.Invoke());
        smallButton.onClick.AddListener(() => OnSmallButtonPressed?.Invoke());
    }
}