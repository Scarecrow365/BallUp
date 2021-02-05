using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private PlatformController platformController;
    [SerializeField] private BallController ballController;
    [SerializeField] private UiController uiController;
    [SerializeField] private VfxView vfxView;
    [SerializeField] private Spawner spawner;

    private StateController _stateController;
    private VfxController _vfxController;

    private void Awake()
    {
        _stateController = new StateController();
        _vfxController = new VfxController(vfxView);
        InitGame();
    }

    private void OnDestroy()
    {
        RemoveSubscribers();
    }

    private void InitGame()
    {
        spawner.Init();
        uiController.Init();
        InitSubscribers();
        _stateController.Init();

        var startPlatformList = spawner.InitStartPlatforms();
        platformController.InitStartGame(startPlatformList);
    }

    private void InitSubscribers()
    {
        platformController.OnPlatformDisable += GetNewPlatform;
        platformController.OnDisablePlatformsCountChange += uiController.UpdateJumpScoreCount;
        platformController.OnDisablePlatformsCountChange += _vfxController.SetJumpCount;
        uiController.OnPlayButtonPressed += _stateController.StartGame;
        uiController.OnPlayButtonPressed += ballController.Init;
        uiController.OnRestartButtonPressed += Restart;
        uiController.OnExitButtonPressed += ExitGame;
        ballController.OnFallAction += _stateController.GameOver;
        ballController.OnCoinGet += uiController.UpdateCoinScoreCount;
        ballController.OnCoinGet += _vfxController.SetCoinCount;
        _stateController.OnCurrentState += platformController.SetGameState;
        _stateController.OnCurrentState += ballController.SetGameState;
        _stateController.OnCurrentState += uiController.ChangeScreen;
    }

    private void RemoveSubscribers()
    {
        platformController.OnPlatformDisable -= GetNewPlatform;
        platformController.OnDisablePlatformsCountChange -= uiController.UpdateJumpScoreCount;
        platformController.OnDisablePlatformsCountChange -= _vfxController.SetJumpCount;
        uiController.OnPlayButtonPressed -= _stateController.StartGame;
        uiController.OnExitButtonPressed -= ExitGame;
        ballController.OnFallAction -= _stateController.GameOver;
        ballController.OnCoinGet -= uiController.UpdateCoinScoreCount;
        ballController.OnCoinGet -= _vfxController.SetCoinCount;
        _stateController.OnCurrentState -= platformController.SetGameState;
        _stateController.OnCurrentState -= ballController.SetGameState;
        _stateController.OnCurrentState -= uiController.ChangeScreen;
    }

    private void GetNewPlatform()
    {
        var newPlatform = spawner.SpawnPlatform();
        platformController.GetNewPlatform(newPlatform);
    }

    private void Restart() => SceneManager.LoadScene(0);
    private void ExitGame() => Application.Quit();
}