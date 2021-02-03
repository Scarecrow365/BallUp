using System;
using TMPro;
using UnityEngine;

public class UiView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScoreJump;
    [SerializeField] private TextMeshProUGUI textScoreCoin;
    [SerializeField] private UiWindow[] screens;

    public event Action OnPlayButtonPressed;
    public event Action OnExitButtonPressed;

    private void Awake()
    {
        foreach (var screen in screens)
        {
            screen.OnBigButtonPressed += BigButtonPressed;
            screen.OnSmallButtonPressed += SmallButtonPressed;
        }
    }

    private void OnDestroy()
    {
        foreach (var screen in screens)
        {
            screen.OnBigButtonPressed -= BigButtonPressed;
            screen.OnSmallButtonPressed -= SmallButtonPressed;
        }
    }

    private void BigButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
    }

    private void SmallButtonPressed()
    {
        OnExitButtonPressed?.Invoke();
    }

    public void JumpScoreCount(int count)
    {
        textScoreJump.text = $"Jumps: {count}";
    }

    public void CoinScoreCount(int count)
    {
        textScoreCoin.text = $"Coins: {count}";
    }

    public void ChangeScreen(GameState state)
    {
        foreach (var screen in screens) 
            screen.gameObject.SetActive(screen.GetWindowState == state);
    }
}