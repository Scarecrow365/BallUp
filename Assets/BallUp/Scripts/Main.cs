using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private PlatformController platformController;
    [SerializeField] private BallController ballController;
    [SerializeField] private UiController uiController;
    [SerializeField] private Spawner spawner;
    
    private StateController _stateController;

    private void Awake()
    {
        _stateController = new StateController();
        InitGame();
    }

    private void OnDestroy()
    {
        RemoveSubscribers();
    }

    private void InitGame()
    {
        spawner.Init();
        InitSubscribers();
        _stateController.Init();

        var startPlatformList = spawner.InitStartPlatforms();
        platformController.InitStartGame(startPlatformList);
        
        UpdateCoinScore();
        UpdateJumpScore();
    }

    private void InitSubscribers()
    {
        platformController.OnPlatformDisable += GetNewPlatform;
        platformController.OnPlatformDisable += UpdateJumpScore;
        uiController.OnPlayButtonPressed += _stateController.StartGame;
        uiController.OnPlayButtonPressed += ballController.Init;
        uiController.OnExitButtonPressed += ExitGame;
        ballController.OnFallAction += _stateController.GameOver;
        ballController.OnCoinGet += UpdateCoinScore;
        _stateController.OnCurrentState += platformController.SetGameState;
        _stateController.OnCurrentState += ballController.SetGameState;
        _stateController.OnCurrentState += uiController.ChangeScreenState;
    }

    private void RemoveSubscribers()
    {
        platformController.OnPlatformDisable -= GetNewPlatform;
        platformController.OnPlatformDisable -= UpdateJumpScore;
        uiController.OnPlayButtonPressed -= _stateController.StartGame;
        uiController.OnExitButtonPressed -= ExitGame;
        ballController.OnFallAction -= _stateController.GameOver;
        ballController.OnCoinGet -= UpdateCoinScore;
        _stateController.OnCurrentState -= platformController.SetGameState;
        _stateController.OnCurrentState -= ballController.SetGameState;
        _stateController.OnCurrentState -= uiController.ChangeScreenState;
    }

    private void UpdateJumpScore() => uiController.UpdateJumpScore(platformController.GetCountDisabledPlatform);
    private void UpdateCoinScore() => uiController.UpdateCoinScore(ballController.GetCoinInfo);
    
    private void GetNewPlatform()
    {
        var newPlatform = spawner.SpawnPlatform();
        platformController.GetNewPlatform(newPlatform);
    }
    
    private void ExitGame()
    {
        Application.Quit();
    }
}