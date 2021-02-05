using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : BaseWindow
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    
    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonPressed);
        exitButton.onClick.AddListener(ExitButtonPressed);
    }
}
